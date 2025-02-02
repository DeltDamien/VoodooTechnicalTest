using PersonalizedOffersSdk.Service;
using System;

namespace PersonalizedOffersSdk.Controller
{
    public class Controllers
    {
        private readonly PersonalizedOffersController _personalizedOfferController;
        private readonly PersonalizedOffersSanityCheckController _personalizedOffersSanityCheckController;

        public Controllers(Services services, Guid playerUUid, bool immediateStartSanityCheck, float checkInterval)
        {
            _personalizedOfferController = new PersonalizedOffersController(playerUUid, services.GetPersonalizedOffersService());

            _personalizedOffersSanityCheckController = new PersonalizedOffersSanityCheckController(_personalizedOfferController, immediateStartSanityCheck, checkInterval);

        }

        public PersonalizedOffersController GetPersonalizedOffersController()
        {
            return _personalizedOfferController;
        }

        public PersonalizedOffersSanityCheckController GetPersonalizedOffersSanityCheckController()
        {
            return _personalizedOffersSanityCheckController;
        }
    }
}