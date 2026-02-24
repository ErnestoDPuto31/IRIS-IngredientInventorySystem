using System.Collections.Generic;
using IRIS.Domain.Entities;
using IRIS.Domain.Enums;

namespace IRIS.Services.Interfaces
{
    public interface IRequestService
    {
        List<Request> GetAllRequests();

        Request GetRequestById(int id);
        int GetPendingRequestCount();
        void UpdateRequestStatus(int requestId, RequestStatus newStatus, string remarks, int currentUserId);
        void CreateRequest(Request newRequest, List<RequestDetails> items);
    }
}