using System;

namespace PersonalizedOffersSdk.Offers.Prices
{
    [Serializable]
    public struct PriceData
    {
        public CurrencyType currencyType;
        public float amount;
        public float discountPercent;
    }
}