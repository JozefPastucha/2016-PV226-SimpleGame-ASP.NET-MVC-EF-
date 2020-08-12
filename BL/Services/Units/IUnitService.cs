using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.DTOs.Units;

namespace BL.Services.Units
{
    public interface IUnitService
    {
        /// <summary>
        /// Trains a new unit(Updates the count of the unit)
        /// </summary>
        /// <param name="unitId">unit id</param>
        /// <param name="amount">amount of units to be trained</param>
        /// <returns>True if there is enough products for the unit to be trained</returns>
        bool Train(int unitId, int amount); 
        
        /// <summary>
        /// Lists all units of a village
        /// </summary>
        /// <param name="villageId"></param>
        /// <returns></returns>
        IEnumerable<UnitDTO> ListUnitsByVillage(int villageId); //trening, a adventures
    }
}
