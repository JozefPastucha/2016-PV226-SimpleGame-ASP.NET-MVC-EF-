using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Services.User;
using System;
using BL.DTOs.UserAccount;
using BL.Facades;
using Castle.Windsor;

namespace BL.Bootstrap
{
    public static class UserAccountInit
    {
        /// <summary>
        /// Initializes DB with admin's user account
        /// </summary>
        /// <param name="container"></param>
        public static void InitializeUserAccounts(IWindsorContainer container)
        {
            CreateUsers(container);
        }

        private static void CreateUsers(IWindsorContainer container)
        {
            var userAccountManagementService = container.Resolve<IUserService>();

            userAccountManagementService.RegisterUserAccount(new UserRegistrationDTO
            {
                UserName = "Admin",
                Email = "admin@game.com",
                Password = "admin1234"
            }, true);
        }
    }
}
