using System;
using System.Web.Mvc;
using Sixeyed.MessageQueue.Messages.Queries;
using Sixeyed.MessageQueue.Messages.Commands;
using Sixeyed.MessageQueue.Messages.Extensions;
using Sixeyed.MessageQueue.Integration.Workflows;
using msmq = System.Messaging;

namespace Sixeyed.MessageQueue.Web.Controllers
{
    public class UnsubscribeController : Controller
    {
        // GET: Unsubscribe
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Submit(string emailAddress)
        {
            if (DoesUserExist(emailAddress))
            {
                StartUnsubscribe(emailAddress);
                return View("Confirmation");
            }

            return View("Unknown");
        }

        private bool DoesUserExist(string emailAddress)
        {
            // Better to create one response queue and leverage correlation IDs and get by correlation ID
            var responseAddress = Guid.NewGuid().ToString().Substring(0, 6);
            responseAddress = ".\\private$\\" + responseAddress;

            try
            {
                using (var responseQueue = msmq.MessageQueue.Create(responseAddress))
                {
                    var doesUserExistRequest = new DoesUserExistRequest { EmailAddress = emailAddress };
                    using (var requestQueue = new msmq.MessageQueue(".\\private$\\sixeyed.messagequeue.doesuserexist"))
                    {
                        var message = new msmq.Message();
                        message.BodyStream = doesUserExistRequest.ToJsonStream();
                        message.Label = doesUserExistRequest.GetMessageType();
                        message.ResponseQueue = responseQueue;
                        requestQueue.Send(message);
                    }

                    var response = responseQueue.Receive();
                    var responseBody = response.BodyStream.ReadFromJson<DoesUserExistResponse>();
                    return responseBody.Exists;
                }
            }
            finally
            {
                if (msmq.MessageQueue.Exists(responseAddress))
                    msmq.MessageQueue.Delete(responseAddress);
            }
        }

        private void StartUnsubscribe(string emailAddress)
        {
            var unsubscribeCommand = new UnsubscribeCommand
            {
                EmailAddress = emailAddress
            };

            using (var queue = new msmq.MessageQueue(".\\private$\\sixeyed.messagequeue.unsubscribe"))
            {
                var message = new msmq.Message();
                message.BodyStream = unsubscribeCommand.ToJsonStream();
                message.Label = unsubscribeCommand.GetMessageType();
                queue.Send(message);                
            }

            /*
            using (var queue = new msmq.MessageQueue(".\\private$\\sixeyed.messagequeue.unsubscribe.tx"))
            {
                // This uses default MSMQ XML serialization (215 bytes)
                //var message = new msmq.Message(unsubscribeCommand);

                // Use JSON serialization for smaller message body size in queue, 215 bytes to 36 bytes
                var message = new msmq.Message();
                var jsonBody = JsonConvert.SerializeObject(unsubscribeCommand);
                message.BodyStream = new MemoryStream(Encoding.Default.GetBytes(jsonBody));

                var tx = new msmq.MessageQueueTransaction();
                tx.Begin();
                queue.Send(message, tx);
                tx.Commit();
            }
            */
        }

        public ActionResult SubmitSync(string emailAddress)
        {
            var workflow = new UnsubscribeWorkflow(emailAddress);
            workflow.Run();
            return View("Confirmation");
        }
    }
}