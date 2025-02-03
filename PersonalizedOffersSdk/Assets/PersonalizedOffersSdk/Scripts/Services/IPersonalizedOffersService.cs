using Cysharp.Threading.Tasks;
using PersonalizedOffersSdk.Offers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalizedOffersSdk.Service
{
    public interface IPersonalizedOffersService
    {
        UniTask<List<OfferDto>> GetTriggeredOffersAsync(Guid playerUuid, TriggerType trigger);
        UniTask<bool> ValidatePurchaseOfferAsync(Guid playerUuid, Guid offerUuid);
        UniTask<bool> CancelledOfferAsync(Guid playerUuid, Guid offerUuid);
        UniTask<List<Guid>> GetValidOffersAsync(Guid playerUuid);
    }
}