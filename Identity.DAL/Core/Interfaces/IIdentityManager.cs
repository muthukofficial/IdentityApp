using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.DAL.Models;

namespace Identity.DAL.Core.Interfaces
{
    public interface IIdentityManager
    {
        Task<bool> CheckPasswordAsync(AppUser user, string password);
        Task<(bool Succeeded, string[] Errors)> CreateRoleAsync(AppRole role, IEnumerable<string> claims);
        Task<(bool Succeeded, string[] Errors)> CreateUserAsync(AppUser user, IEnumerable<string> roles, string password);
        Task<(bool Succeeded, string[] Errors)> DeleteRoleAsync(AppRole role);
        Task<(bool Succeeded, string[] Errors)> DeleteRoleAsync(string roleName);
        Task<(bool Succeeded, string[] Errors)> DeleteUserAsync(AppUser user);
        Task<(bool Succeeded, string[] Errors)> DeleteUserAsync(string userId);
        Task<AppRole> GetRoleByIdAsync(string roleId);
        Task<AppRole> GetRoleByNameAsync(string roleName);
        Task<AppRole> GetRoleLoadRelatedAsync(string roleName);
        Task<List<AppRole>> GetRolesLoadRelatedAsync(int page, int pageSize);
        Task<(AppUser User, string[] Roles)?> GetUserAndRolesAsync(string userId);
        Task<AppUser> GetUserByEmailAsync(string email);
        Task<AppUser> GetUserByIdAsync(string userId);
        Task<AppUser> GetUserByUserNameAsync(string userName);
        Task<IList<string>> GetUserRolesAsync(AppUser user);
        Task<List<(AppUser User, string[] Roles)>> GetUsersAndRolesAsync(int page, int pageSize);
        Task<(bool Succeeded, string[] Errors)> ResetPasswordAsync(AppUser user, string newPassword);
        Task<bool> TestCanDeleteRoleAsync(string roleId);
        Task<bool> TestCanDeleteUserAsync(string userId);
        Task<(bool Succeeded, string[] Errors)> UpdatePasswordAsync(AppUser user, string currentPassword, string newPassword);
        Task<(bool Succeeded, string[] Errors)> UpdateRoleAsync(AppRole role, IEnumerable<string> claims);
        Task<(bool Succeeded, string[] Errors)> UpdateUserAsync(AppUser user);
        Task<(bool Succeeded, string[] Errors)> UpdateUserAsync(AppUser user, IEnumerable<string> roles);
    }
}
