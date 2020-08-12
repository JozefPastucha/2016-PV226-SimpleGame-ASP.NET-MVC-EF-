using System;
using BL.DTOs.UserAccount;

namespace BL.Services.User
{
    /// <summary>
    /// Provides user account related functionality
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Registers user (typically with default claims)
        /// </summary>
        /// <param name="userRegistration">User registration details</param>
        /// <param name="createAdmin">Grant user admin rights</param>
        /// <returns>ID of registered user</returns>
        Guid RegisterUserAccount(UserRegistrationDTO userRegistration, bool createAdmin = false);

        /// <summary>
        /// Authenticates user with given username and password
        /// </summary>
        /// <param name="loginDto">user login details</param>
        /// <returns>ID of the authenticated user</returns>
        Guid AuthenticateUser(UserLoginDTO loginDto);

        /// <summary>
        /// Deletes the user account
        /// </summary>
        /// <param name="accountID">account id</param>
        void DeleteAccount(Guid accountID);
    }
}