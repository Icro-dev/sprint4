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

        public async Task<Abonnement?> FindAbonnementByName(string username)
        {
            return await _context.Abonnement
                .FirstOrDefaultAsync(a => a.AbboName == username);
         
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

