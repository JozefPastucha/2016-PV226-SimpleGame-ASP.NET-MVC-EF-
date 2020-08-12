using BL.DTOs.Village;
using BL.Repositories;
using DAL.Entities;
using DAL.Enums;
using BL.Queries;
using AutoMapper;
using System.Linq;
using BL.DTOs.Filters;
using System;
using Riganti.Utils.Infrastructure.Core;    //SortDirection
using System.Collections.Generic;

namespace BL.Services.Villages
{
    public class VillageService : GameService, IVillageService
    {
        public int VillagesPageSize => 9;

        private readonly PlayerRepository playerRepository;
        private readonly VillageRepository villageRepository;
        private readonly BuildingRepository buildingRepository;
        private readonly BuildingTypeRepository buildingTypeRepository; 
        private readonly UnitRepository unitRepository;
        private readonly ProductRepository productRepository;
        private readonly ProductTypeRepository productTypeRepository;   
        private readonly ResourceRepository resourceRepository;
        private readonly ResourceTypeRepository resourceTypeRepository; 
        private readonly AdventureRepository adventureRepository;
        private readonly AdventureTypeRepository adventureTypeRepository;

        private readonly BuildingTypeListQuery buildingTypeListQuery;
        private readonly ProductListQuery productListQuery;
        private readonly ProductTypeListQuery productTypeListQuery;
        private readonly ResourceTypeListQuery resourceTypeListQuery;
        private readonly UnitTypeListQuery unitTypeListQuery;
        private readonly UnitTypeRepository unitTypeRepository; 
        private readonly AdventureTypeListQuery adventureTypeListQuery;
        private readonly VillageListQuery villageListQuery;
        private readonly UnitListQuery unitListQuery;

        public VillageService(PlayerRepository playerRepository, VillageRepository villageRepository, BuildingRepository buildingRepository, BuildingTypeRepository buildingTypeRepository, UnitRepository unitRepository, UnitTypeRepository unitTypeRepository, ProductRepository productRepository, ProductTypeRepository productTypeRepository, ResourceRepository resourceRepository, ResourceTypeRepository resourceTypeRepository, BuildingListQuery buildingListQuery, BuildingTypeListQuery buildingTypeListQuery, ProductListQuery productListQuery, ProductTypeListQuery productTypeListQuery, ResourceTypeListQuery resourceTypeListQuery, UnitTypeListQuery unitTypeListQuery, VillageListQuery villageListQuery, AdventureRepository adventureRepository, AdventureTypeRepository adventureTypeRepository, AdventureTypeListQuery adventureTypeListQuery, UnitListQuery unitListQuery)
        {
            this.playerRepository = playerRepository;
            this.villageRepository = villageRepository;

            this.buildingRepository = buildingRepository;
            this.buildingTypeRepository = buildingTypeRepository;  

            this.unitRepository = unitRepository;
            this.unitTypeRepository = unitTypeRepository; 

            this.productRepository = productRepository;
            this.productTypeRepository = productTypeRepository; 

            this.resourceRepository = resourceRepository;
            this.resourceTypeRepository = resourceTypeRepository;

            this.adventureRepository = adventureRepository;
            this.adventureTypeRepository = adventureTypeRepository;
            

            this.buildingTypeListQuery = buildingTypeListQuery;

            this.productTypeListQuery = productTypeListQuery;
            this.productListQuery = productListQuery;           

            this.resourceTypeListQuery = resourceTypeListQuery;
            this.unitTypeListQuery = unitTypeListQuery;
            this.adventureTypeListQuery = adventureTypeListQuery;
            this.villageListQuery = villageListQuery;
            this.unitListQuery = unitListQuery;
        }

        //vytvori village, resources, buildings, products, units pre village
        //pre kazdy typ budovy budovu, pre kazdy typ res res atd

        public void CreateVillage(int playerID)
        {
            var village = new Village(); 

            using (var uow = UnitOfWorkProvider.Create())
            {
                var player = playerRepository.GetById(playerID, p => p.Villages, p=>p.Account);
                player.NumOfVillages++;
                
                village.Player = player;
                village.Huts = 1;
                village.AvailableWorkers = 4;
                if (player.Villages.Count() == 0)
                {
                    village.Number = 1;
                }
                else
                {
                    village.Number = player.Villages.Last().Number + 1;  
                }
                villageRepository.Insert(village);
                playerRepository.Update(player);
                uow.Commit();
            }

            using (var uow = UnitOfWorkProvider.Create())
            {
                var v = villageRepository.GetById(village.ID);

                var resourceTypesCount = resourceTypeListQuery.GetTotalRowCount();
                Resource[] resources = new Resource[resourceTypesCount];

                //takto, alebo i = 0  inicializovat, i++ inicializovat a for od 2 //opakovanie kodu, ci efektivita?
                for (int i = 0; i < resourceTypesCount; ++i)    
                {
                    resources[i] = new Resource();
                    resources[i].ResourceType = resourceTypeRepository.GetById(i + 1);

                    if (i == 0)
                    {
                        resources[i].Amount = 90;  
                    }

                    if (i == 1)
                    {
                        resources[i].Amount = 50;
                    }

                    resources[i].Village = v;
                    resourceRepository.Insert(resources[i]);
                }
                
                var buildingTypesCount = buildingTypeListQuery.GetTotalRowCount();
                var buildingTypes = buildingTypeRepository.GetByIds(Enumerable.Range(1, buildingTypesCount).ToArray()); // 2. Sposob na priradovanie type
                Building[] buildings = new Building[buildingTypesCount];

                for (int i = 0; i < buildingTypesCount; ++i) 
                {
                    buildings[i] = new Building();
                    buildings[i].BuildingType = buildingTypes[i];   //2. sposob
                    buildings[i].Village = v;
                    buildingRepository.Insert(buildings[i]);
                }

                
                var productTypesCount = productTypeListQuery.GetTotalRowCount();
                Product[] products = new Product[productTypesCount];

                for (int i = 0; i < productTypesCount; ++i)
                {
                    products[i] = new Product();
                    products[i].ProductType = productTypeRepository.GetById(i + 1); 
                    products[i].Village = v;
                    productRepository.Insert(products[i]);
                }


                var unitTypesCount = unitTypeListQuery.Execute().Where(u=>u.Role.Equals(UnitRole.Friendly)).Count();    //friendly/hostile units

                Unit[] units = new Unit[unitTypesCount];
                for (int i = 0; i < unitTypesCount; ++i)
                {
                    units[i] = new Unit();
                    units[i].UnitType = unitTypeRepository.GetById(i + 1);
                    units[i].Village = v;
                    unitRepository.Insert(units[i]);
                }

                var adventureTypesCount = adventureTypeListQuery.GetTotalRowCount();
                var adventureTypes = adventureTypeRepository.GetByIds(Enumerable.Range(1, adventureTypesCount).ToArray()); // 2. Sposob na priradovanie type
                Adventure[] adventures = new Adventure[adventureTypesCount];

                for (int i = 0; i < adventureTypesCount; ++i)
                {
                    adventures[i] = new Adventure();
                    adventures[i].AdventureType = adventureTypes[i];   //2. sposob
                    adventures[i].Village = v;
                    adventureRepository.Insert(adventures[i]);
                }

                uow.Commit();
            }
        }


        public VillageDTO GetVillage(int villageID)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var village = villageRepository.GetById(villageID);
                return village != null ? Mapper.Map<VillageDTO>(village) : null;
            }
        }

        public bool SettleNewVillage(int villageId)
        {
            Unit unit;
            int playerId;
            using (var uow = UnitOfWorkProvider.Create())
            {
                unitListQuery.Filter = new UnitFilter { VillageId = villageId, UnitType = "Settler" };
                unit = unitRepository.GetById(unitListQuery.Execute().SingleOrDefault().ID, u => u.UnitType, u => u.Village);

                if (unit.Count < 1)
                {
                    return false;
                }
                
                unit.Count--;
                unitRepository.Update(unit);
                playerId = unit.Village.Player.ID;
                uow.Commit();
            }
            CreateVillage(playerId);
            return true;
        }

        public VillageListQueryResultDTO ListVillagesByPlayerPaging(int playerId, int requiredPage = 1)
        {
            using (UnitOfWorkProvider.Create())
            {
                var query = villageListQuery;
                query.Filter = new PlayerVillageFilter { PlayerId = playerId };
                query.Skip = Math.Max(0, requiredPage - 1) * VillagesPageSize;
                query.Take = VillagesPageSize;

                var sortOrder = SortDirection.Ascending;
                query.AddSortCriteria("ID", sortOrder);

                return new VillageListQueryResultDTO
                {
                    RequestedPage = requiredPage,
                    TotalResultCount = query.GetTotalRowCount(),
                    ResultsPage = query.Execute(),
                    Filter = query.Filter
                };
            }
        }

        public IEnumerable<VillageDTO> ListVillagesByPlayer(int playerId)
        {
            using (UnitOfWorkProvider.Create())
            {
                var query = villageListQuery;
                query.Filter = new PlayerVillageFilter { PlayerId = playerId };
                return villageListQuery.Execute();
            }
        }
    }
}