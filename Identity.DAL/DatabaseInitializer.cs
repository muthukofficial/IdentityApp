using System;
using System.Threading.Tasks;
using Identity.DAL.Core;
using Identity.DAL.Core.Interfaces;
using Identity.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Identity.DAL
{
    public interface IDatabaseInitializer
    {
        Task SeedAsync();
    }



    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly AppDbContext _context;
        private readonly IIdentityManager _identityManager;
        private readonly ILogger _logger;

        public DatabaseInitializer(AppDbContext context, IIdentityManager identityManager, ILogger<DatabaseInitializer> logger)
        {
            _identityManager = identityManager;
            _context = context;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            await _context.Database.MigrateAsync().ConfigureAwait(false);

            if (!await _context.Users.AnyAsync())
            {
                _logger.LogInformation("Generating inbuilt accounts");

                const string adminRoleName = "Administrator";

                await EnsureRoleAsync(adminRoleName, "Inbuilt Administrator", AppPermissions.GetAllPermissionValues());

                await CreateUserAsync("admin", "P@ssw0rd", "Administrator", "identity@gmail.com", "+1234567890", new string[] { adminRoleName });

                _logger.LogInformation("Inbuilt account generation completed");
            }

        }



        private async Task EnsureRoleAsync(string roleName, string description, string[] claims)
        {
            if ((await _identityManager.GetRoleByNameAsync(roleName)) == null)
            {
                AppRole applicationRole = new AppRole(roleName, description);

                var result = await this._identityManager.CreateRoleAsync(applicationRole, claims);

                if (!result.Succeeded)
                    throw new Exception($"Seeding \"{description}\" role failed. Errors: {string.Join(Environment.NewLine, result.Errors)}");
            }
        }

        private async Task<AppUser> CreateUserAsync(string userName, string password, string fullName, string email, string phoneNumber, string[] roles)
        {
            AppUser applicationUser = new AppUser
            {
                UserName = userName,
                FullName = fullName,
                Email = email,
                PhoneNumber = phoneNumber,
                EmailConfirmed = true,
                IsEnabled = true
            };

            var result = await _identityManager.CreateUserAsync(applicationUser, roles, password);

            if (!result.Succeeded)
                throw new Exception($"Seeding \"{userName}\" user failed. Errors: {string.Join(Environment.NewLine, result.Errors)}");


            return applicationUser;
        }
    }
}
