using System.ComponentModel.DataAnnotations;

namespace IRIS.Domain.Entities
{
    public class Request
    {
        [Key] public int RequestId { get; set; }

        [Required, MaxLength(100)] public string? Subject { get; set; }
        [Required, MaxLength(100)] public string? FacultyName { get; set; }
        [Required] public int StudentCount { get; set; }
        [Required, MaxLength(20)] public string? Status { get; set; }
        [Required] public int EncodedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        //Navigation
        public ICollection<RequestItem>? RequestItems { get; set; }
        public ICollection<Approval>? Approvals { get; set; }
    }
}
