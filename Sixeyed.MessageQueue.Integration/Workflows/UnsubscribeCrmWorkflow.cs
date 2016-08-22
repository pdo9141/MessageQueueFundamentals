using System.Threading;

namespace Sixeyed.MessageQueue.Integration.Workflows
{
    public class UnsubscribeCrmWorkflow
    {
        public string EmailAddress { get; set; }

        public UnsubscribeCrmWorkflow(string emailAddress)
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
