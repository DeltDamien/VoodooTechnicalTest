using System;

namespace PersonalizedOffersSdk.Offer.Reward
{
    [Serializable]
    public struct RewardData
    {
        public RewardType rewardType;
        public int amount;
    }
}