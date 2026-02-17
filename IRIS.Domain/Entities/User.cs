using System.ComponentModel.DataAnnotations;
using IRIS.Domain.Enums;

namespace IRIS.Domain.Entities
{
    public class User
    {
        [Key] public int UserId { get; set; }
        [Required, MaxLength(50)] public string? Username { get; set; }
        [Required, MaxLength(225)] public string? PasswordHash { get; set; }
        public UserRole Role { get; set; }
        public bool isFirstLogin { get; set; } = true;
        public bool IsActive { get; set; }
        public bool IsLoggedIn { get; set; } = false;
        public string? SessionToken { get; set; }
    }
}
