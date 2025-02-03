using System;
using System.Collections.Generic;
using PersonalizedOffersSdk.Offers.Prices;
using PersonalizedOffersSdk.Offers.Rewards;
using PersonalizedOffersSdk.Offers.ValidationConditions;

namespace PersonalizedOffersSdk.Offers
{

    public class Offer
    {
            private readonly Guid _uuid;
            private readonly string _title;
            private readonly string _description;
            private readonly Price _price;
            private readonly List<Reward> _rewards;
            private readonly List<ValidationCondition> _validationConditions;
            private readonly DateTime _startTime;
            private readonly OfferType _offerType;

            private List<Guid> _linkedOffers;

            private OfferCachedState _cachedState;

        public Offer(OfferData offerData)
        {
            _uuid = offerData.uuid;
            _title = offerData.title;
            _description = offerData.description;
            _price = new Price(offerData.price);
            _rewards = new List<Reward>();
            _offerType = offerData.offerType;
            foreach (var rewardData in offerData.rewards)
            {
                _rewards.Add(new Reward(rewardData));
            }

            _validationConditions = new List<ValidationCondition>();
            foreach (var validationConditionData in offerData.validationConditions)
            {
                _validationConditions.Add(new ValidationCondition(validationConditionData));
            }

            _startTime = offerData.startTime;
            _linkedOffers = offerData.linkedOffers;
            _cachedState = new OfferCachedState(false, false);
        }

        public Guid GetUuid() => _uuid;
        public string GetTitle() => _title;
        public string GetDescription() => _description;
        public string GetFinalPriceLabel() => _price.GetFinalPriceLabel();
        public List<Reward> GetRewards() => _rewards;
        public OfferType GetOfferType() => _offerType;
        public void MarkAsBought() => _cachedState.MarkAsBought();
        public void MarkAsConditionMet() => _cachedState.MarkAsConditionMet();

    }
}