using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.AppInfrastructure;
using BL.DTOs.AdventureTypes;
using Riganti.Utils.Infrastructure.Core;
using DAL.Entities;
using AutoMapper.QueryableExtensions;

namespace BL.Queries
{
    public class AdventureTypeListQuery : AppQuery<AdventureTypeDTO>
    {
        public AdventureTypeListQuery(IUnitOfWorkProvider provider) : base(provider) { }

        protected override IQueryable<AdventureTypeDTO> GetQueryable()
        {
            IQueryable<AdventureType> query = Context.AdventureTypes;
            return query.ProjectTo<AdventureTypeDTO>();
        }
    }
}