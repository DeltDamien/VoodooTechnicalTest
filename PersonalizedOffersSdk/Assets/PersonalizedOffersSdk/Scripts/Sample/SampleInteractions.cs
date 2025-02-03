using NUnit.Framework;
using PersonalizedOffersSdk.Controller;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PersonalizedOffersSdk.Offers;
using PersonalizedOffersSdk.Scripts.Sample;
using System.Linq;
using Cysharp.Threading.Tasks;

namespace PersonalizedOffersSdk.Sample
{
    internal enum SamplePurchaseState
    {
        NonePurchased = 0,
        RegularPurchased,
        FirstChainedPurchased,
        SecondChainedPurchased,
        SamplePurchaseStateCount
    }

    public class SampleInteractions : MonoBehaviour
    {
        [SerializeField] 
        private Button _startSessionButton;
        [SerializeField] 
        private SampleOfferPopup _offersPopup;
        [SerializeField] 
        private PersonalizedOffersSDK _personalizedOffersSDK;
        [SerializeField]
        private SOOfferTypeLabelMap _offerTypeLabelMap;

        private PersonalizedOffersController _personalizedOffersController;
        private bool isChainedOfferDisplayed = false;
        private SamplePurchaseState _purchaseState = SamplePurchaseState.NonePurchased;

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

            if (_offersPopup == null)
            {
                Debug.LogError("Popup offer is not set.");
                return;
            }

            _personalizedOffersController = _personalizedOffersSDK.GetPersonalizedOffersController();
        }



        public void OnStartSessionClicked()
        {
            _offersPopup?.gameObject.SetActive(true);
            _startSessionButton.gameObject.SetActive(false);
            OnTriggerReceived(TriggerType.SessionStarted);

        }

        private async void OnTriggerReceived(TriggerType triggerType)
        {
            if (_personalizedOffersController != null)
            {
                await _personalizedOffersController.OnTriggerReceivedAsync(triggerType);

                await _personalizedOffersController.UpdateOffersValidationAsync();

                List<Offer> offers = _personalizedOffersController.GetOffers();

                if (offers.Count > 0)
                {
                    string offerPopupTitleLabel = _offerTypeLabelMap.OfferTypeToLabel.First(x => x.offerType == offers[0].GetOfferType()).label ?? "Offer";
                    _offersPopup.UpdatePopupUI(offerPopupTitleLabel);

                    for (int i = 0; i < offers.Count; i++)
                    {
                        Guid offerGuid = offers[i].GetUuid();
                        _offersPopup.CreateOffer(offers[i].GetTitle(), offers[i], () =>
                        {
                            OnPurchaseOffer(offerGuid);
                        });
                    }
                }
            }
        }

        private async void OnPurchaseOffer(Guid offerUuid)
        {
            // a validation could be here, but for the sake of the sample we are going to assume that the purchase is valid
            // await _personalizedOffersController.ValidatePurchaseOffer(offerUuid);
            // Should call here purchase controller which call iap unity sdk then store the purchase backend
            // let's consider that the purchase is successful
            await _personalizedOffersController.ValidatePurchaseOfferAsync(offerUuid);
            _offersPopup.RemoveOffer(offerUuid);

            ++_purchaseState;


            if (_purchaseState < SamplePurchaseState.FirstChainedPurchased)
            {
                OnTriggerReceived(TriggerType.OfferPurchased);
            }
            else
            {
                if (_purchaseState == SamplePurchaseState.SecondChainedPurchased)
                {
                    _startSessionButton.gameObject.SetActive(true);
                    _offersPopup.gameObject.SetActive(false);
                    _purchaseState = SamplePurchaseState.NonePurchased;
                    await _personalizedOffersController.UpdateOffersValidationAsync();
                }
                _offersPopup.UpdateOffersUI();
            }
        }
    }
}