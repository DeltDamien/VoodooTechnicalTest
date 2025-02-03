using PersonalizedOffersSdk.Offers.Prices;
using System.Collections.Generic;

namespace PersonalizedOffersSdk.Controller
{
    public class CurrencyController
    {
        private readonly List<CurrencyTypeToLabel> _currencyTypeToLabel;

        public CurrencyController(List<CurrencyTypeToLabel> currencyTypeToLabels)
        {
            _currencyTypeToLabel = currencyTypeToLabels;
        }

        public string GetPriceCurrencyLabel(CurrencyType currencyType)
        {
            return _currencyTypeToLabel.Find(x => x.currencyType == currencyType).label;
        }

        public string GetFinalPriceLabel(Price price)
        {
            float amount = price.GetFinalPrice();
            if (amount > 0)
            {
                return $"{amount} {GetPriceCurrencyLabel(price.CurrencyType)}";
            }
            else
            {
                return "Free";
            }
        }

        public string GetOriginalPriceLabel(Price price)
        {
            float amount = price.Amount;
            if (amount > 0)
            {
                return $"{amount} {GetPriceCurrencyLabel(price.CurrencyType)}";
            }
            else
            {
                return "Free";
            }
        }
    }
}
