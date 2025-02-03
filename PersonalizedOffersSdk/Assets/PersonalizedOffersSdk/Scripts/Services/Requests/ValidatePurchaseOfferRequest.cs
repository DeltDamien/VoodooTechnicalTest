using System;

namespace PersonalizedOffersSdk.Service
{
    [Serializable]
    internal readonly struct ValidatePurchaseOfferRequest
    {
        public readonly string PlayerUuid;
        public readonly string OfferUuid;

        public ValidatePurchaseOfferRequest(string playerUuid, string offerUuid)
        {
            this.PlayerUuid = playerUuid;
            this.OfferUuid = offerUuid;
        }
    }
}