using StaffDBContext_Code_first.Model.DTO;
using StaffWebApi.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffWebApi.BL.Services
{
    public partial class UsersService
    {
        public async Task<Exception> AssignRole(UserDbDto user, string role)
        {
            var result = await userManager.AddToRoleAsync(user, role);
            if (!result.Succeeded)
            {
                return new Exception($"Не удалось назначить пользователю {user.UserName} роль {role}.");
            }
            return null;
        }

        public async Task<Exception> AssignRole(string userName, string role)
        {
            return await ApplyToUser(userName, user => AssignRole(user, role));
        }

        public async Task<Exception> RemoveFromRole(UserDbDto user, string role)
        {
            var result = await userManager.RemoveFromRoleAsync(user, role);
            if (!result.Succeeded)
            {
                return new Exception($"Не удалось удалить у пользователя {user.UserName} роль {role}.");
            }
            return null;
        }

        public async Task<Exception> RemoveFromRole(string userName, string role)
        {
            return await ApplyToUser(userName, user => RemoveFromRole(user, role));
        }

        public async Task<Exception> SetRoles(UserDbDto user, IEnumerable<string> roles, bool updateCookies)
        {
            var result = await userManager.RemoveFromRolesAsync(user, await userManager.GetRolesAsync(user));
            if (result.Succeeded)
            {
                result = await userManager.AddToRolesAsync(user, roles);
            }

            if (!result.Succeeded)
            {
                return new SaveChangesException($"Не удалось назначить пользователю {user.UserName} роли");
            }

            if (updateCookies)
            {
                await signInManager.RefreshSignInAsync(user);
            }

            return null;
        }

    }
}
