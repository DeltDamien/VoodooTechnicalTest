using NUnit.Framework;
using PersonalizedOffersSdk.Controller;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PersonalizedOffersSdk.Offers;

namespace PersonalizedOffersSdk.Sample
{
    public class SampleInteractions : MonoBehaviour
    {
        [SerializeField] 
        private Button _startSessionButton;
        [SerializeField] 
        private Transform _offersContainer;
        [SerializeField] 
        private GameObject _popupOfferPrefab;
        [SerializeField] 
        private PersonalizedOffersSDK _personalizedOffersSDK;

        private PersonalizedOffersController _personalizedOffersController;
        private GameObject _offerDisplay;

        private void Start()
        {
            if (_personalizedOffersSDK == null)
            {
                Debug.LogError("PersonalizedOffersSDK is not set.");
                return;
            }

            if (_startSessionButton == null)
            {
                Debug.LogError("Start session button is not set.");
                return;
            }

            _personalizedOffersController = _personalizedOffersSDK.GetPersonalizedOffersController();
            _startSessionButton.onClick.AddListener(OnStartSessionClicked);
        }



        private void OnStartSessionClicked()
        {
            _personalizedOffersController.OnTriggerReceived(TriggerType.SessionStarted);

            List<Offer> offers = _personalizedOffersController.GetOffers();

            if (offers.Count > 0)
            {
                DisplayOffers(offers);
            }

        }

        private void DisplayOffers(List<Offer> offers)
        {
            _offerDisplay = Instantiate(_popupOfferPrefab, _offersContainer);
            SampleOfferPopup sampleOfferPopup = _offerDisplay.GetComponent<SampleOfferPopup>();
            if (sampleOfferPopup)
            {
                for (int i = 0; i < offers.Count; i++)
                {
                    Guid offerGuid = offers[i].GetUuid();
                    sampleOfferPopup.CreateOffer(offers[i].GetTitle(), offers[i], () =>
                    {
                        OnPurchaseOffer(offerGuid);
                        GameObject.Destroy(_offerDisplay);
                    });
                }
            }
        }   

        private void OnCancelOffer(Guid offerUuid)
        {
            _personalizedOffersController.CancelledOffer(offerUuid);
            // cancel destroy for the sample, in real case just popup close
            GameObject.Destroy(_offerDisplay);
        }

        private void OnPurchaseOffer(Guid offerUuid)
        {
            // Should call here purchase controller which call iap unity sdk then store the purchase backend
            // let's consider that the purchase is successful
            _personalizedOffersController.ValidatePurchaseOffer(offerUuid);
        }
    }
}