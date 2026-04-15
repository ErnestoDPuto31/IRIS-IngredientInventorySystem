using IRIS.Infrastructure.Data;
using IRIS.Domain.Entities;
using IRIS.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using IRIS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IRIS.Services.Implementations
{
    public class RequestService : IRequestService
    {
        private readonly IrisDbContext _context;

        public RequestService(IrisDbContext context)
        {
            _context = context;
        }

        public List<string> GetUniqueFacultyNames()
        {
            return _context.Requests
                .Where(r => !string.IsNullOrWhiteSpace(r.FacultyName))
                .Select(r => r.FacultyName)
                .Distinct()
                .OrderBy(name => name)
                .ToList();
        }

        public int GetPendingRequestCount()
        {
            return _context.Requests.Count(r => r.Status == RequestStatus.Pending);
        }

        public Request GetRequestById(int id)
        {
            return _context.Requests
                .Include(r => r.RequestItems)
                    .ThenInclude(ri => ri.Ingredient)
                .Include(r => r.EncodedBy)
                .Include(r => r.Approvals)
                    .ThenInclude(a => a.Approver)
                .FirstOrDefault(r => r.RequestId == id);
        }

        public List<Request> GetAllRequests()
        {
            return _context.Requests
                .Include(r => r.EncodedBy)
                .OrderByDescending(r => r.CreatedAt)
                .ToList();
        }

        public void UpdateRequestStatus(int requestId, RequestStatus newStatus, string remarks, int currentUserId)
        {
            var strategy = _context.Database.CreateExecutionStrategy();
            strategy.Execute(() =>
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var request = _context.Requests
                            .Include(r => r.RequestItems)
                            .ThenInclude(ri => ri.Ingredient)
                            .FirstOrDefault(r => r.RequestId == requestId);

                        if (request == null) throw new Exception("Request not found");

                        var localNotificationService = new NotificationService(_context);

                        if (newStatus == RequestStatus.Released && request.Status != RequestStatus.Released)
                        {
                            foreach (var item in request.RequestItems)
                            {
                                var ingredient = item.Ingredient;

                                if (ingredient != null)
                                {
                                    if (ingredient.CurrentStock < item.RequestedQty)
                                    {
                                        throw new InvalidOperationException($"Insufficient stock for: {ingredient.Name}. Available: {ingredient.CurrentStock}, Requested: {item.RequestedQty}");
                                    }

                                    ingredient.CurrentStock -= item.RequestedQty;
                                    ingredient.UpdatedAt = DateTime.Now;

                                    if (ingredient.CurrentStock == 0)
                                    {
                                        localNotificationService.CreateLowStockNotification(ingredient.IngredientId, ingredient.Name, true);
                                    }
                                    else if (ingredient.CurrentStock <= ingredient.MinimumStock)
                                    {
                                        localNotificationService.CreateLowStockNotification(ingredient.IngredientId, ingredient.Name, false);
                                    }
                                }
                            }
                        }

                        request.Status = newStatus;
                        request.UpdatedAt = DateTime.Now;

                        var approval = new Approval
                        {
                            RequestId = requestId,
                            ApproverId = currentUserId,
                            ActionType = newStatus,
                            Remarks = remarks,
                            ActionDate = DateTime.Now
                        };

                        _context.Approvals.Add(approval);
                        _context.SaveChanges();

                        var user = _context.Users.FirstOrDefault(u => u.UserId == currentUserId);
                        string actionByName = user != null ? user.Username : "System";

                        localNotificationService.ResolveRequestNotification(requestId, newStatus.ToString(), actionByName);
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            });
        }

        public void CreateRequest(Request newRequest, List<RequestDetails> items)
        {
            var strategy = _context.Database.CreateExecutionStrategy();
            strategy.Execute(() =>
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        newRequest.CreatedAt = DateTime.Now;
                        newRequest.Status = RequestStatus.Pending;

                        _context.Requests.Add(newRequest);
                        _context.SaveChanges();

                        foreach (var item in items)
                        {
                            item.RequestId = newRequest.RequestId;
                            _context.RequestItems.Add(item);
                        }

                        _context.SaveChanges();

                        // Commit the transaction
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            });
        }

        public int GetApprovedRequestCount()
        {
            return _context.Requests.Count(r =>
                r.Status == RequestStatus.Approved);
        }
    }
}