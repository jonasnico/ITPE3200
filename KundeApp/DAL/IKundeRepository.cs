using KundeApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KundeApp.DAL
{
    public interface IKundeRepository
    {

        Task<bool> Lagre(Kunde innKunde);
        Task<List<Kunde>> HentAlle();
        Task<bool> Slett(int Id);
        Task<Kunde> HentEn(int Id);
        Task<bool> Endre(Kunde endreKunde);
    }
}
