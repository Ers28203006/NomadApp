using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NomadApp.Models
{
    public class SubscriptionRegistration
    {
        public int Id { get; set; }
        public int? ClientId { get; set; }
        public virtual Client Client { get; set; }
        public int? SubscriptionId { get; set; }
        public virtual Subscription Subscription { get; set; }
    }
}
