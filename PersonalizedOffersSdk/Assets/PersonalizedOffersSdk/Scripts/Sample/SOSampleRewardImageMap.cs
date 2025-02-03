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


    [CreateAssetMenu(fileName = "SampleRewardImageMap", menuName = "Scriptable Objects/SampleRewardImageMap")]
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