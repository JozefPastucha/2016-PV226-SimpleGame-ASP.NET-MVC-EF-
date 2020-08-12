using System;
using System.Collections.Generic;
using BL.DTOs.Players;
using BL.DTOs.Village;
using BL.Services.Players;
using BL.Services.Villages;
using BL.Services.User;
using BL.DTOs.UserAccount;

namespace BL.Facades
{
    public class PlayerFacade
    {
        public int PlayerPageSize => playerService.PlayerPageSize;
        private readonly IPlayerService playerService;
        private readonly IVillageService villageService;
        private readonly IUserService _userService;


        public PlayerFacade(IPlayerService playerService, IVillageService villageService, IUserService _userService)
        {
            this.playerService = playerService;
            this.villageService = villageService;
            this._userService = _userService;
        }

        /// <summary>
        /// Gets player (including its user account) according to username
        /// </summary>
        /// <param name="userName">username</param>
        /// <returns>Player with specified username</returns>
        public PlayerDTO GetPlayerAccordingToUserName(string userName)
        {
            return playerService.GetPlayerAccordingToUserName(userName);
        }
 
        /// <summary>
        /// Performs player registration
        /// </summary>
        /// <param name="registrationDto">Player registration details</param>
        /// <param name="success">argument that tells whether the registration was successful</param>
        /// <returns>Registered player account id</returns>
        public Guid RegisterPlayer(UserRegistrationDTO registrationDto, out bool success)
        {
            var player = playerService.GetPlayerAccordingToUserName(registrationDto.UserName);

            if (player != null)
            {
                success = false;
                return new Guid();
            }

            var accountId = _userService.RegisterUserAccount(registrationDto);
            int playerId = playerService.CreatePlayer(accountId);
            villageService.CreateVillage(playerId);
            success = true;
            return accountId;
        }

        /// <summary>
        /// Authenticates user with given username and password
        /// </summary>
        /// <param name="loginDto">user login details</param>
        /// <returns>ID of the authenticated user</returns>
        public Guid AuthenticateUser(UserLoginDTO loginDto)
        {
            return _userService.AuthenticateUser(loginDto);
        }
        
        /// <summary>
        /// Deletes a player
        /// </summary>
        /// <param name="id">player id</param>
        public void DeletePlayer(int id)
        {
            _userService.DeleteAccount(playerService.DeletePlayer(id));

        }

        /// <summary>
        /// Gets village according to player id and required page
        /// </summary>
        /// <param name="playerId">player id</param>
        /// <param name="requiredPage">required page</param>
        /// <returns>villages by player id and required page</returns>
        public VillageListQueryResultDTO ListVillages(int playerId, int requiredPage = 1)
        {
            return villageService.ListVillagesByPlayerPaging(playerId, requiredPage);
        }

        public PlayerDTO GetPlayer(int id)
        {
            return playerService.GetPlayer(id);
        }
        public PlayerListQueryResultDTO ListPlayersPaging(string sortingCriteria, int requiredPage = 1)
        {
            return playerService.ListPlayersPaging(sortingCriteria, requiredPage);
        }

        public IEnumerable<PlayerDTO> ListPlayers()
        {
            return playerService.ListPlayers();
        }
    }
}
