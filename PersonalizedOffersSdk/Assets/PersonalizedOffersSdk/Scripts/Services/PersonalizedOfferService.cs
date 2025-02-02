using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;
using PersonalizedOffersSdk.Offer;
using PersonalizedOffersSdk.Service;
using PersonalizedOffersSdk;
using UnityEditor.PackageManager;
using System.Linq;
using UnityEditor.PackageManager.Requests;

namespace PersonalizedOffersSdk.Service
{
   
    public class PersonalizedOffersService : IPersonalizedOffersService
    {
        private const string _triggeredOffersEndpoint = "/api/offers/triggered";
        private const string _validatePurchaseEndpoint = "/api/offers/validatePurchase";
        private const string _cancelOfferEndpoint = "/api/offers/cancel";
        private const string _validOffersEndpoint = "/api/offers/valid";


        private readonly string _backendAdress;
        private readonly Guid _authToken;


        public PersonalizedOffersService(Guid authToken, string backendAdress)
        {
            _authToken = authToken;
            _backendAdress = backendAdress;
        }

        private UnityWebRequest CreateRequest(string url, string method, string jsonPayload = null)
        {
            UnityWebRequest request = new UnityWebRequest(url, method);
            byte[] bodyRaw = !string.IsNullOrEmpty(jsonPayload) ?  System.Text.Encoding.UTF8.GetBytes(jsonPayload) : null;
            if (bodyRaw != null)
            {
                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            }
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", "Bearer " + _authToken);
            return request;
        }

        #region TriggerRequest

        private List<OfferData> handleTriggerRequestResult(UnityWebRequest request)
        {
            if (request.result != UnityWebRequest.Result.Success)
            {
                throw new Exception($"Failed to fetch triggered offers: {request.error}");
            }
            else
            {
                TriggeredOffersResponse response = JsonUtility.FromJson<TriggeredOffersResponse>(request.downloadHandler.text);
                return response.offers.ToList();
            }
        }

        public async UniTask<List<OfferData>> GetTriggeredOffersAsync(Guid playerUuid, TriggerType trigger)
        {
            UnityWebRequest request = CreateRequest(_backendAdress + _triggeredOffersEndpoint, "POST", JsonUtility.ToJson(new TriggeredOffersRequest
            {
                playerUuid = playerUuid,
                trigger = trigger
            }));

            UnityWebRequestAsyncOperation operation = request.SendWebRequest();

            await operation;

            return handleTriggerRequestResult(request);
        }

        public List<OfferData> GetTriggeredOffers(Guid playerUuid, TriggerType trigger)
        {
            UnityWebRequest request = CreateRequest(_backendAdress + _triggeredOffersEndpoint, "POST", JsonUtility.ToJson(new TriggeredOffersRequest
            {
                playerUuid = playerUuid,
                trigger = trigger
            }));
            request.SendWebRequest();

            while (!request.isDone)
            {
                // TODO : add timeout
            }

            return handleTriggerRequestResult(request);
        }

        #endregion

        #region ValidatePurchaseOffer


        private bool handleValidatePurchaseOfferResult(UnityWebRequest request)
        {
            if (request.result != UnityWebRequest.Result.Success)
            {
                throw new Exception($"Failed to validate purchase offer: {request.error}");
            }
            else
            {
                ValidateOfferResponse response = JsonUtility.FromJson<ValidateOfferResponse>(request.downloadHandler.text);
                return response.success;
            }
        }

        public async UniTask<bool> ValidatePurchaseOfferAsync(Guid playerUuid, Guid offerUuid)
        {
            UnityWebRequest request = CreateRequest(_backendAdress + _validatePurchaseEndpoint, "POST", JsonUtility.ToJson(new ValidateOfferRequest
            {
                playerUuid = playerUuid,
                offerUuid = offerUuid
            }));

            UnityWebRequestAsyncOperation operation = request.SendWebRequest();
            await operation;

            return handleValidatePurchaseOfferResult(request);
        }

        public bool ValidatePurchaseOffer(Guid playerUuid, Guid offerUuid)
        {
            UnityWebRequest request = CreateRequest(_backendAdress + _validatePurchaseEndpoint, "POST", JsonUtility.ToJson(new ValidateOfferRequest
            {
                playerUuid = playerUuid,
                offerUuid = offerUuid
            }));
            request.SendWebRequest();

            while (!request.isDone)
            {
                // TODO : add timeout
            }

            return handleValidatePurchaseOfferResult(request);
        }

        #endregion


        #region CancelledOffer

        private bool handleCancelledOfferResult(UnityWebRequest request)
        {
            if (request.result != UnityWebRequest.Result.Success)
            {
                throw new Exception($"Failed to cancel offer: {request.error}");
            }
            else
            {
                CancelledOfferResponse response = JsonUtility.FromJson<CancelledOfferResponse>(request.downloadHandler.text);
                return response.success;
            }
        }


        public async UniTask<bool> CancelledOfferAsync(Guid playerUuid, Guid offerUuid)
        {
            UnityWebRequest request = CreateRequest(_backendAdress + _cancelOfferEndpoint, "POST", JsonUtility.ToJson(new CancelledOfferRequest
            {
                playerUuid = playerUuid.ToString(),
                offerUuid = offerUuid.ToString()
            }));

            UnityWebRequestAsyncOperation operation = request.SendWebRequest();
            await operation;

            return handleCancelledOfferResult(request);
        }

        public bool CancelledOffer(Guid playerUuid, Guid offerUuid)
        {
            UnityWebRequest request = CreateRequest(_backendAdress + _cancelOfferEndpoint, "POST", JsonUtility.ToJson(new CancelledOfferRequest
            {
                playerUuid = playerUuid.ToString(),
                offerUuid = offerUuid.ToString()
            }));
            request.SendWebRequest();

            while (!request.isDone)
            {
                // TODO : add timeout
            }

            return handleCancelledOfferResult(request);
        }

        #endregion

        #region GetValidOffers

        private List<Guid> handleValidOffersResult (UnityWebRequest request)
        {
            if (request.result != UnityWebRequest.Result.Success)
            {
                throw new Exception($"Failed to fetch valid offers: {request.error}");
            }
            else
            {
                ValidOffersResponse response = JsonUtility.FromJson<ValidOffersResponse>(request.downloadHandler.text);
                return response.offersUuid.ToList();
            }
        }

        public async UniTask<List<Guid>> GetValidOffersAsync(Guid playerUuid)
        {
            UnityWebRequest unityWebRequest = CreateRequest(_backendAdress + _validOffersEndpoint, "GET");
            UnityWebRequestAsyncOperation operation = unityWebRequest.SendWebRequest();
            await operation;
            return handleValidOffersResult(unityWebRequest);
        }

        public List<Guid> GetValidOffers(Guid playerUuid)
        {
            UnityWebRequest unityWebRequest = CreateRequest(_backendAdress + _validOffersEndpoint, "GET");
            unityWebRequest.SendWebRequest();
            while (!unityWebRequest.isDone)
            {
                // TODO : add timeout
            }
            return handleValidOffersResult(unityWebRequest);
        }

        #endregion
    }
}