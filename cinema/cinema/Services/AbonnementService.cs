using cinema.Models;
using cinema.Repositories;


namespace cinema.Services
{
    public class AbonnementService : IAbonnementService
    {

        private readonly IAbonnementRepository _abonnementRepository;
       

            public AbonnementService(IAbonnementRepository abonnementRepository)
            {
            _abonnementRepository = abonnementRepository;
            }

        public async Task<List<Abonnement>>? GetAllAbonnements()
        {
            return await _abonnementRepository.ListOfAllAbonnements();
      
           /* var abo = _abonnementRepository.FindAbonnementByName(username).Result;
            List<Abonnement> listOfAbonnements = new List<Abonnement>();
            var getListOfAbonnements = _abonnementRepository.ListOfAllAbonnements();
            listOfAbonnements.AddRange(getListOfAbonnements);
            if (abo != null)
            {
                Abonnement AbonnementUser = new Abonnement();
                AbonnementUser.AbboName = abo.AbboName;
                AbonnementUser.Expired = abo.Expired;
                if (AbonnementUser.AbboName == username)
                {
                    ViewBag.Abonnement = AbonnementUser;
                }
            }*/
        }
       

    }
}
