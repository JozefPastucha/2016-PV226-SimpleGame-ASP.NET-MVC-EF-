using System;
using System.Collections.Generic;
using DAL;
using DAL.Entities;
using BL.DTOs.Players;
using BL.DTOs.Resources;
using BL.DTOs.Buildings;
using BL.DTOs.Products;
using BL.DTOs.Units;
using BL.Services.Players;
using BL.Services.Villages;
using BL.Services.Resources;
using BL.Services.Buildings;
using BL.Services.Products;
using BL.Services.Units;
using BL.Services.Adventures;
using Castle.Windsor;
using BL.Bootstrap;

namespace ConsoleApplication1
{
    public class Program
    {
        private static IPlayerService playerService;
        private static IVillageService villageService;
        private static IResourceService resourceService;
        private static IBuildingService buildingService;
        private static IProductService productService;
        private static IUnitService unitService;
        private static IAdventureService adventureService;



        private static readonly IWindsorContainer Container = new WindsorContainer();

        private static void InitializeWindsorContainerAndMapper()
        {
            Container.Install(new BussinessLayerInstaller());

            MappingInit.ConfigureMapping();
        }

        private static void ListResourcesByVillage(int villageId)
        {
            resourceService = Container.Resolve<IResourceService>();
            IEnumerable<ResourceDTO> v1Resouces = resourceService.ListResourcesByVillage(1);
            foreach (var item in v1Resouces)
            {
                Console.WriteLine("ID: " + item.ID);
                Console.WriteLine("ResourceType: " + item.ResourceType);
                Console.WriteLine("Amount: " + item.Amount);
                Console.WriteLine();
            }
        }

        private static void ListBuildingsByVillage(int villageId)
        {
            buildingService = Container.Resolve<IBuildingService>();
            IEnumerable<BuildingDTO> v1Buildings = buildingService.ListBuildingsByVillage(1);
            foreach (var item in v1Buildings)
            {
                Console.WriteLine("ID: " + item.ID);
                Console.WriteLine("BuildingType: " + item.BuildingType);
                Console.WriteLine("Cost: " + item.Cost);
                Console.WriteLine("Workers assigned: " + item.WorkersAssigned);
                Console.WriteLine();
            }
        }

        private static void ListProductsByVillage(int villageId)
        {
            productService = Container.Resolve<IProductService>();
            IEnumerable<ProductDTO> v1Buildings = productService.ListProductsByVillage(1);
            foreach (var item in v1Buildings)
            {
                Console.WriteLine("ID: " + item.ID);
                Console.WriteLine("ProductType: " + item.ProductType);
                Console.WriteLine("Cost: " + item.Cost);
                Console.WriteLine("Amount: " + item.Amount);
                Console.WriteLine();
            }
        }

        private static void ListUnitsByVillage(int villageId)
        {
            unitService = Container.Resolve<IUnitService>();
            IEnumerable<UnitDTO> v1units = unitService.ListUnitsByVillage(1);
            foreach (var item in v1units)
            {
                Console.WriteLine("ID: " + item.ID);
                Console.WriteLine("UnitType: " + item.UnitType);
                Console.WriteLine("VillageID: " + item.VillageID);
                Console.WriteLine("Count: " + item.Count);
                Console.WriteLine();
            }
        }

        private static void TestCreatePlayer()
        {
            playerService = Container.Resolve<IPlayerService>();
            PlayerDTO newPlayer = new PlayerDTO();  
            
            //playerService.CreatePlayer(newPlayer);
            var firstPlayer = playerService.GetPlayer(1);
            Console.WriteLine(firstPlayer != null ? "PlayerService - Test01 - OK" : "PlayerService - Test01 - FAIL");
        }

        private static void TestCreateVillage(int playerId)
        {
            villageService = Container.Resolve<IVillageService>();
            villageService.CreateVillage(playerId);
            var firstVillage = villageService.GetVillage(1);
            Console.WriteLine(firstVillage != null ? "VillageService - Test01 - OK" : "VillageService - Test01 - FAIL");
            ListResourcesByVillage(1);
            ListBuildingsByVillage(1);
            ListProductsByVillage(1);
            ListUnitsByVillage(1);
        }

        private static void TestBuildHut(int villageId)
        {
            buildingService.BuildHut(villageId);
            int wood;
            int stone;

            using (var db = new AppDbContext())
            {
                wood = db.Resources.Find(1).Amount;
                stone = db.Resources.Find(2).Amount;
            }
            //wood = 90, stone = 94     
            Console.WriteLine(wood == 90 && stone == 94 ? "BuildingService - Test01 - OK" : "BuildingService - Test01 - FAIL");
        }

        private static void TestBuildTimberCamp(int villageId)
        {
            buildingService.Build(1);
            int wood;
            int stone;

            using (var db = new AppDbContext())
            {
                wood = db.Resources.Find(1).Amount;
                stone = db.Resources.Find(2).Amount;
            }
            // wood = 80, stone = 84
            Console.WriteLine(wood == 80 && stone == 84 ? "BuildingService - Test02 - OK" : "BuildingService - Test02 - FAIL");
        }

        private static void TestAddWorkers(int buildingId)
        {
            buildingService.AddWorkers(buildingId, 35); //privelke cislo
            int workersAssignedInTC;
            int freeWorkers;
            Building building;
            using (var db = new AppDbContext())
            {
                building = db.Buildings.Find(buildingId);
                workersAssignedInTC = building.WorkersAssigned;
                freeWorkers = db.Villages.Find(building.Village.ID).FreeWorkers;
            }
            Console.WriteLine(workersAssignedInTC == 0 && freeWorkers == 10 ? "BuildingService - Test03 - OK" : "BuildingService - Test03 - FAIL");

            buildingService.AddWorkers(buildingId, 5);  //OK
            using (var db = new AppDbContext())
            {
                workersAssignedInTC = db.Buildings.Find(buildingId).WorkersAssigned;
                freeWorkers = db.Villages.Find(building.Village.ID).FreeWorkers;
            }
            Console.WriteLine(workersAssignedInTC == 5 && freeWorkers == 5 ? "BuildingService - Test04 - OK" : "BuildingService - Test04 - FAIL");
        }

        private static void TestRemoveWorkers(int buildingId)
        {
            buildingService.RemoveWorkers(buildingId, 35); //privelke cislo
            int workersAssignedInTC;
            int freeWorkers;
            Building building;

            using (var db = new AppDbContext())
            {
                building = db.Buildings.Find(buildingId);
                workersAssignedInTC = db.Buildings.Find(buildingId).WorkersAssigned;
                freeWorkers = db.Villages.Find(building.Village.ID).FreeWorkers;
            }
            Console.WriteLine(workersAssignedInTC == 5 && freeWorkers == 5 ? "BuildingService - Test05 - OK" : "BuildingService - Test05 - FAIL");

            buildingService.RemoveWorkers(buildingId, 2); //ok
            using (var db = new AppDbContext())
            {
                workersAssignedInTC = db.Buildings.Find(buildingId).WorkersAssigned;
                freeWorkers = db.Villages.Find(building.Village.ID).FreeWorkers;
            }
            Console.WriteLine(workersAssignedInTC == 3 && freeWorkers == 7 ? "BuildingService - Test06 - OK" : "BuildingService - Test06 - FAIL");
        }

        private static void TestProduceBowAndLeatherArmor()
        {
            productService.Produce(1);  //bow //stoji 10 dreva, 2 leather
            int bowsAmount;
            int leatherArmorAmount;

            int wood;
            int leather;

            using (var db = new AppDbContext())
            {
                bowsAmount = db.Products.Find(1).Amount;
                wood = db.Resources.Find(1).Amount;
                leather = db.Resources.Find(4).Amount;
            }
            Console.WriteLine(bowsAmount == 1 && wood == 70 && leather == 98 ? "ProductService - Test04 - OK" : "ProductService - Test01 - FAIL");

            productService.Produce(4); //leatherArmor // stoji 10 leather
            
            using (var db = new AppDbContext())
            {
                leatherArmorAmount = db.Products.Find(4).Amount;
                leather = db.Resources.Find(4).Amount;
            }

            Console.WriteLine(leatherArmorAmount == 1 && leather == 88 ? "ProductService - Test02 - OK" : "ProductService - Test02 - FAIL");
        }

        private static void TestTrainArcher()
        {
            unitService.Train(1);   //1 bow, 1 leatherArmor
            int bowsAmount;
            int leatherArmorAmount;
            int archersCount;
            using (var db = new AppDbContext())
            {
                bowsAmount = db.Products.Find(1).Amount;
                leatherArmorAmount = db.Products.Find(4).Amount;
                archersCount = db.Units.Find(1).Count;   
            }
            Console.WriteLine(bowsAmount == 0 && leatherArmorAmount == 0 && archersCount == 1 ? "UnitService - Test01 - OK" : "UnitService - Test01 - FAIL");
        }
        
        private static void TestKillArcher()
        {
            adventureService = Container.Resolve<IAdventureService>();
            adventureService.Kill(1);
            int archersCount;
            using (var db = new AppDbContext())
            {
                archersCount = db.Units.Find(1).Count;
            }
            Console.WriteLine(archersCount == 0  ? "UnitService - Test02 - OK" : "UnitService - Test02 - FAIL");
        }


        /*private static List<AdventuresUnitDTO> InitUnits()
        {
            List<AdventuresUnitDTO> units = new List<AdventuresUnitDTO>();
            AdventuresUnitDTO unit = new AdventuresUnitDTO { Count = 2, Damage = 5, HP = 5, ID = 1, UnitType = "Archer" };

            units.Add(unit);
            return units;
        }
        private static void  TestCheckForUnits()
        {
            var units = InitUnits();
            bool result = adventureService.CheckUnits(units);
            Console.WriteLine(result == false ? "AdventureService - Test01 - OK" : "AdventureService - Test01 - FAIL");

            productService.Produce(1);
            productService.Produce(4);
            productService.Produce(1);
            productService.Produce(4);

            unitService.Train(1);
            unitService.Train(1);

            result = adventureService.CheckUnits(units);
            Console.WriteLine(result == true ? "AdventureService - Test02 - OK" : "AdventureService - Test02 - FAIL");
        }

        private static void TestFindTank()
        {
            var units = InitUnits();
            
            var tank = adventureService.FindTank(units);
            Console.WriteLine(tank.ID == 1 ? "AdventureService - Test03 - OK" : "AdventureService - Test03 - FAIL");

            //findTank sa pozuziva po CheckUnits, cize netreba testovat nekonzistenty stav
        }

        private static void TestGetMyUnitsDMG()
        {
            var units = InitUnits();

            int dmg = adventureService.GetMyUnitsDMG(units);

            Console.WriteLine(dmg == 10 ? "AdventureService - Test04 - OK" : "AdventureService - Test04 - FAIL");
            units = new List<AdventuresUnitDTO>();
            dmg = adventureService.GetMyUnitsDMG(units);
            Console.WriteLine(dmg == 0 ? "AdventureService - Test05 - OK" : "AdventureService - Test05 - FAIL");
        }

        /*private static void TestAdventure()
        {
            var units = InitUnits();
            //goblins 5 dmg 5hp, archeres 5 dmg 5hp     //goblin zautoci 1archer mrtvy, archers zautocia, on mrtvy
            adventureService.Adventure(units, "Goblin", 2);

            int archersCount;
            using (var db = new AppDbContext())
            {
                archersCount = db.Units.Find(1).Count;
            }

            Console.WriteLine(archersCount == 1 ? "AdventureService - Test06 - OK" : "AdventureService - Test06 - FAIL");
        }*/

        /*public static void TestAddResources()
        {
            TimeSpan timeSpan = new TimeSpan(0, 0, 10);
            resourceService.AddResources(timeSpan, 1);
            int wood;
            using (var db = new AppDbContext())
            {
                wood = db.Resources.Find(1).Amount;
            }
            Console.WriteLine(wood == 200 ? "ResourceService - Test01 - OK" : "ResourceService - Test01 - FAIL");
        }*/



        static void Main(string[] args)
        {
            InitializeWindsorContainerAndMapper();
           
            TestCreatePlayer();
            TestCreateVillage(1);
            TestBuildHut(1);
            TestBuildTimberCamp(1);
            TestAddWorkers(1);
            TestRemoveWorkers(1);
            TestProduceBowAndLeatherArmor();
            TestTrainArcher();
            TestKillArcher();

            /*TestCheckForUnits();
            TestFindTank();
            TestGetMyUnitsDMG();
            
            TestAdventure();

            TestAddResources();*/
        }
    }
}

