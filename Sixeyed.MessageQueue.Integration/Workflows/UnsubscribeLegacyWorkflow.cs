using System.Threading;

namespace Sixeyed.MessageQueue.Integration.Workflows
{
    public class UnsubscribeLegacyWorkflow
    {
        public string EmailAddress { get; set; }

        public UnsubscribeLegacyWorkflow(string emailAddress)
        {
            EmailAddress = emailAddress;
        }

        public void Run()
        {
            // Fake the funk
            Thread.Sleep(UnsubscribeWorkflow.StepDuration);
        }
    }
}
