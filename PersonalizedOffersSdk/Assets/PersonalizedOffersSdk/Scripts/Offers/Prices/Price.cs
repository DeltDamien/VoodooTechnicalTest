using System;

namespace PersonalizedOffersSdk.Offers.Prices
{
    public class Price
    {
        private readonly Currency _currency;
        private readonly float _amount;
        private readonly Discount _discount;

        public Price(Currency currency, float amount, float discountPercent)
        {
            _currency = currency ?? new Currency(CurrencyType.USD);
            _amount = amount;
            _discount = new Discount(discountPercent);
        }

        public Price(PriceData priceData)
            : this(new Currency(priceData.currencyType), priceData.amount, priceData.discountPercent) { }

        public string GetFinalPriceLabel() => $"{GetFinalPrice()} {_currency.GetPriceCurrencyLabel()}";

        public string GetOriginalPriceLabel() => $"{_amount} {_currency.GetPriceCurrencyLabel()}";

        public string GetDiscountLabel() => _discount.GetDiscountLabel();

        public float GetFinalPrice() => _discount.CalculateFinalPriceAmount(_amount);

        public float GetOriginalPrice() => _amount;
    }
}
