using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IRIS.Domain.Enums;

namespace IRIS.Domain.Entities
{
    public class Approval
    {
        [Key] public int ApprovalId { get; set; }

        public int RequestId { get; set; }
        public int ApproverId { get; set; } 

        public DateTime ActionDate { get; set; } = DateTime.Now;
        public RequestStatus ActionType { get; set; } = RequestStatus.Pending;  
        public string? Remarks { get; set; }
        [ForeignKey("RequestId")] public virtual Request Request { get; set; }

        [ForeignKey("ApproverId")] public virtual User Approver { get; set; }
    }
}