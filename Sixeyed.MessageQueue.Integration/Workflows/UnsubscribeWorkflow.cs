using System.Threading;

namespace Sixeyed.MessageQueue.Integration.Workflows
{
    public class UnsubscribeWorkflow
    {
        private const int StepDuration = 9000;

        public string EmailAddress { get; set; }

        public UnsubscribeWorkflow(string emailAddress)
        {
            EmailAddress = emailAddress;
        }

        public void Run()
        {
            PersistAsUnsubscribed();
            UnsubscribeInLegacySystem();
            SetCrmMailingPreference();
            CancelPendingMailshots();
        }

        private void PersistAsUnsubscribed()
        {
            Thread.Sleep(StepDuration);
        }

        private void UnsubscribeInLegacySystem()
        {
            Thread.Sleep(StepDuration);
        }

        private void SetCrmMailingPreference()
        {
            Thread.Sleep(StepDuration);
        }

        private void CancelPendingMailshots()
        {
            Thread.Sleep(StepDuration);
        }
    }
}
