using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Services.Buildings;
using BL.Services.Resources;
using BL.DTOs.Buildings;
using BL.DTOs.Resources;

namespace BL.Facades
{
    public class BuildingFacade
    {
        private readonly IBuildingService buildingService;
        private readonly IResourceService resourceService;

        public BuildingFacade(IBuildingService buildingService, IResourceService resourceService)
        {
            this.buildingService = buildingService;
            this.resourceService = resourceService;
        }

        /// <summary>
        /// Builds huts in the village
        /// </summary>
        /// <param name="villageId">village id</param>
        /// <param name="amount">number of huts to be built</param>
        public void BuildHut(int villageId, int amount)
        {
            buildingService.BuildHut(villageId, amount);
        }

        /// <summary>
        /// Builds a building in the village
        /// </summary>
        /// <param name="buildingId">building id</param>
        public void BuildBuilding(int buildingId)
        {
            buildingService.Build(buildingId);
        }

        /// <summary>
        /// Assigns workers to a building
        /// </summary>
        /// <param name="buildingId">building id</param>
        /// <param name="count">number of workers to be assigned</param>
        public void AssignWorkers(int buildingId, int count)
        {
            buildingService.AssignWorkers(buildingId, count);
        }

        /// <summary>
        /// Withdraws workers from a building
        /// </summary>
        /// <param name="buildingId">building id</param>
        /// <param name="count">number of workers to be withdrawn</param>
        public void WithdrawWorkers(int buildingId, int count)
        {
            buildingService.WithdrawWorkers(buildingId, count);
        }

        /// <summary>
        /// Lists all buildings of a village
        /// </summary>
        /// <param name="villageId">village id</param>
        /// <returns>List of building by village</returns>
        public IEnumerable<BuildingDTO> ListBuildingsByVillage(int villageId)
        {
            return buildingService.ListBuildingsByVillage(villageId);
        }

        /// <summary>
        /// Return number of huts in the village
        /// </summary>
        /// <param name="villageId">village id</param>
        /// <returns>number of huts in the village</returns>
        public int GetNumOfHuts(int villageId)
        {
            return buildingService.GetNumOfHuts(villageId);
        }

        /// <summary>
        /// Returns a building by name and villageId
        /// </summary>
        /// <param name="buildingType">name of the building type</param>
        /// <param name="villageId">village id</param>
        /// <returns>building</returns>
        public BuildingDTO GetBuildingByNameAndVillageId(string buildingType, int villageId)
        {
            return buildingService.GetBuildingByNameAndVillageId(buildingType, villageId);
        }
    }
}
