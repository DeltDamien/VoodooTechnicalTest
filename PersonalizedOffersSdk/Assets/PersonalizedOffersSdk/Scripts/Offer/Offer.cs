using System;
using System.Collections.Generic;

namespace PersonalizedOffersSdk.Offer
{

    public class Offer
    {
        private readonly Guid _uuid;
        private readonly string _title;
        private readonly string _description;
        private readonly Price.Price _price;
        private readonly List<Reward.Reward> _rewards;
        private readonly List<ValidationCondition.ValidationCondition> _validationConditions;
        private readonly DateTime _startTime;

        private List<Offer> _linkedOffers;

        private OfferCachedState _cachedState;

        public Offer(OfferData offerData)
        {
            _uuid = offerData.uuid;
            _title = offerData.title;
            _description = offerData.description;
            _price = new Price.Price(offerData.price);
            _rewards = new List<Reward.Reward>();
            foreach (var rewardData in offerData.rewards)
            {
                _rewards.Add(new Reward.Reward(rewardData));
            }

            _validationConditions = new List<ValidationCondition.ValidationCondition>();
            foreach (var validationConditionData in offerData.validationConditions)
            {
                _validationConditions.Add(new ValidationCondition.ValidationCondition(validationConditionData));
            }

            _startTime = offerData.startTime;
            _linkedOffers = new List<Offer>();
            foreach (var linkedOfferData in offerData.linkedOffers)
            {
                _linkedOffers.Add(new Offer(linkedOfferData));
            }

            _cachedState = new OfferCachedState(false, false);
        }

        public Guid GetUuid()
        {
            return _uuid;
        }

        public string GetTitle()
        {
            return _title;
        }

        public string GetDescription()
        {
            return _description;
        }

        public void MarkAsBought()
        {
            _cachedState.MarkAsBought();
        }

        public void MarkAsConditionMet()
        {
            _cachedState.MarkAsConditionMet();
        }

    }
}