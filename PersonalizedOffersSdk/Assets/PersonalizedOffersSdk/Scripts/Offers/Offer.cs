using System;
using System.Collections.Generic;
using PersonalizedOffersSdk.Offers.Prices;
using PersonalizedOffersSdk.Offers.Rewards;
using PersonalizedOffersSdk.Offers.ValidationConditions;

namespace PersonalizedOffersSdk.Offers
{

    public class Offer
    {
        public readonly Guid OfferUuid;
        public readonly string Title;
        public readonly string Description;
        public readonly Price Price;
        public readonly List<Reward> Rewards;
        public readonly List<ValidationCondition> ValidationCondition;
        public readonly DateTime StartTime;
        public readonly OfferType OfferType;

        private List<Guid> _linkedOffers;
        private OfferCachedState _cachedState;

        public Offer(OfferDto offerData)
        {
            OfferUuid = offerData.OfferUUid;
            Title = offerData.Title;
            Description = offerData.Description;

            Price = offerData.Price;

            Rewards = new List<Reward>();
            OfferType = offerData.OfferType;
            Rewards = offerData.Rewards;

            ValidationCondition = offerData.ValidationConditions;
            StartTime = offerData.StartTime;
            _linkedOffers = new List<Guid>(offerData.LinkedOffers);
            _cachedState = new OfferCachedState(false, false);
        }

        public void MarkAsBought()
        {
            _cachedState.MarkAsBought();
            _linkedOffers.Clear();
        }
        public void MarkAsConditionMet() => _cachedState.MarkAsConditionMet();

        public IReadOnlyList<Guid> GetLinkedOffers() => _linkedOffers;

        public float GetDiscountPercent() => Price.GetDiscountPercent();
        public string GetDiscountLabel() => Price.GetDiscountLabel();

    }
}