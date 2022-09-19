using KundeApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KundeApp.DAL
{
    public class KundeRepository : IKundeRepository
    {

        private readonly KundeContext _db;

        public KundeRepository(KundeContext kundeDB)
        {
            _db = kundeDB;
        }

        public async Task<bool> Lagre(Kunde innKunde)
        {
            try
            {
                var nyKundeRad = new Kunder();
                nyKundeRad.Fornavn = innKunde.Fornavn;
                nyKundeRad.Etternavn = innKunde.Etternavn;
                nyKundeRad.Adresse = innKunde.Adresse;

                var sjekkPoststed = _db.Poststeder.Find(innKunde.Postnr);
                if (sjekkPoststed == null)
                {
                    var nyPoststedRad = new Poststeder();
                    nyPoststedRad.Postnr = innKunde.Postnr;
                    nyPoststedRad.Poststed = innKunde.Poststed;
                    nyKundeRad.Poststed = nyPoststedRad;
                }
                else
                {
                    nyKundeRad.Poststed = sjekkPoststed;
                }
                _db.Kunder.Add(nyKundeRad);
                await _db.SaveChangesAsync();
                return true;

            }
            catch
            {
                return false;
            }
        }
        public async Task<List<Kunde>> HentAlle()
        {
            try
            {
                List<Kunde> alleKunder = await _db.Kunder.Select(k => new Kunde
                {
                    Id = k.Id,
                    Fornavn = k.Fornavn,
                    Etternavn = k.Etternavn,
                    Adresse = k.Adresse,
                    Postnr = k.Poststed.Postnr,
                    Poststed = k.Poststed.Poststed
                }).ToListAsync();

                return alleKunder;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> Slett(int Id)
        {
            try
            {
                Kunder enKunde = await _db.Kunder.FindAsync(Id);
                _db.Kunder.Remove(enKunde);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Kunde> HentEn(int Id)
        {
            try
            {
                Kunder enKunde = await _db.Kunder.FindAsync(Id);
                var hentetKunde = new Kunde()
                {
                    Id = enKunde.Id,
                    Fornavn = enKunde.Fornavn,
                    Etternavn = enKunde.Etternavn,
                    Adresse = enKunde.Adresse,
                    Postnr = enKunde.Poststed.Postnr,
                    Poststed = enKunde.Poststed.Poststed
                };
                return hentetKunde;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> Endre(Kunde endreKunde)
        {
            try
            {
                Kunder enKunde = await _db.Kunder.FindAsync(endreKunde.Id);

                if (enKunde.Poststed.Postnr != endreKunde.Postnr)
                {
                    var sjekkPoststed = _db.Poststeder.Find(endreKunde.Postnr);
                    if (sjekkPoststed == null)
                    {
                        var nyPoststedRad = new Poststeder
                        {
                            Postnr = endreKunde.Postnr,
                            Poststed = endreKunde.Poststed
                        };
                        enKunde.Poststed = nyPoststedRad;
                    }
                    else
                    {
                        enKunde.Poststed = sjekkPoststed;
                    }
                }
                enKunde.Fornavn = endreKunde.Fornavn;
                enKunde.Etternavn = endreKunde.Etternavn;
                enKunde.Adresse = endreKunde.Adresse;
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

