namespace PersonalizedOffersSdk.Offer.ValidationCondition
{
    public class ValidationCondition
    {
        private readonly ValidationConditionType _type;
        private readonly string _value;

        public ValidationCondition(ValidationConditionType type, string value)
        {
            _type = type;
            _value = value;
        }

        public ValidationCondition(ValidationConditionData validationConditionData)
        {
            _type = validationConditionData.validationConditionType;
            _value = validationConditionData.value;
        }

        public ValidationConditionType GetConditionType()
        {
            return _type;
        }

        public string GetConditionValue()
        {
            return _value;
        }
    }
}
