using UnityEngine;
using PersonalizedOffersSdk.Offers;
using TMPro;
using System.Collections.Generic;
using System;

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

        private Dictionary<Guid, GameObject> guidsToOffersPanel = new Dictionary<Guid, GameObject>();

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
                sampleOfferUI.PopupulateOffer(offer, onPurchased);
            }
            guidsToOffersPanel.Add(offer.GetUuid(), offerUIObject);
        }

        public void RemoveOffer(Guid guid)
        {
            if (guidsToOffersPanel.ContainsKey(guid))
            {
                Debug.Log("contains key");
                GameObject offerUIObject = guidsToOffersPanel[guid];
                guidsToOffersPanel.Remove(guid);
                Destroy(offerUIObject);
            }
        }
    }
}
