using Cysharp.Threading.Tasks;
using NUnit.Framework;
using System;
using System.Diagnostics;

namespace PersonalizedOffersSdk.Tests
{
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
        public void GetTriggeredOffersAsync_ReturnsOffersForValidTrigger()
        {
            var playerUuid = Guid.NewGuid();
            var trigger = TriggerType.SessionStarted;

            var result = _mockService.GetTriggeredOffersAsync(playerUuid, trigger).AsTask().Result;

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0, "No offers returned for valid trigger.");
        }

        [Test]
        public void ValidatePurchaseOfferAsync_ReturnsTrueForValidOffer()
        {
            var playerUuid = Guid.NewGuid();
            var validOfferUuid = _mockService.GetTriggeredOffersAsync(playerUuid, TriggerType.SessionStarted).AsTask().Result[0].uuid;


            var result = _mockService.ValidatePurchaseOfferAsync(playerUuid, validOfferUuid).AsTask().Result;

            Assert.IsTrue(result, "Validation failed for a valid offer.");
        }

        [Test]
        public void ValidatePurchaseOfferAsync_ReturnsFalseForInvalidOffer()
        {
            var playerUuid = Guid.NewGuid();
            var invalidOfferUuid = Guid.NewGuid(); // Random UUID that doesn't exist in the mock data

            var result = _mockService.ValidatePurchaseOfferAsync(playerUuid, invalidOfferUuid).AsTask().Result;

            Assert.IsFalse(result, "Validation passed for an invalid offer.");
        }

        [Test]
        public void CancelledOfferAsync_ReturnsTrueForValidOffer()
        {
            var playerUuid = Guid.NewGuid();
            var validOfferUuid = _mockService.GetTriggeredOffersAsync(playerUuid, TriggerType.SessionStarted).AsTask().Result[0].uuid;

            var result = _mockService.CancelledOfferAsync(playerUuid, validOfferUuid).AsTask().Result;

            Assert.IsTrue(result, "Cancellation failed for a valid offer.");
        }

        [Test]
        public void CancelledOfferAsync_ReturnsFalseForInvalidOffer()
        {
            var playerUuid = Guid.NewGuid();
            var invalidOfferUuid = Guid.NewGuid(); // Random UUID that doesn't exist in the mock data

            var result = _mockService.CancelledOfferAsync(playerUuid, invalidOfferUuid).AsTask().Result;

            Assert.IsFalse(result, "Cancellation passed for an invalid offer.");
        }

        [Test]
        public void GetValidOffersAsync_ReturnsListOfValidOfferUuids()
        {
            var playerUuid = Guid.NewGuid();

            var result = _mockService.GetValidOffersAsync(playerUuid).AsTask().Result;

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0, "No valid offers returned.");
        }

        [Test]
        public void GetTriggeredOffers_ReturnsOffersForValidTrigger()
        {
            var playerUuid = Guid.NewGuid();
            var trigger = TriggerType.SessionStarted;

            var result = _mockService.GetTriggeredOffers(playerUuid, trigger);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0, "No offers returned for valid trigger.");
        }

        [Test]
        public void ValidatePurchaseOffer_ReturnsTrueForValidOffer()
        {
            var playerUuid = Guid.NewGuid();
            var validOfferUuid = _mockService.GetTriggeredOffers(playerUuid, TriggerType.SessionStarted)[0].uuid;

            var result = _mockService.ValidatePurchaseOffer(playerUuid, validOfferUuid);

            Assert.IsTrue(result, "Validation failed for a valid offer.");
        }

        [Test]
        public void ValidatePurchaseOffer_ReturnsFalseForInvalidOffer()
        {
            var playerUuid = Guid.NewGuid();
            var invalidOfferUuid = Guid.NewGuid(); // Random UUID that doesn't exist in the mock data

            var result = _mockService.ValidatePurchaseOffer(playerUuid, invalidOfferUuid);

            Assert.IsFalse(result, "Validation passed for an invalid offer.");
        }

        [Test]
        public void CancelledOffer_ReturnsTrueForValidOffer()
        {
            var playerUuid = Guid.NewGuid();
            var validOfferUuid = _mockService.GetTriggeredOffers(playerUuid, TriggerType.SessionStarted)[0].uuid;

            var result = _mockService.CancelledOffer(playerUuid, validOfferUuid);

            Assert.IsTrue(result, "Cancellation failed for a valid offer.");
        }

        [Test]
        public void CancelledOffer_ReturnsFalseForInvalidOffer()
        {
            var playerUuid = Guid.NewGuid();
            var invalidOfferUuid = Guid.NewGuid(); // Random UUID that doesn't exist in the mock data

            var result = _mockService.CancelledOffer(playerUuid, invalidOfferUuid);

            Assert.IsFalse(result, "Cancellation passed for an invalid offer.");
        }

        [Test]
        public void GetValidOffers_ReturnsListOfValidOfferUuids()
        {
            var playerUuid = Guid.NewGuid();

            var result = _mockService.GetValidOffers(playerUuid);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0, "No valid offers returned.");
        }
    }
}