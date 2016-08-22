using System.Threading;
using msmq = System.Messaging;
using Sixeyed.MessageQueue.Messages.Event;
using Sixeyed.MessageQueue.Messages.Extensions;

namespace Sixeyed.MessageQueue.Integration.Workflows
{
    public class UnsubscribeWorkflow
    {
        public const int StepDuration = 1000;

        public string EmailAddress { get; set; }

        public UnsubscribeWorkflow(string emailAddress)
        {
            EmailAddress = emailAddress;
        }

        public void Run()
        {
            PersistAsUnsubscribed();
            NotifyUserUnsubscribed();
        }

        private void PersistAsUnsubscribed()
        {
            // Fake the funk, persist to DB here
            Thread.Sleep(StepDuration);
        }

        private void NotifyUserUnsubscribed()
        {
            var unsubscribedEvent = new UserUnsubscribed { EmailAddress = EmailAddress };
            using (var queue = new msmq.MessageQueue("FormatName:MULTICAST=234.1.1.2:8001"))
            {
                var message = new msmq.Message();
                message.BodyStream = unsubscribedEvent.ToJsonStream();
                message.Label = unsubscribedEvent.GetMessageType();
                message.Recoverable = true;
                queue.Send(message);
            }
        }                
    }
}
