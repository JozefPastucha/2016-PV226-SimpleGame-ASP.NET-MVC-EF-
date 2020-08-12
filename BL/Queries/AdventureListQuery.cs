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
using BL.DTOs.Filters;

namespace BL.Queries
{
    public class AdventureListQuery : AppQuery<AdventureDTO>
    {
        public AdventureFilter Filter { get; set; }
        public AdventureListQuery(IUnitOfWorkProvider provider) : base(provider) { }

        protected override IQueryable<AdventureDTO> GetQueryable()
        {
            IQueryable<Adventure> query;
            if (Filter.AdventureTypeId == 0)    //dotaz na vsetky pristupne adventures
            {
                Adventure adventure = Context.Adventures.Where(a => !a.Accomplished && a.Village.ID == Filter.VillageId).FirstOrDefault();
                if (adventure == null) //uz su vsetky accomplished
                {
                    query = Context.Adventures.Where(a => a.Village.ID == Filter.VillageId);
                }
                else
                {
                    query = Context.Adventures.Where(a => a.AdventureType.ID == 1 || a.ID == adventure.ID || a.Accomplished);
                    query = query.Where(a => a.Village.ID == Filter.VillageId);
                }
            }
            else //dotaz na konkretnu adventure
            {
                query = Context.Adventures.Where(a => a.AdventureType.ID == Filter.AdventureTypeId);
                query = query.Where(a => a.Village.ID == Filter.VillageId);
            }

            return query.ProjectTo<AdventureDTO>();
        }
    }
}
