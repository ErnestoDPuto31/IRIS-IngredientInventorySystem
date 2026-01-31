using IRIS.Domain.Entities;
using IRIS.Domain.Enums;
using IRIS.Infrastructure.Security;

namespace IRIS.Infrastructure.Data
{
    public static class SeedData
    {
        public static void Initialize(IrisDbContext context)
        {
            if (context.Users.Any()) return; // DB has been seeded

            var usersToSeed = new List<User>();

            usersToSeed.Add(new User
            {
                Username = "dean",
                PasswordHash = PasswordHasher.HashPassword("dean"),
                Role = UserRole.Dean,
                IsActive = true
            });

            usersToSeed.Add(new User
            {
                Username = "assistantdean",
                PasswordHash = PasswordHasher.HashPassword("assistantdean"),
                Role = UserRole.AssistantDean,
                IsActive = true
            });

            usersToSeed.Add(new User
            {
                Username = "qa",
                PasswordHash = PasswordHasher.HashPassword("qa"),
                Role = UserRole.QA,
                IsActive = true
            });

            // Seeding 10 Officers
            for (int i = 1; i <= 10; i++)
            {
                string uname = $"officestaff{i}";

                usersToSeed.Add(new User
                {
                    Username = uname,
                    PasswordHash = PasswordHasher.HashPassword(uname),
                    Role = UserRole.OfficeStaff,
                    IsActive = true,
                });
            }

            context.Users.AddRange(usersToSeed);
            context.SaveChanges();
        }
    }
}
