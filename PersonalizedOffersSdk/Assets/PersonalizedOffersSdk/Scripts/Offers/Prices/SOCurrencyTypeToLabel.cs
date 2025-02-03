using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace PersonalizedOffersSdk.Offers.Prices
{
    [System.Serializable]
    public class CurrencyTypeToLabel
    {
        [SerializeField]
        public CurrencyType currencyType;
        [SerializeField]
        public string label;
    }

    [CreateAssetMenu(fileName = "CurrencyTypeToLabel", menuName = "Scriptable Objects/SOCurrencyTypeToLabel")]
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
