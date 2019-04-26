using NomadApp.Models;
using System.Collections.Generic;
using System.Data.Entity;

namespace NomadApp.DataAccess
{
    internal class DataInitializer : CreateDatabaseIfNotExists<NomadsContext>
    {
        protected override void Seed(NomadsContext context)
        {
            context.Subscriptions.AddRange
            (
                new List<Subscription>()
                {
                    new Subscription
                    {
                       Name="Nomad",
                       Price=100,
                       Period=12
                    },

                    new Subscription
                    {
                       Name="Nomad",
                       Price=150,
                       Period=24
                    },

                    new Subscription
                    {
                      Name="Nomad",
                       Price=200,
                       Period=36
                    }
                }
            );
        }
    }
}