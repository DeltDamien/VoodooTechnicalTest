using System.Collections.Generic;
using System;
using UnityEngine;

namespace PersonalizedOffersSdk.Offers.ValidationConditions
{
    public class ValidationConditionController
    {
        // Allow us to have common type for validation condition, a string then parse it to what we need
        private readonly Dictionary<ValidationConditionType, Func<string, object>> _parsers;

        public ValidationConditionController()
        {
            _parsers = new Dictionary<ValidationConditionType, Func<string, object>>
            {
                {ValidationConditionType.TimeLeft, ParseTimeLeft},
                {ValidationConditionType.LevelPassed, ParseLevelSucceed},
                {ValidationConditionType.OtherOfferBought, ParseOfferToComplete},
                {ValidationConditionType.FeatureUnlocked, ParseFeatureUnlocked}
            };
        }

        public object Parse(ValidationConditionType type, string value)
        {
            if (_parsers.TryGetValue(type, out var parser))
            {
                return parser(value);
            }
            Debug.LogError($"No parser found for {type}");
            return null;
        }

        private object ParseTimeLeft(string value)
        {
            bool parseSuccess = int.TryParse(value, out int timeLeftInSeconds);
            if (!parseSuccess)
            {
                Debug.LogError("Invalid format for TimeLeft");
                return null;
            }
            return timeLeftInSeconds;
        }

        private object ParseLevelSucceed(string value)
        {
            bool parseSuccess = int.TryParse(value, out int level);
            if (!parseSuccess)
            {
                Debug.LogError("Invalid format for LevelSucceed");
                return null;
            }
            return level;
        }

        private object ParseOfferToComplete(string value)
        {
            bool parseSuccess = Guid.TryParse(value, out Guid offerUuid);
            if (!parseSuccess)
            {
                Debug.LogError("Invalid format for OfferToComplete");
                return null;
            }
            return offerUuid;
        }

        private object ParseFeatureUnlocked(string value)
        {
            return value;
        }
    }
}

