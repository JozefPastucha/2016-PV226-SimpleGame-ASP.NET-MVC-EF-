using System;
using System.Collections.Generic;
using System.Linq;
using BL.DTOs.Units;
using BL.Repositories;
using DAL.Entities;
using BL.Queries;
using BL.DTOs.Filters;
using BL.DTOs.UnitTypes;
using BL.DTOs.AdventureTypes;

namespace BL.Services.Adventures
{
    public class AdventureService : GameService, IAdventureService
    {
        private readonly AdventureTypeRepository adventureTypeRepository;
        private readonly AdventureListQuery adventureListQuery;
        private readonly UnitRepository unitRepository;
        private readonly UnitTypeListQuery unitTypeListQuery;
        private readonly ProductRepository productRepository;
        private readonly ProductListQuery productListQuery;
        private readonly ResourceRepository resourceRepository;
        private readonly ResourceListQuery resourceListQuery;
        private readonly AdventureRepository adventureRepository;

        public AdventureService(AdventureTypeRepository adventureTypeRepository, AdventureListQuery adventureListQuery, UnitRepository unitRepository, UnitTypeListQuery unitTypeListQuery, ProductRepository productRepository, ProductListQuery productListQuery, ResourceRepository resourceRepository, ResourceListQuery resourceListQuery, AdventureRepository adventureRepository)
        {
            this.adventureTypeRepository = adventureTypeRepository;
            this.adventureListQuery = adventureListQuery;
            this.unitRepository = unitRepository;
            this.unitTypeListQuery = unitTypeListQuery;
            this.productRepository = productRepository;
            this.productListQuery = productListQuery;
            this.resourceRepository = resourceRepository;
            this.resourceListQuery = resourceListQuery;
            this.adventureRepository = adventureRepository;
        }

        public bool CheckUnits(IEnumerable<UnitDTO> units)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                foreach (var item in units)
                {
                    var unit = unitRepository.GetById(item.ID);
                    if (item.Count > unit.Count) return false;
                }
            }
            return true;
        }

        public int numOfUnits (IEnumerable<UnitDTO> units)
        {
            int numOfUnits = 0;
            for (int i = 0; i < units.Count(); ++i)
            {
                if(units.ElementAt(i).Count < 0)
                {
                    return 0;
                }
                numOfUnits += units.ElementAt(i).Count;
            }
            return numOfUnits;
        }

        public bool CheckBread(IEnumerable<UnitDTO> units, AdventureType adventureType)
        {
            productListQuery.Filter = new ProductFilter { VillageId = units.First().VillageID, ProductType = "Bread" };
            Product bread;
            using (var uow = UnitOfWorkProvider.Create())
            {
                bread = productRepository.GetById(productListQuery.Execute().SingleOrDefault().ID, p => p.ProductType, p => p.Village);
            }

            int numbOfUnits = numOfUnits(units);

            if (bread.Amount < adventureType.BreadPerUnit * numbOfUnits)
            {
                return false;
            }
            bread.Amount -= adventureType.BreadPerUnit * numbOfUnits;

            using (var uow = UnitOfWorkProvider.Create())
            {
                productRepository.Update(bread);
                uow.Commit();
            }
            return true;
        }


        public UnitDTO FindTank(IEnumerable<UnitDTO> units)
        {
            return units.Where(u => u.Count > 0).First();   //mazem pri count 0, takze nebude prazdna units
        }


        public int GetMyUnitsDMG(IEnumerable<UnitDTO> units)
        {
            if (units == null)
            {
                throw new NullReferenceException("Unit service - GetMyUnitsDMG(...) units cant be null");    
            }

            int myUnitsDmg = 0;
            foreach (var item in units)
            {
                myUnitsDmg += item.Damage * item.Count;
            }
            return myUnitsDmg;
        }

        public void Kill(int unitId)    
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var unit = unitRepository.GetById(unitId, u => u.UnitType, u => u.Village);
                if (unit == null)
                {
                    throw new NullReferenceException("Unit service - Kill(...) unit cant be null"); //nestane sa
                }

                if (unit.Count == 0)
                {
                    throw new System.InvalidOperationException("Unit service - Kill(...) there are no units of this type in the village "); //nestane sa
                }
                unit.Count--;
                unitRepository.Update(unit);
                uow.Commit();
            }
        }

        public void Accomplished(int villageID, int adventureTypeId)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                adventureListQuery.Filter = new AdventureFilter { VillageId = villageID, AdventureTypeId = adventureTypeId };
                Adventure adventure = adventureRepository.GetById(adventureListQuery.Execute().SingleOrDefault().ID, a => a.AdventureType, a => a.Village);
                adventure.Accomplished = true;
                adventureRepository.Update(adventure);
                uow.Commit();
            }
        }

        public void GetReward(AdventureType adventureType, int villageID)
        {
            string[] splittedRes = adventureType.ResourcesReward.Split(null);

            //zisti ci je dost sur
            Resource[] resources = new Resource[splittedRes.Count() / 2];
            using (var uow = UnitOfWorkProvider.Create())
            {
                for (int i = 0; i < splittedRes.Count() / 2; ++i)
                {
                    resourceListQuery.Filter = new ResourceFilter { VillageId = villageID, ResourceType = splittedRes[2 * i] };
                    resources[i] = resourceRepository.GetById(resourceListQuery.Execute().SingleOrDefault().ID, r => r.ResourceType, r => r.Village);

                    if (resources[i] == null)
                    {
                        throw new NullReferenceException("Adventure Service - GetReward(...) resource cant be null");
                    }
                }

                for (int i = 0; i < splittedRes.Count() / 2; ++i)
                {
                    resources[i].Amount += Int32.Parse(splittedRes[2 * i + 1]);
                    resourceRepository.Update(resources[i]);
                }

                string[] splittedProd = adventureType.ProductsReward.Split(null);

                //zisti ci je dost sur
                Product[] products = new Product[splittedProd.Count() / 2];

                for (int i = 0; i < splittedProd.Count() / 2; ++i)
                {
                    productListQuery.Filter = new ProductFilter { VillageId = villageID, ProductType = splittedProd[2 * i] };
                    products[i] = productRepository.GetById(productListQuery.Execute().SingleOrDefault().ID, p => p.ProductType, p => p.Village);

                    if (products[i] == null)
                    {
                        throw new NullReferenceException("Adventure Service - GetReward(...) product cant be null");
                    }
                }

                for (int i = 0; i < splittedProd.Count() / 2; ++i)
                {
                    products[i].Amount += Int32.Parse(splittedProd[2 * i + 1]);
                    productRepository.Update(products[i]);
                }
                uow.Commit();
            }


        }
        public bool Adventure(IEnumerable<UnitDTO> units, int adventureTypeId)
        {
 
            if (units == null)
            {
                throw new NullReferenceException("Adventure service - Adventure(...) units cant be null");
            }

            if (!units.Any()) //nestane s k neni nejak blbo pl spravena
            {
                return false;
            }
            
            if (numOfUnits(units) <= 0)
            {
                return false;
            }

            units = units.Where(u => u.Count > 0);

            var villageID = units.First().VillageID;
            
            if (!CheckUnits(units))
            {
                return false;   //ak neposielam nikoho tak ok, lebo tolko mam
            }

            AdventureType adventureType;
            using (var uow = UnitOfWorkProvider.Create())
            {
                adventureType = adventureTypeRepository.GetById(adventureTypeId, a => a.Enemy);
            }

            if (!CheckBread(units, adventureType))
            {
                return false;
            }

            units = units.OrderByDescending(u => u.HP);//nebudu sa rovnat hp roznym jednotkam
            
            UnitDTO tank = FindTank(units);
           // if (tank == null) return false;//zmazat mozem myslim, lebo ani any, ani nm neni 0

            unitTypeListQuery.Filter = new UnitTypeFilter { Name = adventureType.Enemy.Name };

            UnitTypeDTO enemyUnit;

            using(var uow = UnitOfWorkProvider.Create())
            {
                enemyUnit = unitTypeListQuery.Execute().SingleOrDefault(); //meno bude jednoznacne //tak potm asi required ked musi existovat enemy Unit, hm
            }
               
            if (enemyUnit == null)
            {
                throw new NullReferenceException("Adventure service - Adventure(...) enemyUnit cant be null"); //spravim obsadenie bez enemies, takze to bude moyne toto_?
            }


            /*int enemyUnitDMG = enemyUnit.Damage;
            int enemyUnitHP = enemyUnit.HP;
            */
            var tankHP = tank.HP;       //tank.hp nemenim, potrebujem si pamatat kolko nastavit tankovi zase hp a dmg pri dalsom kole
            var tankDMG = tank.Damage;

            int myUnitsDmg = GetMyUnitsDMG(units);

            for (int i = 0; i < adventureType.NumberOfEnemies; ++i)
            {
                int enemyUnitDMG = enemyUnit.Damage;
                int enemyUnitHP = enemyUnit.HP;

                while (enemyUnitHP > 0)
                {
                    tankHP -= enemyUnitDMG;
                    enemyUnitHP -= myUnitsDmg;     //vratane dmgu tanka, on bojuje tiez

                    if (tankHP <= 0)
                    {
                        
                        Kill(tank.ID);
                        if (tank.Count > 1) //ak ich bolo pred kill viacej, cize ostava ten isty typ jednotky
                        {
                            tank.Count--;
                        }
                        else if (tank.Count == 1) //ak bol pred kill len 1, cize prechadzam na iny typ Unit
                        {
                            units = units.Where(u => u.ID != tank.ID);//
                            if (!units.Any())//vsetci dead, uz nemam v units ziadny typ jednotky
                            {
                                if (enemyUnitHP <= 0 && i == adventureType.NumberOfEnemies - 1) //aj oni dead
                                {
                                    Accomplished(villageID, adventureType.ID);
                                    GetReward(adventureType, villageID);
                                    return true;
                                }
                                else//oni prezili
                                {
                                    return false;
                                }
                            }
                            
                            tank = FindTank(units);
                        }
                        tankHP = tank.HP;       
                        tankDMG = tank.Damage;
                        myUnitsDmg = GetMyUnitsDMG(units);
                    }

                }
            }

            
            Accomplished(villageID, adventureType.ID);
            GetReward(adventureType, villageID);
            return true;
        }

        public IEnumerable<AdventureDTO> ListAdventures(int villageId)
        {
            using (UnitOfWorkProvider.Create())
            {
                adventureListQuery.Filter = new AdventureFilter { VillageId = villageId };
                return adventureListQuery.Execute();
            }
        }    
    }
}
