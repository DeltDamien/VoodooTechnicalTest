namespace PersonalizedOffersSdk.Offers.Prices
{
    public class Discount
    {
        public float Percentage { get; }

        public Discount(float percentage)
        {
            Percentage = percentage;
        }

        public string GetDiscountLabel()
        {
            return $"{Percentage}% off";
        }

        public float CalculateFinalPriceAmount(float originalPrice)
        {
            return originalPrice - (originalPrice * Percentage / 100);
        }

        public bool IsDiscountApplicable(float originalPrice)
        {
            return originalPrice > 0 && Percentage > 0;
        }
    }
}

