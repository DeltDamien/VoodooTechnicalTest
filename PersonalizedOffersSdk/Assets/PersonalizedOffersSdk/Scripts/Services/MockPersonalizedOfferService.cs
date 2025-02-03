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
    private readonly List<OfferDto> _mockOffers = new List<OfferDto>
    {
        new OfferDto(
            Guid.NewGuid(),
            "New Offer !",
            "Special for new players !",
            new Price(CurrencyType.USD, 9.99f, 0),
            new List<Reward>
            {
                new Reward(RewardType.Energy, 100)
            },
            new List<ValidationCondition>
            {
                new ValidationCondition(ValidationConditionType.TimeLeft, "3600")
            },
            DateTime.UtcNow,
            new List<Guid>(),
            OfferType.Regular
        ),
        new OfferDto(
            Guid.Parse("e61bc7cf-b8d2-42be-aa99-3e2da2a838ef"),
            "Winter Offer",
            "Second cold time offer!",
            new Price(CurrencyType.USD, 399f, 20),
            new List<Reward>
            {
                new Reward(RewardType.HardCurrency, 500)
            },
            new List<ValidationCondition>
            {
                new ValidationCondition(ValidationConditionType.LevelPassed, "10")
            },
            DateTime.UtcNow,
            new List<Guid>(),
            OfferType.Chained
        ),
        new OfferDto(
            Guid.Parse("e61bc7cf-b8d2-42be-aa99-3e2da2a838ea"),
            "Winter Offer",
            "First cold time offer!",
            new Price(CurrencyType.USD, 0f, 0), // Assuming a default price since it was missing in your original code
            new List<Reward>
            {
                new Reward(RewardType.SoftCurrency, 50)
            },
            new List<ValidationCondition>
            {
                new ValidationCondition(ValidationConditionType.LevelPassed, "10")
            },
            DateTime.UtcNow,
            new List<Guid>
            {
                Guid.Parse("e61bc7cf-b8d2-42be-aa99-3e2da2a838ef")
            },
            OfferType.Chained
        )
    };

    private int _internalValidationCouter = 0;

    // TODO : OfferMockData could have a more complex structure including their trigger type
    public UniTask<List<OfferDto>> GetTriggeredOffersAsync(Guid playerUuid, TriggerType trigger)
    {
        if (trigger == TriggerType.SessionStarted)
        {
            return UniTask.FromResult(new List<OfferDto>() { _mockOffers[0] });
        } 
        else if (trigger == TriggerType.OfferPurchased)
        {
            return UniTask.FromResult(new List<OfferDto>() { _mockOffers[2], _mockOffers[1] });
        } 
        else
        {
            return UniTask.FromResult(new List<OfferDto>() {});
        }
    }

    public UniTask<bool> ValidatePurchaseOfferAsync(Guid playerUuid, Guid offerUuid)
    {
        var isValid = _mockOffers.Exists(o => o.OfferUUid == offerUuid);
        return UniTask.FromResult(isValid);
    }

    public UniTask<bool> CancelledOfferAsync(Guid playerUuid, Guid offerUuid)
    {
        var isCancelled = _mockOffers.Exists(o => o.OfferUUid == offerUuid);
        return UniTask.FromResult(isCancelled);
    }

    public UniTask<List<Guid>> GetValidOffersAsync(Guid playerUuid)
    {
        if (_internalValidationCouter == 0)
        {
            _internalValidationCouter++;
            return UniTask.FromResult(new List<Guid>()
                {
                _mockOffers[0].OfferUUid,  
            });
        } 
        else if (_internalValidationCouter == 1)
        {
            _internalValidationCouter++;
            return UniTask.FromResult(new List<Guid>()
                {
                _mockOffers[1].OfferUUid,
                _mockOffers[2].OfferUUid
            });
        } 
        else
        {
            _internalValidationCouter = 0;
            return UniTask.FromResult(new List<Guid>()
                {
                _mockOffers[2].OfferUUid
            });
        }
    }
}