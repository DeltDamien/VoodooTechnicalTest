using System;

namespace PersonalizedOffersSdk.Service
{
    [Serializable]
    internal readonly struct CancelledOfferRequest
    {
        public readonly string PlayerUuid;
        public readonly string OfferUuid;

        public CancelledOfferRequest(string playerUuid, string offerUuid)
        {
            PlayerUuid = playerUuid;
            OfferUuid = offerUuid;
        }
    }
}