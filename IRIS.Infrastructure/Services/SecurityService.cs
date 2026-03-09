using IRIS.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace IRIS.Infrastructure.Services
{
    public class SecurityService
    {
        private readonly PasswordHasher<User> _hasher;

        public SecurityService()
        {
            _hasher = new PasswordHasher<User>();
        }

        public string HashPassword(User user, string plainTextPassword)
        {
            return _hasher.HashPassword(user, plainTextPassword);
        }

        public bool VerifyPassword(User user, string hashedPassword, string providedPassword)
        {
            var result = _hasher.VerifyHashedPassword(user, hashedPassword, providedPassword);
            return result == PasswordVerificationResult.Success;
        }

        public string HashSecurityAnswer(User user, string plainTextAnswer)
        {
            if (string.IsNullOrWhiteSpace(plainTextAnswer)) return string.Empty;
            return _hasher.HashPassword(user, plainTextAnswer.Trim().ToLower());
        }

        public bool VerifySecurityAnswer(User user, string hashedAnswer, string providedAnswer)
        {
            if (string.IsNullOrWhiteSpace(providedAnswer) || string.IsNullOrWhiteSpace(hashedAnswer)) return false;

            var result = _hasher.VerifyHashedPassword(user, hashedAnswer, providedAnswer.Trim().ToLower());
            return result == PasswordVerificationResult.Success;
        }
    }
}