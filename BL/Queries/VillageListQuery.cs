using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.AppInfrastructure;
using BL.DTOs.Filters;
using BL.DTOs.Village;
using Riganti.Utils.Infrastructure.Core;
using DAL.Entities;
using AutoMapper.QueryableExtensions;   //projectTo

namespace BL.Queries
{
    public class VillageListQuery : AppQuery<VillageDTO>
    {
        public PlayerVillageFilter Filter { get; set; }

        public VillageListQuery(IUnitOfWorkProvider provider) : base(provider) { }

        protected override IQueryable<VillageDTO> GetQueryable()
        {
            IQueryable<Village> query = Context.Villages;
            query = query.Where(village => village.Player.ID == Filter.PlayerId);
            return query.ProjectTo<VillageDTO>();
        }
    }
}
