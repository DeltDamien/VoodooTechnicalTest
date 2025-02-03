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
        private SamplePurchaseState _purchaseState = SamplePurchaseState.NonePurchased;

        private const string _offerPopupTitleLabelFallback = "Offer";

        private void Start()
        {
            if (HasInitialisationErrors())
            {
                Debug.Log("At least one error occurred in SampleInteractions, the sample will not work properly.");
                return;
            }

            _personalizedOffersController = _personalizedOffersSDK.GetPersonalizedOffersController();
        }


        private bool HasInitialisationErrors()
        {
            bool hasError = false;

            if (_personalizedOffersSDK == null)
            {
                Debug.LogError("PersonalizedOffersSDK is not set.");
                hasError = true;
            }

            if (_startSessionButton == null)
            {
                Debug.LogError("Start session button is not set.");
                hasError = true;
            }

            if (_offersPopup == null)
            {
                Debug.LogError("Popup offer is not set.");
                hasError = true;
            }

            return hasError;
        }



        public void OnStartSessionClicked()
        {
            if (_offersPopup != null)
            {
                _offersPopup.gameObject.SetActive(true);
            }
            if (_startSessionButton != null)
            {
                _startSessionButton.gameObject.SetActive(false);
            }
            OnTriggerReceived(TriggerType.SessionStarted);

        }

        private async void OnTriggerReceived(TriggerType triggerType)
        {
            if (_personalizedOffersController != null)
            {
                await _personalizedOffersController.OnTriggerReceivedAsync(triggerType);

                await _personalizedOffersController.UpdateOffersValidationAsync();

                IReadOnlyList<Offer> offers = _personalizedOffersController.GetOffers();

                if (offers.Count > 0)
                {
                    string offerPopupTitleLabel = _offerTypeLabelMap.OfferTypeToLabel.First(x => x.offerType == offers[0].OfferType).label ?? _offerPopupTitleLabelFallback;
                    _offersPopup.UpdatePopupUI(offerPopupTitleLabel);

                    for (int i = 0; i < offers.Count; i++)
                    {
                        Guid offerGuid = offers[i].OfferUuid;
                        _offersPopup.CreateOffer(offers[i], () =>
                        {
                            OnOfferPurchased(offerGuid);
                        });
                    }
                }
            }
        }

        private async void OnOfferPurchased(Guid offerUuid)
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