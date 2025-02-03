using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using PersonalizedOffersSdk.Offers.Prices;
using PersonalizedOffersSdk.Offers.Rewards;
using PersonalizedOffersSdk.Offers.ValidationConditions;
using PersonalizedOffersSdk.Offers;
using PersonalizedOffersSdk.Service;
using PersonalizedOffersSdk;

public class MockPersonalizedOffersService : IPersonalizedOffersService
{
    private readonly List<OfferData> _mockOffers = new List<OfferData>
    {
        new OfferData
        {
            uuid = Guid.NewGuid(),
            title = "New Offer !",
            description = "Special for new players !",
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
            linkedOffers = new List<Guid>(),
            offerType = OfferType.Regular
        },
        new OfferData
        {
            uuid = Guid.Parse("e61bc7cf-b8d2-42be-aa99-3e2da2a838ef"),
            title = "Winter Offer",
            description = "Second cold time offer!",
            price = new PriceData { currencyType = CurrencyType.USD, amount = 399, discountPercent = 20 },
            rewards = new List<RewardData>
            {
                new RewardData { rewardType = RewardType.HardCurrency, amount = 500 }
            },
            validationConditions = new List<ValidationConditionData>
            {
                new ValidationConditionData { validationConditionType = ValidationConditionType.LevelPased, value = "10" }
            },
            startTime = DateTime.UtcNow,
            linkedOffers = new List<Guid>(),
            offerType = OfferType.Chained
        },
        new OfferData
        {
            uuid = Guid.Parse("e61bc7cf-b8d2-42be-aa99-3e2da2a838ea"),
            title = "Winter Offer",
            description = "First cold time offer!",
            rewards = new List<RewardData>
            {
                new RewardData { rewardType = RewardType.HardCurrency, amount = 5 }
            },
            validationConditions = new List<ValidationConditionData>
            {
                new ValidationConditionData { validationConditionType = ValidationConditionType.LevelPased, value = "10" }
            },
            startTime = DateTime.UtcNow,
            linkedOffers = new List<Guid>()
            {
                Guid.Parse("e61bc7cf-b8d2-42be-aa99-3e2da2a838ef")
            },
            offerType = OfferType.Chained
        }
    };

    private int _internalValidationCouter = 0;

    // TODO : OfferMockData could have a more complex structure including their trigger type
    public UniTask<List<OfferData>> GetTriggeredOffersAsync(Guid playerUuid, TriggerType trigger)
    {
        if (trigger == TriggerType.SessionStarted)
        {
            return UniTask.FromResult(new List<OfferData>() { _mockOffers[0] });
        } 
        else if (trigger == TriggerType.OfferPurchased)
        {
            return UniTask.FromResult(new List<OfferData>() { _mockOffers[2], _mockOffers[1] });
        } 
        else
        {
            return UniTask.FromResult(new List<OfferData>() {});
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
        if (_internalValidationCouter == 0)
        {
            _internalValidationCouter++;
            return UniTask.FromResult(new List<Guid>()
                {
                _mockOffers[0].uuid,  
            });
        } 
        else if (_internalValidationCouter == 1)
        {
            _internalValidationCouter++;
            return UniTask.FromResult(new List<Guid>()
                {
                _mockOffers[1].uuid,
                _mockOffers[2].uuid
            });
        } 
        else
        {
            _internalValidationCouter = 0;
            return UniTask.FromResult(new List<Guid>()
                {
                _mockOffers[2].uuid
            });
        }
    }
}