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
        public readonly Guid PlayerUuid;
        private readonly IPersonalizedOffersService _personalizedOffersService;

        // TODO : cache by uuid offers these one to access it faster if needed, for instance for linked offers find
        private List<Offer> _offers;

        public PersonalizedOffersController(Guid playerUuid, IPersonalizedOffersService personalizedOffersService)
        {
            _offers = new List<Offer>();
            PlayerUuid = playerUuid;
            _personalizedOffersService = personalizedOffersService;
        }

        public IReadOnlyList<Offer> GetOffers()
        {
            return _offers;
        }


        #region TriggerReceived
        public async UniTask OnTriggerReceivedAsync(TriggerType trigger)
        {
            List<OfferDto> triggeredOffers = await _personalizedOffersService.GetTriggeredOffersAsync(PlayerUuid, trigger);
            AddTrigeredOffer(triggeredOffers);
        }

        private void AddTrigeredOffer(List<OfferDto> triggeredOffers)
        {
            if (triggeredOffers != null)
            {
                HashSet<Guid> existingOfferUuids = new HashSet<Guid>(_offers.ConvertAll(o => o.OfferUuid));

                // an update on the offer should be a new uuid, but that's open to discussion
                for (int i = 0; i < triggeredOffers.Count; i++)
                {
                    OfferDto offerData = triggeredOffers[i];

                    if (!existingOfferUuids.Contains(offerData.OfferUUid))
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
            var offer = _offers.Find(o => o.OfferUuid == offerUuid);
            if (offer != null)
            {

                isPurchaseValid = await _personalizedOffersService.ValidatePurchaseOfferAsync(PlayerUuid, offerUuid);


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
            var offer = _offers.Find(o => o.OfferUuid == offerUuid);
            if (offer != null)
            {
                isCancelled = await _personalizedOffersService.CancelledOfferAsync(PlayerUuid, offerUuid);

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
            List<Guid> allValidOffers = await _personalizedOffersService.GetValidOffersAsync(PlayerUuid);
            return offersUuid.FindAll(o => allValidOffers.Contains(o));
        }

        public async UniTask<bool> IsOfferValidAsync(Guid offerGuid)
        {
           List<Guid> validOffers =  await GetValidOffersAsync(new List<Guid>() { offerGuid});
            return validOffers.Contains(offerGuid);
        }

        public async UniTask UpdateOffersValidationAsync()
        {
            List<Guid> offersUid = await _personalizedOffersService.GetValidOffersAsync(PlayerUuid);
            HashSet<Guid> validOffersSet = new HashSet<Guid>(offersUid);

            _offers.RemoveAll(offer => !validOffersSet.Contains(offer.OfferUuid));
        }

        #endregion


        public bool IsOfferHasLinkedOffers(Guid guid)
        {
            return _offers.Find(o => o.OfferUuid == guid).GetLinkedOffers().Count > 0;
        }

        public bool isOfferLinkedOffers(Guid guid)
        {
            for (int i = 0; i < _offers.Count; i++)
            {
                var linkedOffers = _offers[i].GetLinkedOffers();
                for (int j = 0; j < linkedOffers.Count; j++)
                {
                    if (linkedOffers[j] == guid)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public OfferType GetOfferType(Guid guid)
        {
            return _offers.Find(o => o.OfferUuid == guid).OfferType;
        }

    }
}
