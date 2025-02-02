using Cysharp.Threading.Tasks;
using System;
using System.Timers;

namespace PersonalizedOffersSdk.Controller
{
    public class PersonalizedOffersSanityCheckController
    {
        private PersonalizedOffersController _personalizedOfferController;

        // our game could have an internal timer system which call our backend (or not)  we should use 
        private Timer _timer;

        public PersonalizedOffersSanityCheckController(float checkInterval)
        {
            _timer = new Timer(checkInterval * 1000);
            _timer.Elapsed += OnTimerElapsed;
            _timer.AutoReset = true;
        }

        public void InjectPersonalizedOfferController(PersonalizedOffersController personalizedOfferController)
        {
            _personalizedOfferController = personalizedOfferController;
        }

        public void StartSanityCheck()
        {
            _timer.Start();
        }

        public void StopSanityCheck()
        {

        }

        private async void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            await UpdateAllOffersValidationAsync();
        }

        private async UniTask UpdateAllOffersValidationAsync()
        {
            if (_personalizedOfferController != null)
            {
                await _personalizedOfferController.UpdateOffersValidationAsync();
            }
            else
            {
                _timer.Stop();
                throw new Exception("PersonalizedOfferController is not injected");
            }
        }

    }
}
