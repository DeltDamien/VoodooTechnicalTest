using UnityEngine;
using PersonalizedOffersSdk.Offers;
using TMPro;
using System.Collections.Generic;
using System;
using PersonalizedOffersSdk.Controller;

namespace PersonalizedOffersSdk.Sample 
{
    public class SampleOfferPopup : MonoBehaviour
    {
        [SerializeField]
        private GameObject _offerPrefab;
        [SerializeField]
        private Transform _offersContainer;
        [SerializeField]
        private TextMeshProUGUI _titleText;
        [SerializeField]
        private PersonalizedOffersSDK _personalizedOffersSDK;
        [SerializeField]

        private Dictionary<Guid, SampleOfferUI> _guidsToOffersPanelUI = new Dictionary<Guid, SampleOfferUI>();
        private PersonalizedOffersController _personalizedOffersController;

        private const OfferType _fallbackOfferType = OfferType.Regular;
        private void Start()
        {
            if (HasInitialisationErrors())
            {
                Debug.LogError("One or more required fields are not set. The SampleOfferPopup will not work properly.");
                return;
            }
            _personalizedOffersController = _personalizedOffersSDK.GetPersonalizedOffersController();
        }

        private bool HasInitialisationErrors()
        {
            bool hasError = false;

            if (_offerPrefab == null)
            {
                Debug.LogError("Offer Prefab is not set.");
                hasError = true;
            }

            if (_offersContainer == null)
            {
                Debug.LogError("Offers Container is not set.");
                hasError = true;
            }

            if (_titleText == null)
            {
                Debug.LogError("Title Text is not set.");
                hasError = true;
            }

            if (_personalizedOffersSDK == null)
            {
                Debug.LogError("PersonalizedOffersSDK is not set.");
                hasError = true;
            }

            return hasError;
        }

        public void UpdatePopupUI(string title)
        {
            if (_titleText != null)
            {
                _titleText.SetText(title);
            }
        }

        public void CreateOffer(Offer offer, Action onPurchased)
        { 
            GameObject offerUIObject = Instantiate(_offerPrefab, _offersContainer);
            SampleOfferUI sampleOfferUI = offerUIObject.GetComponent<SampleOfferUI>();
            
            if (sampleOfferUI != null && _personalizedOffersSDK != null)
            {
                sampleOfferUI.InjectCurrencyController(_personalizedOffersSDK.GetCurrencyController());
                sampleOfferUI.PopulateOffer(offer, onPurchased);
            }
            _guidsToOffersPanelUI.Add(offer.OfferUuid, sampleOfferUI);
            UpdateOffersUI();
        }

        public void RemoveOffer(Guid guid)
        {
            if (_guidsToOffersPanelUI.ContainsKey(guid))
            {
                GameObject offerUIObject = _guidsToOffersPanelUI[guid].gameObject;
                _guidsToOffersPanelUI.Remove(guid);
                Destroy(offerUIObject);
            }
        }

        public void UpdateOffersUI()
        {
            foreach(KeyValuePair<Guid, SampleOfferUI> guidToOffer in _guidsToOffersPanelUI)
            {
                OfferType offerType = _personalizedOffersController?.GetOfferType(guidToOffer.Key) ?? _fallbackOfferType;
                if (offerType == OfferType.Chained || offerType == OfferType.Endless) {
                    bool IsOfferHasLinkedOffers = _personalizedOffersController.IsOfferHasLinkedOffers(guidToOffer.Key);
                    bool isOfferLinkedOffers = _personalizedOffersController.IsOfferLinkedOffers(guidToOffer.Key);

                    if (IsOfferHasLinkedOffers)
                    {
                        guidToOffer.Value.DisplayLinkedArrow();
                    } 
                    else
                    {
                        guidToOffer.Value.HideDisplayLinkedArrow();
                    }
                    if (isOfferLinkedOffers)
                    {
                        guidToOffer.Value.DisplayDisablePanel();
                    }
                    else
                    {
                        guidToOffer.Value.HideDisablePanel();
                    }

                }
            }
        }
    }
}