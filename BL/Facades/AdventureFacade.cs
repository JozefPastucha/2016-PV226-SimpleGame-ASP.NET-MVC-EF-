using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Services.Adventures;
using BL.DTOs.Units;
using BL.DTOs.AdventureTypes;

namespace BL.Facades
{
    public class AdventureFacade
    {
        private readonly IAdventureService adventureService;

        public AdventureFacade(IAdventureService adventureService)
        {
            this.adventureService = adventureService;
        }

        /// <summary>
        /// Carries out the process of the adventure
        /// </summary>
        /// <param name="units">units sent on the adventure</param>
        /// <param name="adventureTypeId">adventure type id</param>
        /// <returns></returns>
        public bool Adventure(IEnumerable<UnitDTO> units, int adventureTypeId)
        {
            return adventureService.Adventure(units, adventureTypeId);
        }

        /// <summary>
        /// Lists all adventures accessble to the village
        /// </summary>
        /// <param name="villageId">village id</param>
        /// <returns></returns>
        public IEnumerable<AdventureDTO> ListAdventures(int villageId)
        {
            return adventureService.ListAdventures(villageId);
        }
    }
}
