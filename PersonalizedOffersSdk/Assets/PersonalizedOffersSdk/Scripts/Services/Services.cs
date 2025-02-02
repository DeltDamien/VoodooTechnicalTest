using System;

namespace PersonalizedOffersSdk.Service
{
    public class Services
    {
        private IPersonalizedOffersService _personalizedOffersService;

        public Services(Guid authToken, string backendAdress, bool isDebug = false)
        {
            if (isDebug)
            {
                _personalizedOffersService = new MockPersonalizedOffersService();
            } else
            {
                _personalizedOffersService = new PersonalizedOffersService(authToken, backendAdress);
            }
        }

        public IPersonalizedOffersService GetPersonalizedOffersService()
        {
            return _personalizedOffersService;
        }
    }
}
