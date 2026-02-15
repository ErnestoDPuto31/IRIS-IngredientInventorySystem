using IRIS.Infrastructure.Data;
using IRIS.Domain.Entities;
using IRIS.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using IRIS.Services.Interfaces;

namespace IRIS.Services.Implementations
{
    public class RequestService : IRequestService
    {
        private readonly IrisDbContext _context;

        public RequestService(IrisDbContext context)
        {
            _context = context;
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
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {

                    var request = _context.Requests
                        .Include(r => r.RequestItems)
                        .ThenInclude(ri => ri.Ingredient)
                        .FirstOrDefault(r => r.RequestId == requestId);

                    if (request == null) throw new Exception("Request not found");

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
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw; 
                }
            }
        }

        public void CreateRequest(Request newRequest, List<RequestItem> items)
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
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}