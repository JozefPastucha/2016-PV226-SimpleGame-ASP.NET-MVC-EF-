using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.AppInfrastructure;
using BL.DTOs.UnitTypes;
using Riganti.Utils.Infrastructure.Core;
using DAL.Entities;
using AutoMapper.QueryableExtensions;
using BL.DTOs.Filters;

namespace BL.Queries
{
    public class UnitTypeListQuery : AppQuery<UnitTypeDTO>
    {
        public UnitTypeFilter Filter { get; set; }

        public UnitTypeListQuery(IUnitOfWorkProvider provider) : base(provider) { }

        protected override IQueryable<UnitTypeDTO> GetQueryable()
        {

            IQueryable<UnitType> query = Context.UnitTypes;
            
            if (Filter != null)
            {
                query = query.Where(u => u.Name.Equals(Filter.Name));
            }
            return query.ProjectTo<UnitTypeDTO>();

        }
    }
}
