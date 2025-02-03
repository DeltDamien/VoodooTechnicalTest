namespace PersonalizedOffersSdk.Offers.Rewards
{
    public readonly struct Reward
    {
        public readonly RewardType RewardType;
        public readonly int Amount;

        public Reward(RewardType rewardType, int amount)
        {
            RewardType = rewardType;
            Amount = amount;
        }
    }
}