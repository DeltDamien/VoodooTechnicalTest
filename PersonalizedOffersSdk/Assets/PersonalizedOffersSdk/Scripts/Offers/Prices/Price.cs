using System;

namespace PersonalizedOffersSdk.Offers.Prices
{
    public class Price
    {
        private readonly Currency _currency;
        private readonly float _amount;
        private readonly Discount _discount; 

        public Price(Currency currency, int amount, float discountPercent)
        {
            _currency = currency ?? throw new ArgumentNullException(nameof(currency));
            _amount = amount;
            _discount = new Discount(discountPercent);
        }

        public Price(PriceData priceData)
        {
            _currency = new Currency(priceData.currencyType);
            _amount = priceData.amount;
            _discount = new Discount(priceData.discountPercent);
        }

        public string GetFinalPriceLabel()
        {
            return $"{_discount.CalculateFinalPriceAmount(_amount)} {_currency.getPriceCurrencyLabel()}";
        }

        public string GetOriginalPriceLabel()
        {
            return $"{_amount} {_currency.getPriceCurrencyLabel()}";
        }

        public string GetDiscountLabel()
        {
            return _discount.GetDiscountLabel();
        }

        public float GetFinalPrice()
        {
            return _discount.CalculateFinalPriceAmount(_amount);
        }

        public float GetOriginalPrice()
        {
            return _amount;
        }


    }
}
