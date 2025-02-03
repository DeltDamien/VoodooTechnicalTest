namespace PersonalizedOffersSdk.Offers.Prices
{
    public class Discount
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

    }
}

