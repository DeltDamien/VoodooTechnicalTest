using UnityEngine;

namespace PersonalizedOffersSdk
{
    [CreateAssetMenu(fileName = "PersonalizedOffersParameters", menuName = "Scriptable Objects/PersonalizedOffersParameters")]
    public class PersonalizedOffersParameters : ScriptableObject
    {
        [SerializeField] private string _backendAdress;

        [SerializeField]
        private bool _immediateStartSanityCheck;

        [SerializeField]
        [Range(10, 1800)]
        private int _sanityCheckPeriodInSecond;

   


        public string BackendAdress => _backendAdress;

        public bool ImmediateStartSanityCheck => _immediateStartSanityCheck;

        public float SanityCheckPeriodInSecond => _sanityCheckPeriodInSecond;

    }
}