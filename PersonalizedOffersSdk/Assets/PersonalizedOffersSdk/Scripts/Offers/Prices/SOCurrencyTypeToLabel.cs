using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace PersonalizedOffersSdk.Offers.Prices
{
    [System.Serializable]
    public struct CurrencyTypeToLabel
    {
        [SerializeField]
        public CurrencyType CurrencyType;
        [SerializeField]
        public string Label;
    }

    [CreateAssetMenu(fileName = "CurrencyTypeToLabel", menuName = "PersonalizedOffersSDK/SO/CurrencyTypeToLabel")]
    public class SOCurrencyTypeToLabel : ScriptableObject
    {
        [SerializeField]
        private List<CurrencyTypeToLabel> _currencyTypeToLabel;

        public List<CurrencyTypeToLabel> CurrencyTypeToLabel => _currencyTypeToLabel;

        public SOCurrencyTypeToLabel()
        {
            _currencyTypeToLabel = new List<CurrencyTypeToLabel>();
        }
    }
}
