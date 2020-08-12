using System;
using System.Collections.Generic;
using System.Linq;
using BL.Repositories;
using DAL.Entities;
using BL.DTOs.Units;
using BL.Queries;
using BL.DTOs.Filters;

namespace BL.Services.Units
{
    public class UnitService : GameService, IUnitService
    {
        private readonly UnitRepository unitRepository;
        private readonly ProductRepository productRepository; //platba za trening
        private readonly UnitListQuery unitListQuery; //vypis units by village
        private readonly ProductListQuery productListQuery; //na zaplatenie najst product

        public UnitService(UnitRepository unitRepository, UnitTypeRepository unitTypeRepository, VillageRepository villageRepository, ProductRepository productRepository, ProductTypeRepository productTypeRepository, UnitListQuery unitListQuery, ProductListQuery productListQuery)
        {
            this.unitRepository = unitRepository;
            this.productRepository = productRepository;
            this.unitListQuery = unitListQuery;
            this.productListQuery = productListQuery;
        }

        public bool Train(int unitId, int amount)
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var unit = unitRepository.GetById(unitId, u => u.UnitType, u => u.Village);

                if (unit == null)
                {
                    throw new NullReferenceException("Unit service - Train(...) unit cant be null");
                }

                var splitted = unit.UnitType.Cost.Split(null);
                Product[] products = new Product[splitted.Count()];

                for (int i = 0; i < splitted.Count() / 2; ++i)
                {
                    productListQuery.Filter = new ProductFilter { VillageId = unit.Village.ID, ProductType = splitted[2 * i] };
                    products[i] = productRepository.GetById(productListQuery.Execute().SingleOrDefault().ID, p => p.ProductType, p => p.Village);
                    if (products[i] == null)
                    {
                        throw new NullReferenceException("Unit service - Prouce(...) product cant be null");
                    }
                    
                    if (products[i].Amount < Int32.Parse(splitted[2 * i + 1]) * amount)
                        return false; //skonci fciu ak neni dost niektoreho produktu na trening// return false?
                }

                for (int i = 0; i < splitted.Count() / 2; ++i)
                {
                    products[i].Amount -= Int32.Parse(splitted[2 * i + 1]) * amount;
                    productRepository.Update((products[i]));
                }
                
                unit.Count+= amount;
                unitRepository.Update(unit);
                uow.Commit();
                return true;
            }
        }

        public IEnumerable<UnitDTO> ListUnitsByVillage(int villageId)
        {
            using (UnitOfWorkProvider.Create())
            {
                unitListQuery.Filter = new UnitFilter { VillageId = villageId };
                return unitListQuery.Execute() ?? new List<UnitDTO>();
            }
        }      
    }
}
