using Cysharp.Threading.Tasks;
using System;
using System.Timers;

namespace PersonalizedOffersSdk.Controller
{
    public class PersonalizedOffersSanityCheckController
    {
        private PersonalizedOffersController _personalizedOfferController;

        // our game could have an internal timer system which call our backend (or not) we should use 
        private Timer _timer;

        public PersonalizedOffersSanityCheckController(PersonalizedOffersController personalizedOfferController, bool immediateStartSanityCheck, float checkInterval)
        {
            personalizedOfferController = _personalizedOfferController;
            _timer = new Timer(checkInterval * 1000);
            _timer.Elapsed += OnTimerElapsed;
            _timer.AutoReset = true;
            if (immediateStartSanityCheck)
            {
                StartSanityCheck();
            }
        }


        public void StartSanityCheck()
        {
            _timer.Start();
        }

        public void StopSanityCheck()
        {
            _timer.Stop();
        }

        private async void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            await UpdateAllOffersValidationAsync();
        }

        private async UniTask UpdateAllOffersValidationAsync()
        {
            await _personalizedOfferController.UpdateOffersValidationAsync();
        }

    }
}
