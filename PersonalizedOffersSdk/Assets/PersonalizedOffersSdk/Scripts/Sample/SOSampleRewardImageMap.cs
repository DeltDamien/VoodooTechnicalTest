using NUnit.Framework;
using PersonalizedOffersSdk.Offers.Rewards;
using System.Collections.Generic;
using UnityEngine;

namespace PersonalizedOffersSdk.Scripts.Sample
{
    [System.Serializable]
    public class RewardToImage
    {
        [SerializeField] 
        public RewardType rewardType;
        [SerializeField] 
        public Sprite sprite;
    }

    // TODO : label should be key for localization
    // Resources should have a sprite linked to to use it instead of a string for hard and soft currency
    [CreateAssetMenu(fileName = "SampleRewardImageMap", menuName = "PersonalizedOffersSDK/SO/SampleRewardImageMap")]
    public class SOSampleRewardImageMap : ScriptableObject
    {
        [SerializeField] 
        private List<RewardToImage> _rewardToImages;

        public List<RewardToImage> RewardToImages => _rewardToImages;

        public SOSampleRewardImageMap()
        {
            _rewardToImages = new List<RewardToImage>();
        }
    }
}