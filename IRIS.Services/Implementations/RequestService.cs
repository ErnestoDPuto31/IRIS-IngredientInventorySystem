using IRIS.Infrastructure.Data;
using IRIS.Domain.Entities;
using IRIS.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace IRIS.Services.Implementations
{
    public class RequestService
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

        public void UpdateRequestStatus(int requestId, RequestStatus newStatus, string remarks, int currentUserId)
        {
            var request = _context.Requests.Find(requestId);

            if (request != null)
            {
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
            }
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

        public void ApproveRequest(int requestId, int approverId, string remarks, UserRole userRole)
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

                    if (request.Status == RequestStatus.Released)
                        throw new Exception("Request is already released.");

                    if (request.Status == RequestStatus.Rejected)
                        throw new Exception("Cannot approve a rejected request.");

                    // Log the Approval Action (Audit Trail)
                    var approval = new Approval
                    {
                        RequestId = requestId,
                        ApproverId = approverId,
                        ActionType = RequestStatus.Approved,
                        Remarks = remarks,
                        ActionDate = DateTime.Now
                    };
                    _context.Approvals.Add(approval);

                    // Role-Based Logic
                    if (userRole == UserRole.Dean)
                    {
                        // DEAN LOGIC: Validate Stock -> Subtract -> Release ===

                        foreach (var item in request.RequestItems)
                        {
                            var inventoryItem = item.Ingredient; // Already included above

                            // Check Stock
                            if (inventoryItem.CurrentStock < item.RequestedQty)
                            {
                                throw new Exception($"Insufficient stock for {inventoryItem.Name}. " +
                                                    $"Current: {inventoryItem.CurrentStock}, Needed: {item.RequestedQty}");
                            }

                            // Deduct Stock
                            inventoryItem.CurrentStock -= item.RequestedQty;
                        }

                        request.Status = RequestStatus.Released; // Dean sets it to Released
                    }
                    else if (userRole == UserRole.AssistantDean)
                    {
                        // === ASSISTANT DEAN LOGIC: Approve Only ===
                        // Does NOT subtract stock yet. Just changes status to Approved.

                        request.Status = RequestStatus.Approved;
                    }
                    else
                    {
                        throw new UnauthorizedAccessException("You do not have permission to approve requests.");
                    }

                    request.UpdatedAt = DateTime.Now;

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

        public void RejectRequest(int requestId, int rejectorId, string remarks)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var request = _context.Requests.Find(requestId);
                    if (request == null) throw new Exception("Request not found");

                    // Log the Rejection
                    var approval = new Approval
                    {
                        RequestId = requestId,
                        ApproverId = rejectorId,
                        ActionType = RequestStatus.Rejected,
                        Remarks = remarks,
                        ActionDate = DateTime.Now
                    };
                    _context.Approvals.Add(approval);

                    // Update Status
                    request.Status = RequestStatus.Rejected;
                    request.UpdatedAt = DateTime.Now;

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