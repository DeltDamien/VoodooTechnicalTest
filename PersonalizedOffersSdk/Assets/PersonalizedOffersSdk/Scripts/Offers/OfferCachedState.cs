namespace PersonalizedOffersSdk.Offers
{
    public class OfferCachedState
    {
        public bool IsConditionMet { get; private set; }
        public bool IsBought { get; private set; }

        public OfferCachedState(bool isConditionMet, bool isBought)
        {
            IsConditionMet = isConditionMet;
            IsBought = isBought;
        }

        public void MarkAsBought() => IsBought = true;
        public void MarkAsNotBought() => IsBought = false;
        public void MarkAsConditionMet() => IsConditionMet = true;
        public void MarkAsNotConditionMet() => IsConditionMet = false;
    }
}

