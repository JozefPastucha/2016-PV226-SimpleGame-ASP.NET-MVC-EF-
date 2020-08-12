using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Services.Units;
using BL.DTOs.Units;

namespace BL.Facades
{
    public class UnitFacade
    {   
        private readonly IUnitService unitService;

        /// <summary>
        /// Trains new units (Increases the amount of this type of unit in the village)
        /// </summary>
        /// <param name="unitId">unit id</param>
        /// <returns>True if there is enough products for the unit to be trained</returns>
        public UnitFacade(IUnitService unitService)
        {
            this.unitService = unitService;
        }

        /// <summary>
        /// Lists all units of a village
        /// </summary>
        /// <param name="villageId"></param>
        /// <returns></returns>
        public bool Train(int unitId, int amount)
        {
            return unitService.Train(unitId, amount);
        }

        public IEnumerable<UnitDTO> ListUnitsByVillage(int villageId)
        {
            return unitService.ListUnitsByVillage(villageId);
        }

    }
}
