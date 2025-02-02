using Cysharp.Threading.Tasks;
using PersonalizedOffersSdk.Offer;
using PersonalizedOffersSdk.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;

namespace PersonalizedOffersSdk.Controller
{
    public class PersonalizedOffersController
    {
        private readonly PersonalizedOffersSanityCheckController _personalizedOffersSanityCheckController;
        private readonly Guid _playerUuid;
        private readonly IPersonalizedOffersService _personalizedOffersService;
        // TODO : cache by uuid offers these one to access it faster if needed
        private List<Offer.Offer> _offers;

        public PersonalizedOffersController(Guid playerUuid, IPersonalizedOffersService personalizedOffersService, PersonalizedOffersSanityCheckController personalizedOffersSanityCheckController)
        {
            _offers = new List<Offer.Offer>();
            _playerUuid = playerUuid;
            _personalizedOffersService = personalizedOffersService;
            _personalizedOffersSanityCheckController = personalizedOffersSanityCheckController;
            _personalizedOffersSanityCheckController.InjectPersonalizedOfferController(this);
            _personalizedOffersSanityCheckController.StartSanityCheck();
        }

        public List<Offer.Offer> GetOffers()
        {
            return _offers;
        }


        #region TriggerReceived
        public async UniTaskVoid OnTriggerReceivedAsync(TriggerType trigger)
        {
            List<OfferData> triggeredOffers = await _personalizedOffersService.GetTriggeredOffersAsync(_playerUuid, trigger);
            AddTrigeredOffer(triggeredOffers);
        }

        public void OnTriggerReceived(TriggerType trigger)
        {
            List<OfferData> triggeredOffers = _personalizedOffersService.GetTriggeredOffers(_playerUuid, trigger);
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
                        Offer.Offer offer = new Offer.Offer(triggeredOffers[i]);
                        _offers.Add(offer);
                    }
                }
            }
        }
        #endregion

        #region ValidatePurchaseOffer
        public async UniTask<bool> ValidatePurchaseOfferAsync(Guid offerUuid)
        {
            return await ValidatePurchaseOfferInternal(offerUuid, true);
        }

        public bool ValidatePurchaseOffer(Guid offerUuid)
        {
            return ValidatePurchaseOfferInternal(offerUuid, false).GetAwaiter().GetResult();
        }

        private async UniTask<bool> ValidatePurchaseOfferInternal(Guid offerUuid, bool isAsync)
        {
            bool isPurchaseValid = false;
            var offer = _offers.Find(o => o.GetUuid() == offerUuid);
            if (offer != null)
            {

                 isPurchaseValid = isAsync
                    ? await _personalizedOffersService.ValidatePurchaseOfferAsync(_playerUuid, offerUuid)
                    : _personalizedOffersService.ValidatePurchaseOffer(_playerUuid, offerUuid);

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
            return await CancelledOfferInternal(offerUuid, true);
        }

        public bool CancelledOffer(Guid offerUuid)
        {
            return CancelledOfferInternal(offerUuid, false).GetAwaiter().GetResult();
        }

        private async UniTask<bool> CancelledOfferInternal(Guid offerUuid, bool isAsync)
        {
            bool isCancelled = false;
            var offer = _offers.Find(o => o.GetUuid() == offerUuid);
            if (offer != null)
            {
                isCancelled = isAsync
                    ? await _personalizedOffersService.CancelledOfferAsync(_playerUuid, offerUuid)
                    : _personalizedOffersService.CancelledOffer(_playerUuid, offerUuid);

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

        public List<Guid> GetValidOffers(List<Guid> offersUuid)
        {
            List<Guid> allValidOffers = _personalizedOffersService.GetValidOffers(_playerUuid);
            return offersUuid.FindAll(o => allValidOffers.Contains(o));
        }

        public async UniTask<bool> IsOfferValidAsync(Guid offerGuid)
        {
           List<Guid> validOffers =  await GetValidOffersAsync(new List<Guid>() { offerGuid});
            return validOffers.Contains(offerGuid);
        }

        public bool IsOfferValid(Guid offerGuid)
        {
            List<Guid> validOffers = GetValidOffers(new List<Guid>() { offerGuid });
            return validOffers.Contains(offerGuid);
        }

        public async UniTask UpdateOffersValidationAsync()
        {

            HashSet<Guid> validOfferSet = new HashSet<Guid>(await _personalizedOffersService.GetValidOffersAsync(_playerUuid));

            _offers.RemoveAll(offer => !validOfferSet.Contains(offer.GetUuid()));
        }

        public void UpdateOffersValidation()
        {
            HashSet<Guid> validOfferSet = new HashSet<Guid>(_personalizedOffersService.GetValidOffers(_playerUuid));

            _offers.RemoveAll(offer => !validOfferSet.Contains(offer.GetUuid()));
        }


        #endregion

    }
}
