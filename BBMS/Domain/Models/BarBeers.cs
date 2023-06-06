using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Models
{
    public class BarBeers
    {

        public int BarId { get; set; }
        public int BeerId { get; set; }        
        public List<Beer> Beers { get; set; }
         

    }
}
