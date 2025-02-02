using PersonalizedOffersSdk.Offers;
using System;

//TODO : Break into separate files by endpoints later

namespace PersonalizedOffersSdk.Service
{
    [Serializable]
    internal struct TriggeredOffersRequest
    {
        public Guid playerUuid;
        public TriggerType trigger;
    }

    [Serializable]
    internal struct TriggeredOffersResponse
    {
        public OfferData[] offers;
    }

    [Serializable]
    internal struct ValidateOfferRequest
    {
        public Guid playerUuid;
        public Guid offerUuid;
    }

    [Serializable]
    internal struct ValidateOfferResponse
    {
        public bool success;
    }

    [Serializable]
    internal struct CancelledOfferRequest
    {
        public string playerUuid;
        public string offerUuid;
    }

    [Serializable]
    internal struct CancelledOfferResponse
    {
        public bool success;
    }

    [Serializable]
    public struct ValidOffersResponse
    {
        public Guid[] offersUuid;
    }
}
