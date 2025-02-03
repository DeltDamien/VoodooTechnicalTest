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

        public string GetFinalPriceLabel ()
        {
            if (_amount > 0)
            {
               return $"{GetFinalPrice()} {_currency.GetPriceCurrencyLabel()}";
            } 
            else
            {
                return "Free";
            }
        }

        public string GetOriginalPriceLabel ()
        {
            if (_amount > 0)
            {
                return $"{_amount} {_currency.GetPriceCurrencyLabel()}";
            }
            else
            {
                return "Free";
            }
        }

        public string GetDiscountLabel() => _discount.GetDiscountLabel();

        public float GetFinalPrice() => _discount.CalculateFinalPriceAmount(_amount);

        public float GetOriginalPrice() => _amount;

        public float GetDiscountPercent() => _discount.Percent;
    }
}
