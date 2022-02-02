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

        public UsersService(UserManager<UserDbDto> userManager)
        {
            this.userManager = userManager;
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
    }
}
