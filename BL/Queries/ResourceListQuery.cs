using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.AppInfrastructure;
using BL.DTOs.Filters;
using BL.DTOs.Resources;
using Riganti.Utils.Infrastructure.Core;
using DAL.Entities;
using AutoMapper.QueryableExtensions;

namespace BL.Queries
{
    public class ResourceListQuery : AppQuery<ResourceDTO>
    {
        public ResourceFilter Filter { get; set; }

        public ResourceListQuery(IUnitOfWorkProvider provider) : base(provider) { }

        protected override IQueryable<ResourceDTO> GetQueryable()
        {
            IQueryable<Resource> query = Context.Resources;
            query = query.Where(resource => resource.Village.ID == Filter.VillageId);
            if (Filter.ResourceType != null)
                query = query.Where(resource => resource.ResourceType.Name == Filter.ResourceType);
            return query.ProjectTo<ResourceDTO>();
        }
    }
}
