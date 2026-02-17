using IRIS.Domain.Entities;
using IRIS.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace IRIS.Infrastructure.Data
{
    public static class SeedData
    {
        public static void Initialize(IrisDbContext context)
        {
            if (context.Users.Any()) return;
            var usersToSeed = new List<User>();

            var hasher = new PasswordHasher<User>();

            var dean = new User { Username = "dean", Role = UserRole.Dean, isFirstLogin = true, IsActive = true };
            dean.PasswordHash = hasher.HashPassword(dean, "dean");
            usersToSeed.Add(dean);

            var assistantdean = new User { Username = "assistantdean", Role = UserRole.AssistantDean, isFirstLogin = true, IsActive = true };
            assistantdean.PasswordHash = hasher.HashPassword(assistantdean, "assistantdean");
            usersToSeed.Add(assistantdean);

            var qa = new User { Username = "qa", Role = UserRole.QA, isFirstLogin = true, IsActive = true };
            qa.PasswordHash = hasher.HashPassword(qa, "qa");
            usersToSeed.Add(qa);

            for (int i = 0; i <= 10; i++)
            {
                string uname = $"officestaff{i}";
                var officer = new User { Username = uname, Role = UserRole.OfficeStaff, isFirstLogin = true, IsActive = true };
                officer.PasswordHash = hasher.HashPassword(officer, uname);
                usersToSeed.Add(officer);
            }

            context.Users.AddRange(usersToSeed);
            context.SaveChanges();
        }
    }
}
