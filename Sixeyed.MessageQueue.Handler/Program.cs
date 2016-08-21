using System;
using System.IO;
using Newtonsoft.Json;
using Sixeyed.MessageQueue.Messages.Commands;
using Sixeyed.MessageQueue.Integration.Workflows;
using msmq = System.Messaging;

namespace Sixeyed.MessageQueue.Handler
{
    class Program
    {
        static void Main(string[] args)
        {
            // This should be a windows service but using console for output
            using (var queue = new msmq.MessageQueue(".\\private$\\sixeyed.messagequeue.unsubscribe.tx"))
            {
                while (true)
                {
                    Console.WriteLine("Listening");
                    using (var tx = new msmq.MessageQueueTransaction())
                    {
                        tx.Begin();
                        var message = queue.Receive(tx);  // This will block until another queue item is available
                        var bodyReader = new StreamReader(message.BodyStream);
                        var jsonBody = bodyReader.ReadToEnd();
                        var unsubscribeMessage = JsonConvert.DeserializeObject<UnsubscribeCommand>(jsonBody);
                        var workflow = new UnsubscribeWorkflow(unsubscribeMessage.EmailAddress);
                        Console.WriteLine("Starting unsubscribe for: {0}", unsubscribeMessage.EmailAddress);
                        workflow.Run();
                        Console.WriteLine("Unsubscribe complete for: {0}", unsubscribeMessage.EmailAddress);
                        tx.Commit();
                    }
                }
            }
        }
    }
}
