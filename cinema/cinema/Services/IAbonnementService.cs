using cinema.Models;
using cinema.Repositories;

namespace cinema.Services
{
    public interface IAbonnementService
    {
        Task<List<Abonnement>>? GetAllAbonnements();
    }
}
