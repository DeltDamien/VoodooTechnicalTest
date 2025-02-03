using PersonalizedOffersSdk.Offers;
using System.Collections.Generic;
using UnityEngine;

namespace PersonalizedOffersSdk.Scripts.Sample
{
    // TODO : label should be key for localization
    [System.Serializable]
    public class TypeToLabel
    {
        [SerializeField]
        public OfferType offerType;
        [SerializeField]
        public string label;
    }


    [CreateAssetMenu(fileName = "TriggerTypeLabelMap", menuName = "Scriptable Objects/SOTriggerTypeLabelMap")]
    public class SOOfferTypeLabelMap : ScriptableObject
    {
        [SerializeField]
        private List<TypeToLabel> _offerTypeToLabel;

        public List<TypeToLabel> OfferTypeToLabel => _offerTypeToLabel;

        public SOOfferTypeLabelMap()
        {
            _offerTypeToLabel = new List<TypeToLabel>();
        }
    }
}
