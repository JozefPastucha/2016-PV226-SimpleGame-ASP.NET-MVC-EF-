using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.Entities
{
    public class BuildingType : IEntity<int>
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [Required]
        public string Cost { get; set; }

        public int ProdPerWorker { get; set; }

        public virtual ResourceType ResourceType { get; set; }
    }
}
