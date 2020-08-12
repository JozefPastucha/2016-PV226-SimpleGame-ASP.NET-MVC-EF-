using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.AppInfrastructure;
using BL.DTOs.BuildingTypes;
using Riganti.Utils.Infrastructure.Core;
using DAL.Entities;
using AutoMapper.QueryableExtensions;

namespace BL.Queries
{
    public class BuildingTypeListQuery : AppQuery<BuildingTypeDTO>
    {
        public BuildingTypeListQuery(IUnitOfWorkProvider provider) : base(provider) { }

        protected override IQueryable<BuildingTypeDTO> GetQueryable()
        {
            IQueryable<BuildingType> query = Context.BuildingTypes;
            return query.ProjectTo<BuildingTypeDTO>();
        }
    }
}
