using PersonalizedOffersSdk.Offers.Prices;
using PersonalizedOffersSdk.Offers.ValidationConditions;
using PersonalizedOffersSdk.Service;
using System;
using System.Collections.Generic;

namespace PersonalizedOffersSdk.Controller
{
    public readonly struct ControllersParameters
    {
        public readonly Guid PlayerUUid;
        public readonly bool ImmediateStartSanityCheck;
        public readonly float CheckInterval;
        public readonly List<CurrencyTypeToLabel> CurrencyTypeToLabels;

        public ControllersParameters(Guid playerUuid, bool immediateStartSanityCheck, float checkInterval, List<CurrencyTypeToLabel> currencyTypeToLabels)
        {
            PlayerUUid = playerUuid;
            ImmediateStartSanityCheck = immediateStartSanityCheck;
            CheckInterval = checkInterval;
            CurrencyTypeToLabels = currencyTypeToLabels;
        }
    }

    public class Controllers
    {
        public readonly PersonalizedOffersController PersonalizedOfferController;
        public readonly PersonalizedOffersSanityCheckController PersonalizedOffersSanityCheckController;
        public readonly CurrencyController CurrencyController;
        public readonly ValidationConditionController ValidationConditionController;

        public Controllers(Services services, ControllersParameters controllersData)
        {
            PersonalizedOfferController = new PersonalizedOffersController(controllersData.PlayerUUid, services.PersonalizedOffersService);

            PersonalizedOffersSanityCheckController = new PersonalizedOffersSanityCheckController(PersonalizedOfferController, controllersData.ImmediateStartSanityCheck, controllersData.CheckInterval);

            CurrencyController = new CurrencyController(controllersData.CurrencyTypeToLabels);
            ValidationConditionController = new ValidationConditionController();

        }
    }
}