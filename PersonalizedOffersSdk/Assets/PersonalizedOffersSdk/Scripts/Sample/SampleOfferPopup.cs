using UnityEngine;
using PersonalizedOffersSdk.Offers;

namespace PersonalizedOffersSdk.Sample 
{
    public class SampleOfferPopup : MonoBehaviour
    {
        [SerializeField]
        private GameObject _offerPrefab;
        [SerializeField]
        private Transform _offersContainer;

        public void CreateOffer(string title, Offer offer, System.Action onPurchased)
        { 
            GameObject offerUIObject = GameObject.Instantiate(_offerPrefab, _offersContainer);
            SampleOfferUI sampleOfferUI = offerUIObject.GetComponent<SampleOfferUI>();
            if (sampleOfferUI)
            {
                sampleOfferUI.PopupulateOffer(offer, onPurchased);
            }   
        }
    }
}
