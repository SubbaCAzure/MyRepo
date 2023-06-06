using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Beer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        
        [Column(TypeName = "decimal(8,2)")]
        public decimal PercentageAlcoholByVolume { get; set; }

        //public ICollection<Bar> Bars { get; set; }
    }
}
