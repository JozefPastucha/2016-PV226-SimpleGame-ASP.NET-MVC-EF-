using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.AppInfrastructure;
using BL.DTOs.Filters;
using BL.DTOs.Products;
using Riganti.Utils.Infrastructure.Core;
using DAL.Entities;
using AutoMapper.QueryableExtensions;


namespace BL.Queries
{
    public class ProductListQuery : AppQuery<ProductDTO>
    {
        public ProductFilter Filter { get; set; }

        public ProductListQuery(IUnitOfWorkProvider provider) : base(provider) { }

        protected override IQueryable<ProductDTO> GetQueryable()
        {
            IQueryable<Product> query = Context.Products;
            query = query.Where(product => product.Village.ID == Filter.VillageId);
               
            if (Filter.ProductType != null)//
                query = query.Where(product => product.ProductType.Name.Equals(Filter.ProductType));
            return query.ProjectTo<ProductDTO>();
        }
    }
}
