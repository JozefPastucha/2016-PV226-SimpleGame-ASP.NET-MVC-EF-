using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.AppInfrastructure;
using BL.DTOs.Players;
using Riganti.Utils.Infrastructure.Core;
using DAL.Entities;
using AutoMapper.QueryableExtensions;

namespace BL.Queries
{
    public class PlayerListQuery : AppQuery<PlayerDTO>
    {
        public PlayerListQuery(IUnitOfWorkProvider provider) : base(provider) { }

        protected override IQueryable<PlayerDTO> GetQueryable()
        {
            IQueryable<Player> query = Context.Players;
            return query.ProjectTo<PlayerDTO>();
        }
    }
}
