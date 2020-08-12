using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.AppInfrastructure;
using BL.DTOs.Filters;
using BL.DTOs.Buildings;
using Riganti.Utils.Infrastructure.Core;
using DAL.Entities;
using AutoMapper.QueryableExtensions;

namespace BL.Queries
{
    public class BuildingListQuery : AppQuery<BuildingDTO>
    {
        public BuildingFilter Filter { get; set; }

        public BuildingListQuery(IUnitOfWorkProvider provider) : base(provider) { }

        protected override IQueryable<BuildingDTO> GetQueryable()
        {
            IQueryable<Building> query = Context.Buildings;
            query = query.Where(building => building.Village.ID == Filter.VillageId);
            if (Filter.WorkersAssigned)
            {
                query = query.Where(building => building.WorkersAssigned != 0);
            }

            if (Filter.BuildingType != null)
            {
                query = query.Where(building => building.BuildingType.Name.Equals(Filter.BuildingType));
            }
            return query.ProjectTo<BuildingDTO>();
        }
    }
}
