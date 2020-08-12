using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.Entities
{
    public class AdventureType : IEntity<int>
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [Required]
        public virtual UnitType Enemy { get; set; }

        public int NumberOfEnemies { get; set; }
        
        public int BreadPerUnit { get; set; }

        [Required]
        public string ResourcesReward { get; set; }

        [Required]
        public string ProductsReward { get; set; }
    }
}
