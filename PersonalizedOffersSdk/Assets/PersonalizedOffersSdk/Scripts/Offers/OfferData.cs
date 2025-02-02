using System.Collections.Generic;
using PersonalizedOffersSdk.Offers.Prices;
using PersonalizedOffersSdk.Offers.Rewards;
using PersonalizedOffersSdk.Offers.ValidationConditions;
using System;

namespace PersonalizedOffersSdk.Offers
{
    [Serializable]
    public struct OfferData
    {
        public Guid uuid;
        public string title;
        public string description;
        public PriceData price;
        public List<RewardData> rewards;
        public List<ValidationConditionData> validationConditions;
        public DateTime startTime;
        public List<Guid> linkedOffers;
        public OfferType offerType;
    }
}