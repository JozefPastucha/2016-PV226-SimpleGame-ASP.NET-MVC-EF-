using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using BL.DTOs.AdventureTypes;
using BL.DTOs.Units;

namespace BL.Services.Adventures
{
    public interface IAdventureService
    {
        /// <summary>
        /// Checks if the village really has this many units
        /// </summary>
        /// <param name="units">units on the adventure</param>
        /// <returns>true if the village really has these units, false otherwise</returns>
        bool CheckUnits(IEnumerable<UnitDTO> units);
        
        /// <summary>
        /// Returns number of units sent on the adventure
        /// </summary>
        /// <param name="units">units on the adventure</param>
        /// <returns>number of units sent on the adventure</returns>
        int numOfUnits(IEnumerable<UnitDTO> units);


        /// <summary>
        /// Checks if the village has enough bread for the units
        /// </summary>
        /// <param name="units">units on the adventure</param>
        /// <param name="adventureType">type of adventure which is currently in progress</param>
        /// <returns>true if the village has enough bread, false otherwise</returns>
        bool CheckBread(IEnumerable<UnitDTO> units, AdventureType adventureType);

        /// <summary>
        /// Finds unit with the highest hp on the adventure
        /// </summary>
        /// <param name="units"></param>
        /// <returns>DTO of the tank</returns>
        UnitDTO FindTank(IEnumerable<UnitDTO> units);

        /// <summary>
        /// Gets the damage of all living units on an adventure
        /// </summary>
        /// <param name="units">units on the adventure</param>
        /// <returns>damage of the units</returns>
        int GetMyUnitsDMG(IEnumerable<UnitDTO> units);

        /// <summary>
        /// Kills a unit(Updates its count in the database)
        /// </summary>
        /// <param name="unitId">id of the unit to be killed</param>
        void Kill(int unitId);

        /// <summary>
        /// Sets the adventure of this type and in this village to accomplished
        /// </summary>
        /// <param name="villageID">village id</param>
        /// <param name="adventureTypeId">adventure type id</param>
        void Accomplished(int villageID, int adventureTypeId);

        /// <summary>
        /// Rewards the village with according to adventureType
        /// </summary>
        /// <param name="adventureType">type of adventure which is currently in progress</param>
        /// <param name="villageID">village id</param>
        void GetReward(AdventureType adventureType, int villageID);

        /// <summary>
        /// Carries out the adventure
        /// </summary>
        /// <param name="units">units sent on the adventure</param>
        /// <param name="adventureTypeId">adventure type id</param>
        /// <returns>true if the units killed all the enemies, false otherwise</returns>
        bool Adventure(IEnumerable<UnitDTO> units, int adventureTypeId);

        /// <summary>
        /// Lists all adventures accessible to the village(the first and all those whose previous adventure is accomplished)
        /// </summary>
        /// <param name="villageId">village id</param>
        /// <returns>List of adventures</returns>
        IEnumerable<AdventureDTO> ListAdventures(int villageId);
    }
}
