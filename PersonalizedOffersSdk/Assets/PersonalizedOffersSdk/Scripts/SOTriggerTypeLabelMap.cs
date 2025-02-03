using UnityEngine;

namespace PersonalizedOffersSdk.Scripts.Sample
{
    [System.Serializable]
    public class TypeToLabel
    {
        [SerializeField]
        public Type type;
        [SerializeField]
        public string label;
    }

    [CreateAssetMenu(fileName = "SOTriggerTypeLabelMap", menuName = "Scriptable Objects/SOTriggerTypeLabelMap")]
    public class SOTriggerTypeLabelMap : ScriptableObject
    {

    }
}
