using System.IO;
using System.Text;
using System.Web.Mvc;
using Newtonsoft.Json;
using Sixeyed.MessageQueue.Messages.Commands;
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
            var unsubscribeCommand = new UnsubscribeCommand
            {
                EmailAddress = emailAddress
            };

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

            return View("Confirmation");
        }
    }
}