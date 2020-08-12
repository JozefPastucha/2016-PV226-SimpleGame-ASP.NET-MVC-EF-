using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Services.Villages;
using BL.DTOs.Village;
using BL.Services.Resources;
using BL.Services.Adventures;

namespace BL.Facades
{
    public class VillageFacade
    {
        public int VillagePageSize => villageService.VillagesPageSize;

        private readonly IVillageService villageService;
        private readonly IResourceService resourceService;
        private readonly IAdventureService adventureService;

        public VillageFacade(IVillageService villageService, IResourceService resourceService, IAdventureService adventureService)
        {
            this.villageService = villageService;
            this.resourceService = resourceService;
            this.adventureService = adventureService;
        }

        /// <summary>
        /// Settles new village
        /// </summary>
        /// <param name="playerId">player id</param>
        /// <returns>true if there is a settler in the village</returns>
        public bool SettleNewVillage(int villageId)
        {
            return villageService.SettleNewVillage(villageId);
        }

        /// <summary>
        /// Gets villages according to player id and required page for admin.
        /// </summary>
        /// <param name="playerId">player id</param>
        /// <param name="requiredPage">required page</param>
        /// <returns>villages by player id and required page</returns>
        public VillageListQueryResultDTO ListVillagesByPlayerPaging(int playerId, int requiredPage = 1)
        {
            return villageService.ListVillagesByPlayerPaging(playerId, requiredPage);
        }

        /// <summary>
        /// Lists all villages by player for adding resoursec
        /// </summary>
        /// <param name="villageId"></param>
        /// <returns>List of villages by player</returns>
        public IEnumerable<VillageDTO> ListVillagesByPlayer(int playerId)
        {
            return villageService.ListVillagesByPlayer(playerId);
        }
    }
}
