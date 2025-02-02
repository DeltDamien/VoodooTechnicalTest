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

        private Services _services;
        private Controllers _controllers;
        private Guid _authToken;
        private Guid _playerGuid;

        private void Awake()
        {
            // TODO : get the token from an auth backend
            _authToken = Guid.NewGuid();

            // TODO : get the player uuid from the game (probable get by another backend, or gmail/facebook/apple)
            _playerGuid = Guid.NewGuid();


            _services = new Services(_authToken, personalizedOffersParameters.BackendAdress, _isDebugMode);
            _controllers = new Controllers(_services, _playerGuid, personalizedOffersParameters.ImmediateStartSanityCheck, personalizedOffersParameters.SanityCheckPeriodInSecond);
        }

        public PersonalizedOffersController GetPersonalizedOffersController()
        {
            return _controllers.GetPersonalizedOffersController();
        }
    }
}
