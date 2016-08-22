using System.Threading;

namespace Sixeyed.MessageQueue.Integration.Workflows
{
    public class UnsubscribeFulfillmentWorkflow
    {
        public string EmailAddress { get; set; }

        public UnsubscribeFulfillmentWorkflow(string emailAddress)
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
