using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NomadApp.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public string Name{ get; set; }
        public double Price { get; set; }
        public int Period { get; set; }
    }
}
