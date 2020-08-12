using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.Entities
{
    public class Unit : IEntity<int>
    {
        public int ID { get; set; }

        [Required]
        public virtual UnitType UnitType { get; set; }

        [Required]
        public virtual Village Village { get; set; }

        public int Count { get; set; } 
    }
}
