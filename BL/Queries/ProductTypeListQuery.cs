using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.AppInfrastructure;
using BL.DTOs.ProductTypes;
using Riganti.Utils.Infrastructure.Core;
using DAL.Entities;
using AutoMapper.QueryableExtensions;

namespace BL.Queries
{
    public class ProductTypeListQuery : AppQuery<ProductTypeDTO>
    {
        public ProductTypeListQuery(IUnitOfWorkProvider provider) : base(provider) { }

        protected override IQueryable<ProductTypeDTO> GetQueryable()
        {
            IQueryable<ProductType> query = Context.ProductTypes;
            return query.ProjectTo<ProductTypeDTO>();
        }
    }
}
