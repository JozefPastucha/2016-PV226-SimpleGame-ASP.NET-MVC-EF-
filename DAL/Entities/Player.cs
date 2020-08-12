using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.Entities
{
    public class Player : IEntity<int>
    {
        public int ID { get; set; }

        [Required]
        public virtual UserAccount Account { get; set; }

        public virtual List<Village> Villages { get; set; }

        public int NumOfVillages { get; set; }
    }
}
