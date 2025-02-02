using PersonalizedOffersSdk.Service;
using System;

namespace PersonalizedOffersSdk.Controller
{
    public class Controllers
    {
        private PersonalizedOffersController _personalizedOfferController;
        private PersonalizedOffersSanityCheckController _personalizedOffersSanityCheckController;

        Controllers(Services services, Guid playerUUid, float checkInterval)
        {
            _personalizedOffersSanityCheckController = new PersonalizedOffersSanityCheckController(checkInterval);

            _personalizedOfferController = new PersonalizedOffersController(playerUUid, services.GetPersonalizedOffersService(), _personalizedOffersSanityCheckController);
        }
    }
}