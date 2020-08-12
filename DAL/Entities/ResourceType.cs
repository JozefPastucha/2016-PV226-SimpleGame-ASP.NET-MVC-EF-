using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.Entities
{
    public class ResourceType : IEntity<int>
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
