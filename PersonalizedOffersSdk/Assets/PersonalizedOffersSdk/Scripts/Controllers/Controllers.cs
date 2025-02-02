using PersonalizedOffersSdk.Service;
using System;

namespace PersonalizedOffersSdk.Controller
{
    public class Controllers
    {
        private readonly PersonalizedOffersController _personalizedOfferController;
        private readonly PersonalizedOffersSanityCheckController _personalizedOffersSanityCheckController;

        public Controllers(Services services, Guid playerUUid, float checkInterval)
        {
            _personalizedOffersSanityCheckController = new PersonalizedOffersSanityCheckController(checkInterval);

            _personalizedOfferController = new PersonalizedOffersController(playerUUid, services.GetPersonalizedOffersService(), _personalizedOffersSanityCheckController);
        }
    }
}