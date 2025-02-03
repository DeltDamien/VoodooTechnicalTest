using System;

namespace PersonalizedOffersSdk.Offers.Prices
{
    public class Price
    {
        public readonly CurrencyType CurrencyType;
        public readonly float Amount;
        private readonly Discount _discount;

        public Price(CurrencyType currencyType, float amount, float discountPercent)
        {
            CurrencyType = currencyType;
            Amount = amount;
            _discount = new Discount(discountPercent);
        }

        public Price(PriceData priceData)
            : this(priceData.currencyType, priceData.amount, priceData.discountPercent) {
        }

        public string GetDiscountLabel() => _discount.GetDiscountLabel();

        public float GetFinalPrice() => _discount.CalculateFinalPriceAmount(Amount);
        public float GetDiscountPercent() => _discount.Percent;
    }
}
