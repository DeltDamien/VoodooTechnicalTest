using System;

namespace PersonalizedOffersSdk.Offers.Prices
{
    public struct Price
    {
        public readonly CurrencyType CurrencyType;
        public readonly float Amount;
        private readonly Discount _discount;

        public Price(CurrencyType currencyType, float amount, float discountPercent)
        {
            CurrencyType = currencyType;
            Amount = amount;
            if (discountPercent > 0)
            {
                _discount = new Discount(discountPercent);
            }
            else
            {
                _discount = null;
            }
        }

        public readonly bool HasDiscount() => _discount != null;

        public readonly string GetDiscountLabel()
        {
            if (HasDiscount())
            {
                return _discount.GetDiscountLabel();
            }
            return string.Empty;
        }

        public readonly float GetFinalPrice()
        {
            if (HasDiscount())
            {
                return _discount.CalculateFinalPriceAmount(Amount);
            }
            return Amount;
        }

        public readonly float GetDiscountPercent()
        {
            if (HasDiscount())
            {
                return _discount.Percent;
            }
            return 0;
        }
    }
}
