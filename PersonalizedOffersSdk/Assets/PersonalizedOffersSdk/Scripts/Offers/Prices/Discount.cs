namespace PersonalizedOffersSdk.Offers.Prices
{
    public readonly struct Discount
    {
        public readonly float Percent;

        public Discount(float percent)
        {
            Percent = percent;
        }

        public string GetDiscountLabel()
        {
            return $"{Percent}% off !";
        }

        public float CalculateFinalPriceAmount(float originalPrice)
        {
            return originalPrice - (originalPrice * Percent / 100);
        }

        public bool IsDiscountApplicable(float originalPrice)
        {
            return originalPrice > 0 && Percent > 0;
        }
    }
}

