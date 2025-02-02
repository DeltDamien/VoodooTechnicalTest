using System;

namespace PersonalizedOffersSdk.Service
{
    public class Services
    {
        private IPersonalizedOffersService _personalizedOffersService;

        Services(Guid authToken, string baseUrl, bool isDebug = false)
        {
            if (isDebug)
            {
                _personalizedOffersService = new MockPersonalizedOffersService();
            } else
            {
                _personalizedOffersService = new PersonalizedOffersService(authToken, baseUrl);
            }
        }

        public IPersonalizedOffersService GetPersonalizedOffersService()
        {
            return _personalizedOffersService;
        }
    }
}
