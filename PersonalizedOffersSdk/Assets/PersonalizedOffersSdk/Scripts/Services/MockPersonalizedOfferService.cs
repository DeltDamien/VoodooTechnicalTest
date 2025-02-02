using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using PersonalizedOffersSdk.Offer.Price;
using PersonalizedOffersSdk.Offer.Reward;
using PersonalizedOffersSdk.Offer.ValidationCondition;
using PersonalizedOffersSdk.Offer;
using PersonalizedOffersSdk.Service;
using PersonalizedOffersSdk;

public class MockPersonalizedOffersService : IPersonalizedOffersService
{
    private readonly List<OfferData> _mockOffers = new List<OfferData>
    {
        new OfferData
        {
            uuid = Guid.NewGuid(),
            title = "Welcome Offer",
            description = "Special offer for new players!",
            price = new PriceData { currencyType = CurrencyType.USD, amount = 9.99f },
            rewards = new List<RewardData>
            {
                new RewardData { rewardType = RewardType.Energy, amount = 100 }
            },
            validationConditions = new List<ValidationConditionData>
            {
                new ValidationConditionData { validationConditionType = ValidationConditionType.TimeLeft, value = "3600" }
            },
            startTime = DateTime.UtcNow,
            linkedOffers = new List<OfferData>()
        },
        new OfferData
        {
            uuid = Guid.NewGuid(),
            title = "Seasonal Offer",
            description = "Limited-time seasonal offer!",
            price = new PriceData { currencyType = CurrencyType.USD, amount = 399.99f, discountPercent = 20 },
            rewards = new List<RewardData>
            {
                new RewardData { rewardType = RewardType.HardCurrency, amount = 500 }
            },
            validationConditions = new List<ValidationConditionData>
            {
                new ValidationConditionData { validationConditionType = ValidationConditionType.LevelPased, value = "10" }
            },
            startTime = DateTime.UtcNow,
            linkedOffers = new List<OfferData>()
        }
    };

    private readonly List<Guid> _validOffers = new List<Guid>
    {
        Guid.NewGuid(),
        Guid.NewGuid()
    };

    // TODO : OfferMockData could have a more complex structure including their trigger type
    public UniTask<List<OfferData>> GetTriggeredOffersAsync(Guid playerUuid, TriggerType trigger)
    {
        if (trigger == TriggerType.SessionStarted)
        {
            return UniTask.FromResult(new List<OfferData>() { _mockOffers[0] });
        } else
        {
            return UniTask.FromResult(new List<OfferData>() { _mockOffers[1] });
        }
    }

    public UniTask<bool> ValidatePurchaseOfferAsync(Guid playerUuid, Guid offerUuid)
    {
        var isValid = _mockOffers.Exists(o => o.uuid == offerUuid);
        return UniTask.FromResult(isValid);
    }

    public UniTask<bool> CancelledOfferAsync(Guid playerUuid, Guid offerUuid)
    {
        var isCancelled = _mockOffers.Exists(o => o.uuid == offerUuid);
        return UniTask.FromResult(isCancelled);
    }

    public UniTask<List<Guid>> GetValidOffersAsync(Guid playerUuid)
    {
        return UniTask.FromResult(_validOffers);
    }

    public List<OfferData> GetTriggeredOffers(Guid playerUuid, TriggerType trigger)
    {
        return GetTriggeredOffersAsync(playerUuid, trigger).AsTask().GetAwaiter().GetResult();
    }

    public bool ValidatePurchaseOffer(Guid playerUuid, Guid offerUuid)
    {
        return ValidatePurchaseOfferAsync(playerUuid, offerUuid).AsTask().GetAwaiter().GetResult();
    }

    public bool CancelledOffer(Guid playerUuid, Guid offerUuid)
    {
        return CancelledOfferAsync(playerUuid, offerUuid).AsTask().GetAwaiter().GetResult();
    }

    public List<Guid> GetValidOffers(Guid playerUuid)
    {
        return GetValidOffersAsync(playerUuid).AsTask().GetAwaiter().GetResult();
    }
}