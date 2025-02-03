namespace PersonalizedOffersSdk.Offers.ValidationConditions
{
    public readonly struct ValidationCondition
    {
        public readonly ValidationConditionType ValidationConditionType;
        public readonly string Value;

        public ValidationCondition(ValidationConditionType type, string value)
        {
            ValidationConditionType = type;
            Value = value;
        }
    }
}
