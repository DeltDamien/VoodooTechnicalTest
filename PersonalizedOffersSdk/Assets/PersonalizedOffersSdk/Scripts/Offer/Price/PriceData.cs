using System;

namespace PersonalizedOffersSdk.Offer.Price
{
    [Serializable]
    public struct PriceData
    {
        public CurrencyType currencyType;
        public float amount;
        public float discountPercent;
    }
}