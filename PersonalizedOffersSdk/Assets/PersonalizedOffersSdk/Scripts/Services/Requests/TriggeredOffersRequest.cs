using PersonalizedOffersSdk.Offers;
using System;

namespace PersonalizedOffersSdk.Service
{
    [Serializable]
    internal readonly struct TriggeredOffersRequest
    {
        public readonly Guid PlayerUUid;
        public readonly TriggerType Trigger;

        public TriggeredOffersRequest(Guid playerUuid, TriggerType trigger)
        {
            this.PlayerUUid = playerUuid;
            this.Trigger = trigger;
        }
    }
}
