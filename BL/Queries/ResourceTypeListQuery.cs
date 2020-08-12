using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.AppInfrastructure;
using BL.DTOs.ResourceTypes;
using Riganti.Utils.Infrastructure.Core;
using DAL.Entities;
using AutoMapper.QueryableExtensions;

namespace BL.Queries
{
    public class ResourceTypeListQuery : AppQuery<ResourceTypeDTO>
    {
        public ResourceTypeListQuery(IUnitOfWorkProvider provider) : base(provider) { }

        protected override IQueryable<ResourceTypeDTO> GetQueryable()
        {
            IQueryable<ResourceType> query = Context.ResourceTypes;
            return query.ProjectTo<ResourceTypeDTO>();
        }
    }
}
