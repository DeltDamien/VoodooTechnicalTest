using Cysharp.Threading.Tasks;
using NUnit.Framework;
using PersonalizedOffersSdk.Offers;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PersonalizedOffersSdk.Tests
{
    // TODO : All theses tests could be improved using not this mock service but another targetting our backend with debug parameters (or node.js or postman
    public class PersonalizedOffersServiceEditModeTests
    {
        private MockPersonalizedOffersService _mockService;

        [SetUp]
        public void Setup()
        {
            // Initialize the mock service before each test
            _mockService = new MockPersonalizedOffersService();
        }

        [Test]
        public void GetTriggeredOffersAsync_ReturnsOffersForSessionStartTrigger()
        {
            Guid playerUuid = Guid.NewGuid();
            TriggerType trigger = TriggerType.SessionStarted;

            List<OfferDto> result = _mockService.GetTriggeredOffersAsync(playerUuid, trigger).AsTask().Result;

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 1, "One offer should be triggered");
        }

        [Test]
        public void GetTriggeredOffersAsync_ReturnsOffersForOfferPurchased()
        {
            Guid playerUuid = Guid.NewGuid();
            TriggerType trigger = TriggerType.OfferPurchased;

            List<OfferDto> result = _mockService.GetTriggeredOffersAsync(playerUuid, trigger).AsTask().Result;

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 2, "Two offer should be triggered");
        }

        [Test]
        public void ValidatePurchaseOfferAsync_ReturnsTrueForValidOffer()
        {
            Guid playerUuid = Guid.NewGuid();
            Guid validOfferUuid = _mockService.GetTriggeredOffersAsync(playerUuid, TriggerType.SessionStarted).AsTask().Result[0].OfferUUid;

            bool result = _mockService.ValidatePurchaseOfferAsync(playerUuid, validOfferUuid).AsTask().Result;

            Assert.IsTrue(result, "Validation failed for a valid offer.");
        }


        [Test]
        public void CancelledOfferAsync_ReturnsTrueForValidOffer()
        {
            Guid playerUuid = Guid.NewGuid();
            Guid validOfferUuid = _mockService.GetTriggeredOffersAsync(playerUuid, TriggerType.SessionStarted).AsTask().Result[0].OfferUUid;

            bool result = _mockService.CancelledOfferAsync(playerUuid, validOfferUuid).AsTask().Result;

            Assert.IsTrue(result, "Cancellation failed for a valid offer.");
        }


        [Test]
        public void GetValidOffersAsync_ReturnsListOfValidOfferUuids()
        {
            Guid playerUuid = Guid.NewGuid();

            List<Guid> result = _mockService.GetValidOffersAsync(playerUuid).AsTask().Result;

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 1, "One valid offer should be returned");

            result = _mockService.GetValidOffersAsync(playerUuid).AsTask().Result;

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 2, "Two valid offers should be returned");

            result = _mockService.GetValidOffersAsync(playerUuid).AsTask().Result;

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count == 1, "One valid offer should be returned");
        }
    }
}