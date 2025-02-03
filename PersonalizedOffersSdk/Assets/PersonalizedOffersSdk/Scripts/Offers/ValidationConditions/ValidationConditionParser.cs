using System.Collections.Generic;
using System;
using UnityEngine;

namespace PersonalizedOffersSdk.Offers.ValidationConditions
{
    // Allow us to have common type for validation condition, a string then parse it to what we need
    public static class ValidationConditionParser
    {
        private static readonly Dictionary<ValidationConditionType, Func<string, object>> _parsers = new() 
        {
            { ValidationConditionType.TimeLeft, ParseTimeLeft },
            { ValidationConditionType.LevelPased, ParseLevelSucceed },
            { ValidationConditionType.OtherOfferBought, ParseOfferToComplete },
            { ValidationConditionType.FeatureUnlocked,  ParseFeatureUnlocked}
        };

        public static object Parse(ValidationConditionType type, string value)
        {
            if (_parsers.TryGetValue(type, out var parser))
            {
                return parser(value);
            }
            Debug.LogError($"No parser found for {type}");
            return null;
        }

        private static object ParseTimeLeft(string value)
        {
            bool parseSuccess = int.TryParse(value, out int timeLeftInSeconds);
            if (!parseSuccess)
            {
                Debug.LogError("Invalid format for TimeLeft");
                return null;
            }
            return timeLeftInSeconds;
        }

        private static object ParseLevelSucceed(string value)
        {
            bool parseSuccess = int.TryParse(value, out int level);
            if (!parseSuccess)
            {
                Debug.LogError("Invalid format for LevelSucceed");
                return null;
            }
            return level;
        }

        private static object ParseOfferToComplete(string value)
        {
            bool parseSuccess = Guid.TryParse(value, out Guid offerUuid);
            if (!parseSuccess)
            {
                Debug.LogError("Invalid format for OfferToComplete");
                return null;
            }
            return offerUuid;
        }

        private static object ParseFeatureUnlocked(string value)
        {
            return value;
        }
    }
}

