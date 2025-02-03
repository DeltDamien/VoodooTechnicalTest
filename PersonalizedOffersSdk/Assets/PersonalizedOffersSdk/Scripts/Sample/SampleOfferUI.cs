using UnityEngine;
using TMPro;
using PersonalizedOffersSdk.Offers;
using PersonalizedOffersSdk.Offers.Rewards;
using System.Collections.Generic;
using PersonalizedOffersSdk.Scripts.Sample;
using System;
using UnityEngine.UI;
using PersonalizedOffersSdk.Controller;

namespace PersonalizedOffersSdk.Sample
{
    public class SampleOfferUI : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI _offerTitle;
        [SerializeField] 
        private TextMeshProUGUI _offerDescription;
        [SerializeField] 
        private TextMeshProUGUI _offerPrice;
        [SerializeField]
        private Button _purchaseButton;
        [SerializeField] 
        private GameObject _rewardItemPrefab;
        [SerializeField]
        private SOSampleRewardImageMap _sampleRewardImageMap;
        [SerializeField]
        private Sprite _fallbackImage;
        [SerializeField]
        private Transform _rewardItemContainer;
        [SerializeField]
        private GameObject _arrowImage;
        [SerializeField]
        private GameObject _disablePanelImage;
        [SerializeField]
        private GameObject _discountImage;
        [SerializeField]
        private TextMeshProUGUI _discountText;

        private CurrencyController _currencyController;

        public void Start()
        {
            if (HasInitialisationErrors())
            {
                Debug.LogError("One or more required fields are not set. The SampleOfferUI will not work properly.");
            }
        }

        private bool HasInitialisationErrors()
        {
            bool hasError = false;

            if (_offerTitle == null)
            {
                Debug.LogError("Offer Title is not set.");
                hasError = true;
            }

            if (_offerDescription == null)
            {
                Debug.LogError("Offer Description is not set.");
                hasError = true;
            }

            if (_offerPrice == null)
            {
                Debug.LogError("Offer Price is not set.");
                hasError = true;
            }

            if (_purchaseButton == null)
            {
                Debug.LogError("Purchase Button is not set.");
                hasError = true;
            }

            if (_rewardItemPrefab == null)
            {
                Debug.LogError("Reward Item Prefab is not set.");
                hasError = true;
            }

            if (_sampleRewardImageMap == null)
            {
                Debug.LogError("Sample Reward Image Map is not set.");
                hasError = true;
            }

            if (_fallbackImage == null)
            {
                Debug.LogError("Fallback Image is not set.");
                hasError = true;
            }

            if (_rewardItemContainer == null)
            {
                Debug.LogError("Reward Item Container is not set.");
                hasError = true;
            }

            if (_arrowImage == null)
            {
                Debug.LogError("Arrow Image is not set.");
                hasError = true;
            }

            if (_disablePanelImage == null)
            {
                Debug.LogError("Disable Panel Image is not set.");
                hasError = true;
            }

            if (_discountImage == null)
            {
                Debug.LogError("Discount Image is not set.");
                hasError = true;
            }

            if (_discountText == null)
            {
                Debug.LogError("Discount Text is not set.");
                hasError = true;
            }

            if (_currencyController == null)
            {
                Debug.LogError("Currency Controller is not set. Inject if with InjectCurrencyController");
                hasError = true;
            }

            return hasError;
        }

        public void InjectCurrencyController(CurrencyController currencyController)
        {
            
            _currencyController = currencyController;
        }

        public void PopulateOffer(Offer offer, Action onPurchased)
        {
            _offerTitle.text = offer.Title;
            _offerDescription.text = offer.Description;
            _offerPrice.text = _currencyController.GetFinalPriceLabel(offer.Price);
            _purchaseButton.onClick.AddListener(() => onPurchased?.Invoke());

            PopulateRewardsInOffer(offer.Rewards);

            if (offer.GetDiscountPercent() > 0)
            {
                string offerLabel = offer.GetDiscountLabel();
                _discountImage.SetActive(true);
                _discountText.text = offerLabel;
            }
        }

        private void PopulateRewardsInOffer(List<Reward> rewards)
        {

            for (int i = 0; i < rewards.Count; i++)
            {
                GameObject rewardItem = Instantiate(_rewardItemPrefab, _rewardItemContainer);
                SampleRewardUi sampleRewardUi = rewardItem.GetComponent<SampleRewardUi>();
                if (sampleRewardUi)
                {
                    Sprite sprite;
                    sprite = _sampleRewardImageMap.RewardToImages.Find(x => x.rewardType == rewards[i].RewardType).sprite;

                    sampleRewardUi.PopulateReward(sprite, rewards[i].Amount.ToString());
                }
            }
        }

        public void DisplayLinkedArrow()
        {
            if (_arrowImage != null)
            {
                _arrowImage.SetActive(true);
            }
        }

        public void HideDisplayLinkedArrow()
        {
            if (_arrowImage != null)
            {
                _arrowImage.SetActive(false);
            }
        }

        public void DisplayDisablePanel()
        {
            if (_disablePanelImage != null)
            {
                _disablePanelImage.SetActive(true);               
            }
            if (_purchaseButton != null) 
            {
                _purchaseButton.interactable = false;
            }
        }

        public void HideDisablePanel()
        {
            if (_disablePanelImage != null)
            {
                _disablePanelImage.SetActive(false);
            }
            if (_purchaseButton != null)
            {
                _purchaseButton.interactable = true;
            }
        }
    }
}
