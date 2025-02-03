using Cysharp.Threading.Tasks;
using PersonalizedOffersSdk.Offers;
using PersonalizedOffersSdk.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace PersonalizedOffersSdk.Controller
{
    public class PersonalizedOffersController
    {
        private readonly Guid _playerUuid;
        private readonly IPersonalizedOffersService _personalizedOffersService;
        // TODO : cache by uuid offers these one to access it faster if needed
        private List<Offer> _offers;

        public PersonalizedOffersController(Guid playerUuid, IPersonalizedOffersService personalizedOffersService)
        {
            _offers = new List<Offer>();
            _playerUuid = playerUuid;
            _personalizedOffersService = personalizedOffersService;
        }

        public List<Offer> GetOffers()
        {
            return _offers;
        }


        #region TriggerReceived
        public async UniTask OnTriggerReceivedAsync(TriggerType trigger)
        {
            List<OfferData> triggeredOffers = await _personalizedOffersService.GetTriggeredOffersAsync(_playerUuid, trigger);
            AddTrigeredOffer(triggeredOffers);
        }

        private void AddTrigeredOffer(List<OfferData> triggeredOffers)
        {
            if (triggeredOffers != null)
            {
                HashSet<Guid> existingOfferUuids = new HashSet<Guid>(_offers.ConvertAll(o => o.GetUuid()));

                // an update on the offer should be a new uuid, but that's open to discussion
                for (int i = 0; i < triggeredOffers.Count; i++)
                {
                    OfferData offerData = triggeredOffers[i];

                    if (!existingOfferUuids.Contains(offerData.uuid))
                    {
                        Offer offer = new Offer(triggeredOffers[i]);
                        _offers.Add(offer);
                    }
                }
            }
        }
        #endregion

        #region ValidatePurchaseOffer
        public async UniTask<bool> ValidatePurchaseOfferAsync(Guid offerUuid)
        {
            bool isPurchaseValid = false;
            var offer = _offers.Find(o => o.GetUuid() == offerUuid);
            if (offer != null)
            {

                isPurchaseValid = await _personalizedOffersService.ValidatePurchaseOfferAsync(_playerUuid, offerUuid);


                if (isPurchaseValid)
                {
                    offer.MarkAsBought();
                }
            }

            return isPurchaseValid;
        }
        #endregion

        #region CancelledOffer
        public async UniTask<bool> CancelledOfferAsync(Guid offerUuid)
        {
            bool isCancelled = false;
            var offer = _offers.Find(o => o.GetUuid() == offerUuid);
            if (offer != null)
            {
                isCancelled = await _personalizedOffersService.CancelledOfferAsync(_playerUuid, offerUuid);

                if (isCancelled)
                {
                    offer.MarkAsConditionMet();
                }
            }

            return isCancelled;
        }
        #endregion

        #region OffersValidation

        // TODO : maybe an endpoint for one offer or list of offer could be add to optimize this on backend side
        public async UniTask<List<Guid>> GetValidOffersAsync(List<Guid> offersUuid)
        {
            List<Guid> allValidOffers = await _personalizedOffersService.GetValidOffersAsync(_playerUuid);
            return offersUuid.FindAll(o => allValidOffers.Contains(o));
        }

        public async UniTask<bool> IsOfferValidAsync(Guid offerGuid)
        {
           List<Guid> validOffers =  await GetValidOffersAsync(new List<Guid>() { offerGuid});
            return validOffers.Contains(offerGuid);
        }

        public async UniTask UpdateOffersValidationAsync()
        {
            List<Guid> offersUid = await _personalizedOffersService.GetValidOffersAsync(_playerUuid);
            HashSet<Guid> validOffersSet = new HashSet<Guid>(offersUid);

            _offers.RemoveAll(offer => !validOffersSet.Contains(offer.GetUuid()));
        }

        #endregion


        public bool IsOfferHasLinkedOffers(Guid guid)
        {
            return _offers.Find(o => o.GetUuid() == guid).GetLinkedOffers().Count > 0;
        }

        public bool isOfferLinkedOffers(Guid guid)
        {
            return _offers.Find(o => o.GetLinkedOffers().Contains(guid)) != null;
        }

        public OfferType GetOfferType(Guid guid)
        {
            return _offers.Find(o => o.GetUuid() == guid).GetOfferType();
        }

    }
}
