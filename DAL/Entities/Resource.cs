using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.Entities
{
    public class Resource : IEntity<int>
    {
        public int ID { get; set; }

        public int Amount { get; set; }

        [Required]
        public virtual ResourceType ResourceType { get; set; }

        [Required]
        public virtual Village Village { get; set; }
    }
}
