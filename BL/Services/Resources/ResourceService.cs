using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.Queries;
using BL.DTOs.Resources;
using BL.DTOs.Filters;
using DAL.Entities;
using BL.DTOs.Buildings;
using BL.Repositories;
namespace BL.Services.Resources
{
    public class ResourceService : GameService, IResourceService
    {
        private readonly ResourceListQuery resourceListQuery;
        private readonly BuildingListQuery buildingListQuery;
        private readonly ResourceRepository resourceRepository;

        public ResourceService(ResourceListQuery resourceListQuery, BuildingListQuery buildingListQuery, ResourceRepository resourceRepository)
        {
            this.resourceListQuery = resourceListQuery;
            this.buildingListQuery = buildingListQuery;
            this.resourceRepository = resourceRepository;
        }

        public IEnumerable<ResourceDTO> ListResourcesByVillage(int villageId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                resourceListQuery.Filter = new ResourceFilter { VillageId = villageId };
                return resourceListQuery.Execute() ?? new List<ResourceDTO>();
            }
        }

        public void AddResources(TimeSpan timeSpan, int villageId)    //budova si pamata resource
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                buildingListQuery.Filter = new BuildingFilter { VillageId = villageId, WorkersAssigned = true };
                IEnumerable<BuildingDTO> buildings = buildingListQuery.Execute();

                Resource[] resources = new Resource[buildings.Count()];

                for (int i = 0; i < buildings.Count(); ++i)
                {
                    resourceListQuery.Filter = new ResourceFilter { VillageId = villageId, ResourceType = buildings.ElementAt(i).ResourceType };
                    resources[i] = resourceRepository.GetById(resourceListQuery.Execute().SingleOrDefault().ID, r => r.ResourceType, r => r.Village);
                    if (resources == null)
                    {
                        throw new NullReferenceException("Resource Service - AddResources(...) resource cant be null");
                    }
                    resources[i].Amount += buildings.ElementAt(i).WorkersAssigned * buildings.ElementAt(i).ProductionPerWorker * timeSpan.Seconds;
                    resourceRepository.Update(resources[i]);
                    uow.Commit();
                }
            }
        }
    }
}
