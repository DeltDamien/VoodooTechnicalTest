using PersonalizedOffersSdk.Offers.Prices;
using UnityEngine;

namespace PersonalizedOffersSdk
{
    [CreateAssetMenu(fileName = "PersonalizedOffersParameters", menuName = "PersonalizedOffersSDK/SO/PersonalizedOffersParameters")]
    public class PersonalizedOffersParameters : ScriptableObject
    {
        [SerializeField] private string _backendAdress;

        [SerializeField]
        private bool _immediateStartSanityCheck;

        [SerializeField]
        [Range(10, 1800)]
        private int _sanityCheckPeriodInSecond;

        [SerializeField]
        private SOCurrencyTypeToLabel _currencyTypeToLabel;


        public string BackendAdress => _backendAdress;

        public bool ImmediateStartSanityCheck => _immediateStartSanityCheck;

        public float SanityCheckPeriodInSecond => _sanityCheckPeriodInSecond;

        public SOCurrencyTypeToLabel CurrencyTypeToLabel => _currencyTypeToLabel;

    }
}