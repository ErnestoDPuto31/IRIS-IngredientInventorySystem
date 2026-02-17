using IRIS.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IRIS.Domain.Entities
{
    public class Request
    {
        [Key] public int RequestId { get; set; }

        [Required, MaxLength(100)] public string? Subject { get; set; }
        [Required, MaxLength(100)] public string? FacultyName { get; set; }
        [Required] public int StudentCount { get; set; }
        [Required] public RequestStatus Status { get; set; } = RequestStatus.Pending;
        public DateTime DateOfUse { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [Required] public int EncodedById { get; set; }
        [ForeignKey("EncodedById")] public virtual User? EncodedBy { get; set; }
        public virtual ICollection<RequestDetails> RequestItems { get; set; } = new List<RequestDetails>();
        public virtual ICollection<Approval> Approvals { get; set; } = new List<Approval>();

        [MaxLength(500)]
        public string? Remarks { get; set; }
    }
}