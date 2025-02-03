using UnityEngine;
using PersonalizedOffersSdk.Service;
using PersonalizedOffersSdk.Controller;
using System;

namespace PersonalizedOffersSdk
{
    public class PersonalizedOffersSDK : MonoBehaviour
    {


        [SerializeField]
        private bool _isDebugMode;
        [SerializeField]
        private PersonalizedOffersParameters personalizedOffersParameters;

        private Controllers _controllers;
        private Services _services;

        private void Awake()
        {
            // TODO : get the token from an auth backend
            Guid authToken = Guid.NewGuid();

            // TODO : get the player uuid from the game (probable get by another backend, or gmail/facebook/apple)
            Guid playerGuid = Guid.NewGuid();


            _services = new Services(authToken, personalizedOffersParameters.BackendAdress, _isDebugMode);
            _controllers = new Controllers(_services, new ControllersParameters(
                playerGuid,
                personalizedOffersParameters.ImmediateStartSanityCheck,
                personalizedOffersParameters.SanityCheckPeriodInSecond,
                personalizedOffersParameters.CurrencyTypeToLabel.CurrencyTypeToLabel
            )) ;
        }

        public PersonalizedOffersController GetPersonalizedOffersController()
        {
            return _controllers.PersonalizedOfferController;
        }

        public CurrencyController GetCurrencyController()
        {
            return _controllers.CurrencyController;
        }

        public PersonalizedOffersSanityCheckController GetPersonalizedOffersSanityCheckController()
        {
            return _controllers.PersonalizedOffersSanityCheckController;
        }
    }
}
