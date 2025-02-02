using System;

namespace PersonalizedOffersSdk.Offers.Rewards
{
    [Serializable]
    public struct RewardData
    {
        public RewardType rewardType;
        public int amount;
    }
}