using System.Collections.Generic;
using PersonalizedOffersSdk.Offers.Prices;
using PersonalizedOffersSdk.Offers.Rewards;
using PersonalizedOffersSdk.Offers.ValidationConditions;
using System;

namespace PersonalizedOffersSdk.Offers
{
    [Serializable]
    public readonly struct OfferDto
    {
        public readonly Guid OfferUUid;
        public readonly string Title;
        public readonly string Description;
        public readonly Price Price;
        public readonly List<Reward> Rewards;
        public readonly List<ValidationCondition> ValidationConditions;
        public readonly DateTime StartTime;
        public readonly List<Guid> LinkedOffers;
        public readonly OfferType OfferType;

        public OfferDto(Guid uuid, string title, string description, Price price, List<Reward> rewards,
            List<ValidationCondition> validationConditions, DateTime startTime,
            List<Guid> linkedOffers, OfferType offerType)
        {
            OfferUUid = uuid;
            Title = title;
            Description = description;
            Price = price;
            Rewards = rewards;
            ValidationConditions = validationConditions;
            StartTime = startTime;
            LinkedOffers = linkedOffers;
            OfferType = offerType;
        }
    }
}