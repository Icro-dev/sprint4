using cinema.Models;
namespace cinema.Repositories
{
    public interface IAbonnementRepository
    {
        Task<List<Abonnement>> ListOfAllAbonnements();

        Task<Abonnement?> FindAbonnementById(int id);

        void Add(Abonnement abonnement);

        void SaveAbonnement();

        ValueTask<Abonnement?> FindAbonnementByIdAsync(int id);
        void UpdateAbonnement(Abonnement abonnement);
        void RemoveAbonnement(Abonnement abonnement);
        bool AbonnementExists(int id);

    }
}
