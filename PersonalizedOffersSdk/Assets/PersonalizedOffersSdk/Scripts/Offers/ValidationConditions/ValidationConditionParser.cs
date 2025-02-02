using System.Collections.Generic;
using System;

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
            throw new ArgumentException($"No parser found for {type}");
        }

        private static object ParseTimeLeft(string value)
        {
            return int.TryParse(value, out int seconds) ? seconds : throw new FormatException("Format invalide pour TimeLeft");
        }

        private static object ParseLevelSucceed(string value)
        {
            return int.TryParse(value, out int level) ? level : throw new FormatException("Format invalide pour LevelSucceed");
        }

        private static object ParseOfferToComplete(string value)
        {
            return Guid.TryParse(value, out Guid uuid) ? uuid : throw new FormatException("Format invalide pour OfferToComplete");
        }

        private static object ParseFeatureUnlocked(string value)
        {
            return value;
        }
    }
}

