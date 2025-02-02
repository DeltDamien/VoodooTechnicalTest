using System.Collections.Generic;
using System;

namespace PersonalizedOffersSdk.Offer
{
    [Serializable]
    public struct OfferData
    {
        public Guid uuid;
        public string title;
        public string description;
        public Price.PriceData price;
        public List<Reward.RewardData> rewards;
        public List<ValidationCondition.ValidationConditionData> validationConditions;
        public DateTime startTime;
        public List<OfferData> linkedOffers;
    }
}