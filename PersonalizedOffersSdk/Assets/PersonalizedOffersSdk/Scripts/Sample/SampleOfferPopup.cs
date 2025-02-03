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

        private void Start()
        {
            if (_personalizedOffersSDK == null)
            {
                Debug.LogError("PersonalizedOffersSDK is not set.");
                return;
            }
            _personalizedOffersController = _personalizedOffersSDK?.GetPersonalizedOffersController();
        }

        public void UpdatePopupUI(string title)
        {
            _titleText?.SetText(title);
        }

        public void CreateOffer(string title, Offer offer, System.Action onPurchased)
        { 
            GameObject offerUIObject = GameObject.Instantiate(_offerPrefab, _offersContainer);
            SampleOfferUI sampleOfferUI = offerUIObject.GetComponent<SampleOfferUI>();
            
            if (sampleOfferUI)
            {
                sampleOfferUI.InjectCurrencyController(_personalizedOffersSDK?.GetCurrencyController());
                sampleOfferUI.PopupulateOffer(offer, onPurchased);
            }
            _guidsToOffersPanelUI.Add(offer.GetUuid(), sampleOfferUI);
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
                OfferType offerType = _personalizedOffersController?.GetOfferType(guidToOffer.Key) ?? OfferType.Regular;
                if (offerType == OfferType.Chained || offerType == OfferType.Endless) {
                    bool IsOfferHasLinkedOffers = _personalizedOffersController.IsOfferHasLinkedOffers(guidToOffer.Key);
                    bool isOfferLinkedOffers = _personalizedOffersController.isOfferLinkedOffers(guidToOffer.Key);

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