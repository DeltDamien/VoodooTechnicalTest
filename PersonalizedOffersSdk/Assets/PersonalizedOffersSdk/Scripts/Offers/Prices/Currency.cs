using System.Collections.Generic;

namespace PersonalizedOffersSdk.Offers.Prices
{
    public class Currency
    {
        public CurrencyType CurrencyType { get; private set; }


        // TODO : should be key for translation purpose and no plain text
        private readonly IReadOnlyDictionary<CurrencyType, string> Labels = new Dictionary<CurrencyType, string>
        {
            { CurrencyType.SoftCurrency, "Soft Currency" },
            { CurrencyType.HardCurrency, "Hard Currency" },
            { CurrencyType.EUR, "Euro" },
            { CurrencyType.USD, "US Dollar" },
            { CurrencyType.GBP, "British Pound" }
        };

        public Currency(CurrencyType currencyType)
        {
            CurrencyType = currencyType;
        }

        public string getPriceCurrencyLabel()
        {
            return Labels[this.CurrencyType];
        }
    }
}