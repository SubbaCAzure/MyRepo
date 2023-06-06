using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Bar
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }

        //public ICollection<Beer> Beers { get; set; }
    }
}
