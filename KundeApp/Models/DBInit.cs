using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KundeApp.Models
{
    public class DBInit
    {

        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<KundeContext>();

                // må slette og opprette databasen hver gang når den skal initieres (seed'es)
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var poststed1 = new Poststeder { Postnr = "0672", Poststed = "Oslo" };
                var poststed2 = new Poststeder { Postnr = "1370", Poststed = "Asker" };
                var poststed3 = new Poststeder { Postnr = "0195", Poststed = "Oslo" };

                var kunde1 = new Kunder
                {
                    Fornavn = "Jonas",
                    Etternavn = "Nicolaysen",
                    Adresse = "Hellerudveien 52",
                    Poststed = poststed1
                };
                var kunde2 = new Kunder { Fornavn = "Haakon", Etternavn = "Magnus", Adresse = "Skaugum 1337", Poststed = poststed2 };

                var kunde3 = new Kunder { Fornavn = "Aksel", Etternavn = "Holm Jensen", Adresse = "Turbinveien 5", Poststed = poststed3 };

                context.Kunder.Add(kunde1);
                context.Kunder.Add(kunde2);
                context.Kunder.Add(kunde3);

                context.SaveChanges();




            }
        }
    }
}
