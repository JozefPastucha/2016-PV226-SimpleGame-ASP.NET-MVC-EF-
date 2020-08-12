using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTOs.Players;
using BL.DTOs.Village;

namespace BL.Services.Players
{
    public interface IPlayerService
    {
        int PlayerPageSize { get; }

        /// <summary>
        /// Creates new player (user account must be created first)
        /// </summary>
        /// <param name="userAccountId">player user account id</param>
        int CreatePlayer(Guid userAccountId);

        /// <summary>
        /// Gets player with given username
        /// </summary>
        /// <param name="email">email</param>
        /// <returns>Player with given email address</returns>
        PlayerDTO GetPlayerAccordingToUserName(string UserName);

        /// <summary>
        /// Gets player according to player id
        /// </summary>
        /// <param name="playerId">player id</param>
        /// <returns></returns>
        PlayerDTO GetPlayer(int playerId);
        
        /// <summary>
        /// Deletes player with given id
        /// </summary>
        /// <param name="playerId">player id</param>
        /// <returns>Id of the player's account</returns>
        Guid DeletePlayer(int playerId);

        /// <summary>
        /// Gets players in the game according to sorting criteria and required page
        /// </summary>
        /// <param name="sortingCriteria">sorting criteria</param>
        /// <param name="requiredPage">required page</param>
        /// <returns>All players in the game</returns>
        PlayerListQueryResultDTO ListPlayersPaging(string sortingCriteria, int requiredPage = 1);

        /// <summary>
        /// Lists all players in the game
        /// </summary>
        /// <returns>Lists of players</returns>
        IEnumerable<PlayerDTO> ListPlayers();
    }
}
