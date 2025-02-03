using UnityEngine;
using TMPro;
using PersonalizedOffersSdk.Offers;
using PersonalizedOffersSdk.Offers.Rewards;
using System.Collections.Generic;
using PersonalizedOffersSdk.Scripts.Sample;
using System;
using UnityEngine.UI;

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

        public void PopupulateOffer(Offer offer, Action onPurchased)
        {
            _offerTitle.text = offer.GetTitle();
            _offerDescription.text = offer.GetDescription();
            _offerPrice.text = offer.GetFinalPriceLabel();
            _purchaseButton.onClick.AddListener(() => onPurchased?.Invoke());

            List<Reward> rewards = offer.GetRewards();

            for(int i = 0; i < rewards.Count; i++)
            {
               
                GameObject rewardItem = Instantiate(_rewardItemPrefab, _rewardItemContainer);
                SampleRewardUi sampleRewardUi = rewardItem.GetComponent<SampleRewardUi>();
                if (sampleRewardUi)
                {
                    Sprite sprite = _sampleRewardImageMap.RewardToImages.Find(x => x.rewardType == rewards[i].GetRewardType())?.sprite ?? _fallbackImage;
                    sampleRewardUi.PopulateReward(sprite, rewards[i].GetAmount().ToString());
                }
            }
                
          
            if (offer.GetDiscountPercent() > 0)
            {
                string offerLabel = offer.GetDiscountLabel();
                _discountImage.SetActive(true);
                _discountText.text = offerLabel;
            }
        }

        public void DisplayLinkedArrow()
        {
            _arrowImage?.SetActive(true);
        }

        public void HideDisplayLinkedArrow()
        {
            _arrowImage?.SetActive(false);
        }

        public void DisplayDisablePanel()
        {
            _disablePanelImage?.SetActive(true);
            _purchaseButton.interactable = false;

        }

        public void HideDisablePanel()
        {
            _disablePanelImage?.SetActive(false);
            _purchaseButton.interactable = true;
        }
    }
}
