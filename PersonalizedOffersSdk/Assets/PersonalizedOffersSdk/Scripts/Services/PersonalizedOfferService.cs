using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;
using PersonalizedOffersSdk.Offers;
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

        public async UniTask<List<OfferData>> GetTriggeredOffersAsync(Guid playerUuid, TriggerType trigger)
        {
            UnityWebRequest request = CreateRequest(_backendAdress + _triggeredOffersEndpoint, "POST", JsonUtility.ToJson(new TriggeredOffersRequest
            {
                playerUuid = playerUuid,
                trigger = trigger
            }));

             await request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Failed to fetch triggered offers: {request.error}");
                return null;            }
            else
            {
                TriggeredOffersResponse response = JsonUtility.FromJson<TriggeredOffersResponse>(request.downloadHandler.text);
                return response.offers.ToList();
            }
        }


        #endregion

        #region ValidatePurchaseOffer

        public async UniTask<bool> ValidatePurchaseOfferAsync(Guid playerUuid, Guid offerUuid)
        {
            UnityWebRequest request = CreateRequest(_backendAdress + _validatePurchaseEndpoint, "POST", JsonUtility.ToJson(new ValidateOfferRequest
            {
                playerUuid = playerUuid,
                offerUuid = offerUuid
            }));

            await request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Failed to validate purchase offer: {request.error}");
                return false;
            }
            else
            {
                ValidateOfferResponse response = JsonUtility.FromJson<ValidateOfferResponse>(request.downloadHandler.text);
                return response.success;
            }
        }

        #endregion


        #region CancelledOffer

        public async UniTask<bool> CancelledOfferAsync(Guid playerUuid, Guid offerUuid)
        {
            UnityWebRequest request = CreateRequest(_backendAdress + _cancelOfferEndpoint, "POST", JsonUtility.ToJson(new CancelledOfferRequest
            {
                playerUuid = playerUuid.ToString(),
                offerUuid = offerUuid.ToString()
            }));

            await request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Failed to cancel offer: {request.error}");
                return false;
            }
            else
            {
                CancelledOfferResponse response = JsonUtility.FromJson<CancelledOfferResponse>(request.downloadHandler.text);
                return response.success;
            }
        }

        #endregion

        #region GetValidOffers

        public async UniTask<List<Guid>> GetValidOffersAsync(Guid playerUuid)
        {
            UnityWebRequest request = CreateRequest(_backendAdress + _validOffersEndpoint, "GET");
            await request.SendWebRequest();
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Failed to fetch valid offers: {request.error}");
                return null;
            }
            else
            {
                ValidOffersResponse response = JsonUtility.FromJson<ValidOffersResponse>(request.downloadHandler.text);
                return response.offersUuid.ToList();
            }
        }

        #endregion
    }
}