namespace PersonalizedOffersSdk.Offers.Rewards
{
    public class Reward
    {
        private readonly RewardType _rewardType;
        private readonly int _amount;

        public Reward(RewardType rewardType, int amount)
        {
            _rewardType = rewardType;
            _amount = amount;
        }

        public Reward(RewardData rewardData)
        {
            _rewardType = rewardData.rewardType;
            _amount = rewardData.amount;
        }

        public RewardType GetRewardType()
        {
            return _rewardType;
        }

        public int GetAmount()
        {
            return _amount;
        }
    }
}