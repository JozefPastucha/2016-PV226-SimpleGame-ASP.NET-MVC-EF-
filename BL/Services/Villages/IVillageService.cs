using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTOs.Village;

namespace BL.Services.Villages
{
    public interface IVillageService
    {
        int VillagesPageSize { get; }

        /// <summary>
        /// Creates a new village and initializes its resources, buildings, products and units
        /// </summary>
        /// <param name="playerID">player id</param>
        void CreateVillage(int playerID);

        /// <summary>
        /// Settles new village
        /// </summary>
        /// <param name="playerId">player id</param>
        /// <returns>true if there is a settler in the village</returns>
        bool SettleNewVillage(int playerId);

        /// <summary>
        /// Gets villages according to player id and required page
        /// </summary>
        /// <param name="playerId">player id</param>
        /// <param name="requiredPage">required page</param>
        /// <returns>villages by player id and required page</returns>
        VillageListQueryResultDTO ListVillagesByPlayerPaging(int playerId, int requiredPage = 1);

        /// <summary>
        /// Lists all villages by player
        /// </summary>
        /// <param name="villageId"></param>
        /// <returns>List of villages by player</returns>
        IEnumerable<VillageDTO> ListVillagesByPlayer(int playerId);    //ine DTO neriesim, vsak toto ma len 2 inty
    }
}
