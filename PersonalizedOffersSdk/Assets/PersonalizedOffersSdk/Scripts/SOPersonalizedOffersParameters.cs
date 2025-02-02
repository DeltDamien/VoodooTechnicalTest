using UnityEngine;

namespace PersonalizedOffersSdk
{
    [CreateAssetMenu(fileName = "PersonalizedOffersParameters", menuName = "Scriptable Objects/PersonalizedOffersParameters")]
    public class PersonalizedOffersParameters : ScriptableObject
    {
        [SerializeField] private string _backendAdress;

        [SerializeField]
        [Range(10, 1800)]
        private int _sanityCheckPeriodInSecond;


        public string BackendAdress => _backendAdress;


        public float SanityCheckPeriodInSecond => _sanityCheckPeriodInSecond;

    }
}