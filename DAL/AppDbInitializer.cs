using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DAL.Entities;
using DAL.Enums;

namespace DAL
{
    public class AppDbInitializer : DropCreateDatabaseAlways<AppDbContext>
    {
        public override void InitializeDatabase(AppDbContext context)
        {
            base.InitializeDatabase(context);

            var wood = new ResourceType { Name = "Wood" };
            var stone = new ResourceType { Name = "Stone" };
            var iron = new ResourceType { Name = "Iron" };
            var wheat = new ResourceType { Name = "Wheat" };
            var leather = new ResourceType { Name = "Leather" };

            context.ResourceTypes.Add(wood);
            context.ResourceTypes.Add(stone);
            context.ResourceTypes.Add(iron);
            context.ResourceTypes.Add(wheat);
            context.ResourceTypes.Add(leather);

            var timberCamp = new BuildingType { Name = "TimberCamp", ResourceType = wood, Cost = "Wood 45 Stone 25 ", ProdPerWorker = 1 };
            var quarry = new BuildingType { Name = "Quarry", ResourceType = stone, Cost = "Wood 45 Stone 25 ", ProdPerWorker = 1 };
            var ironMine = new BuildingType { Name = "IronMine", ResourceType = iron, Cost = "Wood 450 Stone 450 ", ProdPerWorker = 1 };
            var farm = new BuildingType { Name = "Farm", ResourceType = wheat, Cost = "Wood 450 Stone 450 ", ProdPerWorker = 2 };
            var hunterCabin = new BuildingType { Name = "HunterCabin", ResourceType = leather, Cost = "Wood 200 Stone 150 ", ProdPerWorker = 1 };

            context.SaveChanges();  //najskor sa vzdy ulozili do databazy entity, ktore mali nullable propertu, v tomto pripade ResourceType null

            var smithery = new BuildingType { Name = "Smithery", Cost = "Wood 1500 Stone 1500 Iron 1200" };
            var trainingCamp = new BuildingType { Name = "TrainingCamp", Cost = "Wood 1500 Stone 1050 Iron 1050" };
            var bakery = new BuildingType { Name = "Bakery", Cost = "Wood 1500 Stone 1500 Iron 750"};
            var tavern = new BuildingType { Name = "Tavern", Cost = "Wood 3000 Stone 3000 Iron 1500" };


            context.BuildingTypes.Add(timberCamp);
            context.BuildingTypes.Add(quarry);
            context.BuildingTypes.Add(ironMine);
            context.BuildingTypes.Add(farm);
            context.BuildingTypes.Add(bakery);

            context.BuildingTypes.Add(hunterCabin);

            context.BuildingTypes.Add(smithery);
            context.BuildingTypes.Add(trainingCamp);
            context.BuildingTypes.Add(tavern);

            var bow = new ProductType { Name = "Bow", Cost = "Wood 50 Leather 10", BuildingType = smithery };
            var ironSword = new ProductType { Name = "IronSword", Cost = "Wood 20 Iron 50", BuildingType = smithery };
            var polearm = new ProductType { Name = "Polearm", Cost = "Wood 40 Iron 30", BuildingType = smithery };
            var leatherArmor = new ProductType { Name = "LeatherArmor", Cost = "Leather 150", BuildingType = smithery };
            var mailArmor = new ProductType { Name = "MailArmor", Cost = "Iron 150", BuildingType = smithery };
            var horse = new ProductType { Name = "Horse", Cost = "Wheat 100 Leather 20", BuildingType = smithery };
            var bread = new ProductType { Name = "Bread", Cost = "Wheat 50", BuildingType = bakery };
            var waggon = new ProductType { Name = "Waggon", Cost = "Wood 10000 Stone 10000 Iron 5000 Leather 5000 Wheat 3000", BuildingType = smithery };

            context.ProductTypes.Add(bow);
            context.ProductTypes.Add(ironSword);
            context.ProductTypes.Add(polearm);
            context.ProductTypes.Add(leatherArmor);
            context.ProductTypes.Add(mailArmor);
            context.ProductTypes.Add(horse);
            context.ProductTypes.Add(bread);
            context.ProductTypes.Add(waggon);

            var archer = new UnitType { Name = "Archer", Cost = "Bow 1 LeatherArmor 1", Role = UnitRole.Friendly, Damage = 5, HP = 5 };
            var swordsman = new UnitType { Name = "Swordsman", Cost = "IronSword 1 MailArmor 1", Role = UnitRole.Friendly, Damage = 5, HP = 10 };
            var knight = new UnitType { Name = "Knight", Cost = "Polearm 1 MailArmor 1 Horse 1", Role = UnitRole.Friendly, Damage = 7, HP = 15 };
            var settler = new UnitType { Name = "Settler", Cost = "Waggon 1 LeatherArmor 1 IronSword 1", Role = UnitRole.Friendly, Damage = 5, HP = 5 };


            var goblin = new UnitType { Name = "Goblin", Role = UnitRole.Hostile, Damage = 4, HP = 5 };    //mozno by som ho chcel byt potom schopny aj najat
            //var ogre = new UnitType { Name = "Ogre", Role = UnitRole.Hostile };
            var wyvern = new UnitType { Name = "Wyvern", Role = UnitRole.Hostile, Damage = 14, HP = 250 };
            //var giantTroll = new UnitType { Name = "GiantTroll", Role = UnitRole.Hostile };
            var dragon = new UnitType { Name = "Dragon", Role = UnitRole.Hostile, Damage = 50, HP = 1000 };



            context.UnitTypes.Add(archer);
            context.UnitTypes.Add(swordsman);
            context.UnitTypes.Add(knight);
            context.UnitTypes.Add(settler);

            context.SaveChanges();  //najskor sa vzdy ulozili do databazy entity, ktore mali nullable propertu, v tomto pripade Cost null
                                    //co az tak tu by ani nevadilo, stacilo by pri pracis nimi iterovat od 3


            context.UnitTypes.Add(goblin);
            //context.UnitTypes.Add(ogre);
            context.UnitTypes.Add(wyvern);
            //context.UnitTypes.Add(giantTroll);
            context.UnitTypes.Add(dragon);

            var goblinsLair = new AdventureType { Name = "GoblinsLair", Enemy = goblin, NumberOfEnemies = 3, BreadPerUnit = 1, ProductsReward = "Bow 10 LeatherArmor 10", ResourcesReward = "Wood 1000 Stone 1000 Iron 1000" };
            var wyvernsNest = new AdventureType { Name = "WyvernsNest", Enemy = wyvern, NumberOfEnemies = 2, BreadPerUnit = 6, ProductsReward = "Bow 20 LeatherArmor 20 Ironsword 10 ", ResourcesReward = "Iron 1000 Leather 1000" };
            var dragonsLair = new AdventureType { Name = "DragonsLair", Enemy = dragon, NumberOfEnemies = 1, BreadPerUnit = 12, ProductsReward = "IronSword 100 MailArmor 100", ResourcesReward = "Iron 10000 Leather 10000" };


            context.AdventureTypes.Add(goblinsLair);
            context.AdventureTypes.Add(wyvernsNest);
            context.AdventureTypes.Add(dragonsLair);
            
            context.SaveChanges();
        }
    }
}
