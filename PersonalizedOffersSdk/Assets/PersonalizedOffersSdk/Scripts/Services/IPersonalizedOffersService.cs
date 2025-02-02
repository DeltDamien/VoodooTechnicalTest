using Cysharp.Threading.Tasks;
using PersonalizedOffersSdk.Offers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalizedOffersSdk.Service
{
    public interface IPersonalizedOffersService
    {
        UniTask<List<OfferData>> GetTriggeredOffersAsync(Guid playerUuid, TriggerType trigger);
        UniTask<bool> ValidatePurchaseOfferAsync(Guid playerUuid, Guid offerUuid);
        UniTask<bool> CancelledOfferAsync(Guid playerUuid, Guid offerUuid);
        UniTask<List<Guid>> GetValidOffersAsync(Guid playerUuid);

        List<OfferData> GetTriggeredOffers(Guid playerUuid, TriggerType trigger);
        bool ValidatePurchaseOffer(Guid playerUuid, Guid offerUuid);
        bool CancelledOffer(Guid playerUuid, Guid offerUuid);
        List<Guid> GetValidOffers(Guid playerUuid);
    }
}