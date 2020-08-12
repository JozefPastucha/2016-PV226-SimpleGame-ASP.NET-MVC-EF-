using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.AppInfrastructure;
using BL.DTOs.Filters;
using BL.DTOs.Units;
using Riganti.Utils.Infrastructure.Core;
using DAL.Entities;
using AutoMapper.QueryableExtensions;

namespace BL.Queries
{
    public class UnitListQuery : AppQuery<UnitDTO>
    {
        public UnitFilter Filter { get; set; }

        public UnitListQuery(IUnitOfWorkProvider provider) : base(provider) { }

        protected override IQueryable<UnitDTO> GetQueryable()
        {
            IQueryable<Unit> query = Context.Units;
            if (Filter.UnitType == null)
            {
                query = query.Where(unit => unit.Village.ID == Filter.VillageId);   //zobrazenie units hracovi
            }
            else
            {
                query = query.Where(unit => unit.Village.ID == Filter.VillageId && unit.UnitType.Name.Equals(Filter.UnitType));//najdenie settlera, pri vytvarani dediny
            }
                return query.ProjectTo<UnitDTO>();
        }
    }
}
