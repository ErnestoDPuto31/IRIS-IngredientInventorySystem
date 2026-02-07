using IRIS.Infrastructure.Data;
using IRIS.Domain.Entities;
using IRIS.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace IRIS.Services.Implementations
{
    public class RequestService
    {
        private readonly IrisDbContext _context;
        public RequestService(IrisDbContext context) { _context = context; }

        public Request GetRequestById(int id)
        {
            return _context.Requests
                .Include(r => r.RequestItems)
                .ThenInclude(r => r.Ingredient)
                .Include(r => r.EncodedBy)
                .Include(r => r.Approvals)
                .FirstOrDefault(r => r.RequestId == id);
        }

        public List<Request> GetAllRequests()
        {
            return _context.Requests
                .Include(r => r.EncodedBy)
                .OrderByDescending(r => r.CreatedAt)
                .ToList();
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

        public void ProcessApproval(int requestId, int approverId, bool isApproved, string remarks)
        {
            var request = _context.Requests.Find(requestId);
            if (request == null) throw new Exception("Request not found");

            var approval = new Approval
            {
                RequestId = requestId,
                ApproverId = approverId,
                ActionType = isApproved ? RequestStatus.Approved : RequestStatus.Rejected,
                Remarks = remarks,
                ActionDate = DateTime.Now
            };
            _context.Approvals.Add(approval);

            if (isApproved) request.Status = RequestStatus.Approved;
            else request.Status = RequestStatus.Rejected;

            _context.SaveChanges();
        }

        public void ReleaseRequest(int requestId)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var request = _context.Requests
                        .Include(r => r.RequestItems)
                        .FirstOrDefault(r => r.RequestId == requestId);

                    if (request == null) return;
                    if (request.Status != RequestStatus.Approved)
                        throw new Exception("Only Approved Requests can be Released.");

                    foreach (var item in request.RequestItems)
                    {
                        var inventoryItem = _context.Ingredients.Find(item.IngredientId);

                        if (inventoryItem != null)
                        {
                            // FIX: Check condition first!
                            if (inventoryItem.CurrentStock < item.RequestedQty)
                            {
                                throw new Exception($"Not enough stock for {inventoryItem.Name}. Current: {inventoryItem.CurrentStock}, Needed: {item.RequestedQty}");
                            }

                            // Deduct stock
                            inventoryItem.CurrentStock -= item.RequestedQty;
                        }
                    }

                    request.Status = RequestStatus.Released;
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
