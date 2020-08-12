using System;
using System.Collections.Generic;
using System.Linq;
using BL.Repositories;
using BL.DTOs.Buildings;
using BL.Queries;
using BL.DTOs.Filters;
using DAL.Entities;

namespace BL.Services.Buildings
{
    public class BuildingService : GameService, IBuildingService
    {
        private readonly BuildingRepository buildingRepository;
        private readonly VillageRepository villageRepository;
        private readonly PlayerRepository playerRepository;
        private readonly ResourceRepository resourceRepository;
        private readonly BuildingListQuery buildingListQuery;
        private readonly ResourceListQuery resourceListQuery;

        public BuildingService(BuildingRepository buildingRepository, VillageRepository villageRepository, PlayerRepository playerRepository, ResourceRepository resourceRepository, BuildingListQuery buildingListQuery, ResourceListQuery resourceListQuery)
        {
            this.buildingRepository = buildingRepository;
            this.villageRepository = villageRepository;
            this.playerRepository = playerRepository;
            this.resourceRepository = resourceRepository;
            this.buildingListQuery = buildingListQuery;
            this.resourceListQuery = resourceListQuery;
        }

        public bool BuildHut(int villageId, int amount)     
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var village = villageRepository.GetById(villageId, s => s.Player);
                
                if (village == null)
                {
                    throw new NullReferenceException("Building service - BuildHut(...) village cant be null");
                }

                resourceListQuery.Filter = new ResourceFilter { ResourceType = "Wood", VillageId = villageId };
                var wood = resourceRepository.GetById(resourceListQuery.Execute().SingleOrDefault().ID, r => r.ResourceType, r => r.Village);
                /*if (wood == null)
                {
                    throw new NullReferenceException("Building Service - BuildHut(...) wood cant be null");
                }*/

                resourceListQuery.Filter = new ResourceFilter { ResourceType = "Stone", VillageId = villageId };
                var stone = resourceRepository.GetById(resourceListQuery.Execute().SingleOrDefault().ID, r => r.ResourceType, r => r.Village);
                /*if (stone == null)
                {
                    throw new NullReferenceException("Building Service - BuildHut(...) stone cant be null");
                }*/

                if ((wood.Amount < (village.Huts * 100 + 100) * amount) || (stone.Amount < (village.Huts * 60 + 60) * amount))
                    return false;
                     
                wood.Amount -= (village.Huts * 100 + 100) * amount;
                stone.Amount -= (village.Huts * 60 + 60) * amount;
                village.Huts += amount;
                village.AvailableWorkers = village.AvailableWorkers += 4 * amount;
                resourceRepository.Update(wood);
                resourceRepository.Update(stone);
                villageRepository.Update(village);
                uow.Commit();
                return true;
            }
        }

        public bool Build(int buildingId)  
        {
            using (var uow = UnitOfWorkProvider.Create()) 
            {
                var building = buildingRepository.GetById(buildingId, b => b.Village, b => b.BuildingType);
                if (building == null)
                {
                    throw new NullReferenceException("Building Service - Build(...) building cant be null");
                }
 
                string[] splitted = building.BuildingType.Cost.Split(null);  

                //zisti ci je dost sur
                Resource[] resources = new Resource[splitted.Count() / 2];

                for (int i = 0; i < splitted.Count() / 2; ++i)
                {
                    resourceListQuery.Filter = new ResourceFilter { VillageId = building.Village.ID, ResourceType = splitted[2 * i]};
                    resources[i] = resourceRepository.GetById(resourceListQuery.Execute().SingleOrDefault().ID, r => r.ResourceType, r => r.Village);

                    if (resources[i] == null)
                    {
                        throw new NullReferenceException("Building Service - Build(...) resource cant be null"); //zle costs v building
                    }

                    if (resources[i].Amount < Int32.Parse(splitted[2 * i + 1]))
                        return false; //skonci fciu ak neni dost niektorej sur
                }
                
                building.Built = true;

                for (int i = 0; i < splitted.Count() / 2; ++i)
                {
                    resources[i].Amount -= Int32.Parse(splitted[2 * i + 1]);
                    resourceRepository.Update(resources[i]);
                }

                buildingRepository.Update(building);
                uow.Commit();
                return true;
            }
        }
        
        public void AssignWorkers(int buildingId, int count) //mozno nejaka navr hodnota?? ze ci sa to podarilo alebo ne?
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var building = buildingRepository.GetById(buildingId, b => b.BuildingType, b => b.Village);
                if (building == null)
                {
                    throw new NullReferenceException("Building Service - AddWorkers(...) building cant be null");
                }

                building.Village.Player = playerRepository.GetById(building.Village.Player.ID);
               
                if (building.Village.AvailableWorkers >= count)  
                {
                    building.WorkersAssigned += count;
                    building.Village.AvailableWorkers -= count;
                }
                
                buildingRepository.Update(building);
                villageRepository.Update(building.Village); 
                uow.Commit();
            }
        }

        public void WithdrawWorkers(int buildingId, int count)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var building = buildingRepository.GetById(buildingId, b => b.BuildingType, b => b.Village);
                if (building == null)
                {
                    throw new NullReferenceException("Building Service - RemoveWorkers(...) building cant be null");
                }

                building.Village.Player = playerRepository.GetById(building.Village.Player.ID);


                if (building.WorkersAssigned >= count)  
                {
                    building.WorkersAssigned -= count;
                    building.Village.AvailableWorkers += count;
                }
                
                buildingRepository.Update(building);
                villageRepository.Update(building.Village);
                uow.Commit();
            }
        }

        public IEnumerable<BuildingDTO> ListBuildingsByVillage(int villageId)
        {
            using (UnitOfWorkProvider.Create())
            {
                buildingListQuery.Filter = new BuildingFilter { VillageId = villageId };
                return buildingListQuery.Execute() ?? new List<BuildingDTO>();          //??
            }
        }

        public int GetNumOfHuts(int villageId)
        {
            using (UnitOfWorkProvider.Create())
            {
                return villageRepository.GetById(villageId).Huts;
            }
        }

        public BuildingDTO GetBuildingByNameAndVillageId(string buildingType, int villageId)
        {
            using (UnitOfWorkProvider.Create())
            {
                buildingListQuery.Filter = new BuildingFilter { BuildingType = buildingType, VillageId = villageId };
                var building = buildingListQuery.Execute().SingleOrDefault();//malo by najst 1
                if (building == null)
                {
                    throw new NullReferenceException("Building service - BuildingByNameAndVillageId(...) building cant be null");
                }
                return building;
            }
        }
    }
}
