using System;
using System.Collections.Generic;
using System.Linq;
using BL.Repositories;
using DAL.Entities;
using BL.DTOs.Products;
using BL.Queries;
using BL.DTOs.Filters;


namespace BL.Services.Products
{
    public class ProductService : GameService, IProductService
    {
        private readonly ProductRepository productRepository;
        private readonly ResourceRepository resourceRepository; //platba
        private readonly ProductListQuery productListQuery;  //list
        private readonly ResourceListQuery resourceListQuery; 
        private readonly BuildingListQuery buildingListQuery;

        public ProductService(ProductRepository productRepository, ProductTypeRepository productTypeRepository, ResourceRepository resourceRepository, ResourceTypeRepository resourceTypeRepository, VillageRepository villageRepository, ProductListQuery productListQuery, ResourceListQuery resourceListQuery, BuildingListQuery buildingListQuery)
        {
            this.productRepository = productRepository;
            this.resourceRepository = resourceRepository;
            this.productListQuery = productListQuery;
            this.resourceListQuery = resourceListQuery;
            this.buildingListQuery = buildingListQuery;
        }

        public bool Produce(int productId, int amount) 
        {
            using (var uow = UnitOfWorkProvider.Create())
            {
                var product = productRepository.GetById(productId, p => p.ProductType, p=> p.Village);
                if (product == null)
                {
                    throw new NullReferenceException("Product service - Produce(...) product cant be null");
                }

                string[] splitted = product.ProductType.Cost.Split(null);

                Resource[] resources = new Resource[splitted.Count() / 2];

                for (int i = 0; i < splitted.Count() / 2; ++i)
                {
                    resourceListQuery.Filter = new ResourceFilter{ VillageId = product.Village.ID, ResourceType = splitted[2 * i] };
                    resources[i] = resourceRepository.GetById(resourceListQuery.Execute().SingleOrDefault().ID, r => r.ResourceType, r=> r.Village);
                    if (resources[i] == null)
                    {
                        throw new NullReferenceException("Product service - Produce(...) resource cant be null");
                    }
                    if (resources[i].Amount < Int32.Parse(splitted[2 * i + 1]) * amount)
                        return false; //skonci fciu ak neni dost niektorej sur
                }

                for (int i = 0; i < splitted.Count() / 2; i++)
                {
                    resources[i].Amount -= Int32.Parse(splitted[2 * i + 1]) * amount;
                    resourceRepository.Update(resources[i]);
                }
                product.Amount+= amount;
                
                productRepository.Update(product);
                uow.Commit();
                return true;
            }
        }

        public IEnumerable<ProductDTO> ListProductsByVillage(int villageId)
        {
            using (UnitOfWorkProvider.Create())
            {
                productListQuery.Filter = new ProductFilter { VillageId = villageId };
                var products =  productListQuery.Execute() ?? new List<ProductDTO>();
                for (int i = 0; i < products.Count; ++i)
                {
                    buildingListQuery.Filter = new BuildingFilter { VillageId = villageId, BuildingType = products[i].BuildingType };
                    if (!buildingListQuery.Execute().SingleOrDefault().Built)//odstrani tie, ktorych vyrobna budova este neexistuje
                    {
                        products.Remove(products[i]);
                        --i;
                    }    
                 }
                return products; 
            }
        }
    }
}
