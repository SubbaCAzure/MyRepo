using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class BreweryBeers
    {
        public int BreweryId { get; set; }
        public int BeerId { get; set; }        
        public List<Beer> Beers { get; set; }
    }
}
