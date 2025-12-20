using BloodBankAPI.Models;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;

namespace BloodBankAPI.Data
{
    public static class SeedData
    {
        public static async Task SeedAsync(AppDbContext db, IConfiguration config)
        {
            await db.Database.MigrateAsync();

            if (!await db.Users.AnyAsync(u => u.Role == "Admin"))
            {
                var admin = new User
                {
                    FullName = "Admin",
                    Email = "admin@bloodbank.local",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(config["Seed:AdminPassword"] ?? "Admin@123"),
                    Role = "Admin"
                };
                db.Users.Add(admin);
                await db.SaveChangesAsync();
            }
        }
    }
}
