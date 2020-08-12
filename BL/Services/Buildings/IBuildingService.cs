using System.Collections.Generic;
using BL.DTOs.Buildings;


namespace BL.Services.Buildings
{
    public interface IBuildingService
    {
        /// <summary>
        /// Builds huts in the village
        /// </summary>
        /// <param name="villageId">village id</param>
        /// <param name="amount">number of huts to be built</param> 
        /// <returns>True if there is enought resources in the village, false otherwise</returns>

        bool BuildHut(int villageId, int amount);

        /// <summary>
        /// Builds a building
        /// </summary>
        /// <param name="buildingId">building id</param>
        /// <returns>True if there is enought resources in the village, false otherwise</returns>

        bool Build(int buildingId);

        /// <summary>
        /// Assigns workers to the building
        /// </summary>
        /// <param name="buildingId">building id</param>
        /// <param name="count">number of workers to be assigned</param>
        void AssignWorkers(int buildingId, int count);

        /// <summary>
        /// Withdraws workers from the building
        /// </summary>
        /// <param name="buildingId">building id</param>
        /// <param name="count">number of workers to be withdrawn</param>
        void WithdrawWorkers(int buildingId, int count);

        /// <summary>
        /// Lists all buildings of a village
        /// </summary>
        /// <param name="villageId">village id</param>
        /// <returns>List of building by village</returns>
        IEnumerable<BuildingDTO> ListBuildingsByVillage(int villageId);

        /// <summary>
        /// Return number of huts in the village
        /// </summary>
        /// <param name="villageId">village id</param>
        /// <returns>number of huts in the village</returns>
        int GetNumOfHuts(int villageId);

        /// <summary>
        /// Returns a building by name and villageId
        /// </summary>
        /// <param name="buildingType">name of the building type</param>
        /// <param name="villageId">village id</param>
        /// <returns>building</returns>
        BuildingDTO GetBuildingByNameAndVillageId(string buildingType, int villageId);

    }
}
