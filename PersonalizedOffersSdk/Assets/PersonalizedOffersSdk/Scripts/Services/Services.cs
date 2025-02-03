using System;

namespace PersonalizedOffersSdk.Service
{
    public class Services
    {
        public readonly IPersonalizedOffersService PersonalizedOffersService;

        public Services(Guid authToken, string backendAdress, bool isDebug = false)
        {
            if (isDebug)
            {
                PersonalizedOffersService = new MockPersonalizedOffersService();
            } else
            {
                PersonalizedOffersService = new PersonalizedOffersService(authToken, backendAdress);
            }
        }
    }
}
