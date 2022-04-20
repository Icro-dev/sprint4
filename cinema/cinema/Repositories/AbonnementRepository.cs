using cinema.Data;
using cinema.Models;
using Microsoft.EntityFrameworkCore;

namespace cinema.Repositories;

    public class AbonnementRepository : IAbonnementRepository
    {

        private readonly CinemaContext _context;

        public AbonnementRepository(CinemaContext cinemaContext)
        {
            _context = cinemaContext;
        }

        public async Task<List<Abonnement>> ListOfAllAbonnements()
        {
            return await _context.Abonnement.ToListAsync();
        }

  /*      public Task<Abonnement?> FindAbonnementByName(string username)
        {
        *//*return await _context.Abonnement.FirstOrDefaultAsync(a => a.AbboName == username);*//*
        return (Task<Abonnement?>)_context.Abonnement.Where(a => a.AbboName == username);


        }*/

    public Abonnement AbonnementByName(string username)
    {
        var result = _context.Abonnement.ToList().FindAll(ab => FindAbonement(ab, username));
        if(result == null)
        {
            var emptyAbonnement = new Abonnement();
            return emptyAbonnement;
        }
        if(result.Count > 1)
        {
            var emptyAbonnement = new Abonnement();
            return emptyAbonnement;
        }    
        else 
        {
            Abonnement abo = new Abonnement(); 
            foreach (var item in result)
            {
                abo.AbboName = item.AbboName;
                abo.AbboQR = item.AbboQR;
                abo.Expired = item.Expired;
                abo.ExpireDate = item.ExpireDate;
                abo.StartDate = item.StartDate;
                    
            }
            return abo;
        }
    }

    private static bool FindAbonement(Abonnement ab, string username)
    {
        if (ab.AbboName == username)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    

    public Task<Abonnement?> FindAbonnementById(int id)
        {
            return _context.Abonnement
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public void Add(Abonnement abonnement)
        {
            _context.Add(abonnement);
        }

        public void SaveAbonnement()
        {
            _context.SaveChangesAsync();
        }

        public ValueTask<Abonnement?> FindAbonnementByIdAsync(int id)
        {
            return _context.Abonnement.FindAsync(id);
        }

        public void UpdateAbonnement(Abonnement abonnement)
        {
            
            _context.Update(abonnement);
        }

        public void RemoveAbonnement(Abonnement abonnement)
        {
            _context.Abonnement.Remove(abonnement);
        }

        public bool AbonnementExists(int id)
        {
            return _context.Abonnement.Any(e => e.Id == id);
        }
    }

