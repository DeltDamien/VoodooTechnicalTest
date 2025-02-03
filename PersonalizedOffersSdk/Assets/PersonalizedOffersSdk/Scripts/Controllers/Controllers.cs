using PersonalizedOffersSdk.Offers.Prices;
using PersonalizedOffersSdk.Service;
using System;
using System.Collections.Generic;

namespace PersonalizedOffersSdk.Controller
{
    public struct ControllersData
    {
        public readonly Guid PlayerUUid;
        public readonly bool ImmediateStartSanityCheck;
        public readonly float CheckInterval;
        public readonly List<CurrencyTypeToLabel> CurrencyTypeToLabels;

        public ControllersData(Guid playerUuid, bool immediateStartSanityCheck, float checkInterval, List<CurrencyTypeToLabel> currencyTypeToLabels)
        {
            PlayerUUid = playerUuid;
            ImmediateStartSanityCheck = immediateStartSanityCheck;
            CheckInterval = checkInterval;
            CurrencyTypeToLabels = currencyTypeToLabels;
        }
    }

    public class Controllers
    {
        private readonly PersonalizedOffersController _personalizedOfferController;
        private readonly PersonalizedOffersSanityCheckController _personalizedOffersSanityCheckController;
        private readonly CurrencyController _currencyController;

        public Controllers(Services services, ControllersData controllersData)
        {
            _personalizedOfferController = new PersonalizedOffersController(controllersData.PlayerUUid, services.GetPersonalizedOffersService());

            _personalizedOffersSanityCheckController = new PersonalizedOffersSanityCheckController(_personalizedOfferController, controllersData.ImmediateStartSanityCheck, controllersData.CheckInterval);

            _currencyController = new CurrencyController(controllersData.CurrencyTypeToLabels);

        }

        public PersonalizedOffersController GetPersonalizedOffersController()
        {
            return _personalizedOfferController;
        }

        public PersonalizedOffersSanityCheckController GetPersonalizedOffersSanityCheckController()
        {
            return _personalizedOffersSanityCheckController;
        }

        public CurrencyController GetCurrencyController()
        {
            return _currencyController;
        }
    }
}