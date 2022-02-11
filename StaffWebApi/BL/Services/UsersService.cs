using Microsoft.AspNetCore.Identity;
using StaffDBContext_Code_first.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StaffWebApi.BL.Services
{
    public partial class UsersService
    {
        private readonly UserManager<UserDbDto> userManager;
        private readonly SignInManager<UserDbDto> signInManager;

        public UsersService(UserManager<UserDbDto> userManager, SignInManager<UserDbDto> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        async Task<Exception> ApplyToUser(string userName, Func<UserDbDto, Task<Exception>> method)
        {
            var user = await userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return new KeyNotFoundException($"Пользователь {userName} не найден.");
            }
            return await method(user);
        }

        async Task<bool> UserExists(string userName)
        {
            return await userManager.FindByNameAsync(userName) != null;
        }
        async Task<Exception> ApplyToUserAsync(string userName, Func<UserDbDto, Task<Exception>> method)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return new KeyNotFoundException($"Пользователь {userName} не найден");
            }
            return await method(user);
        }
        public async Task<Exception> SetRoles(string userName, IEnumerable<string> roles, bool updateCookies = false)
        {
            return await ApplyToUserAsync(userName, user => SetRoles(user, roles, updateCookies));
        }
    }
}
