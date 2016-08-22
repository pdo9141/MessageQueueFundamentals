using System;
using Sixeyed.MessageQueue.Messages.Commands;
using Sixeyed.MessageQueue.Messages.Extensions;
using Sixeyed.MessageQueue.Integration.Workflows;
using Sixeyed.MessageQueue.Messages.Queries;
using Sixeyed.MessageQueue.Integration.Queries;
using msmq = System.Messaging;

namespace Sixeyed.MessageQueue.Handler
{
    class Program
    {
        static void Main(string[] args)
        {
            // This should be a windows service but using console for output
            var queueAddress = args != null && args.Length == 1 ? args[0] : ".\\private$\\sixeyed.messagequeue.unsubscribe";
            using (var queue = new msmq.MessageQueue(queueAddress))
            {
                while (true)
                {
                    Console.WriteLine("Listening on: {0}", queueAddress);
                    var message = queue.Receive();
                    var messageBody = message.BodyStream.ReadFromJson(message.Label);

                    // TODO - would use a factory/IOC/MEF for this:
                    if (messageBody.GetType() == typeof(UnsubscribeCommand))
                        Unsubscribe((UnsubscribeCommand)messageBody);
                    else if (messageBody.GetType() == typeof(DoesUserExistRequest))
                        CheckUserExists((DoesUserExistRequest)messageBody, message);
                }
            }

            /*
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
                        Console.WriteLine("Starting unsubscribe for: {0}, at: {1}", unsubscribeMessage.EmailAddress, DateTime.Now.TimeOfDay);
                        workflow.Run();
                        Console.WriteLine("Unsubscribe complete for: {0}, at: {1}", unsubscribeMessage.EmailAddress, DateTime.Now.TimeOfDay);
                        tx.Commit();
                    }
                }
            }
            */
        }

        private static void CheckUserExists(DoesUserExistRequest doesUserExistRequest, msmq.Message message)
        {
            Console.WriteLine("Starting CheckUserExists for: {0}, at: {1}", doesUserExistRequest.EmailAddress, DateTime.Now.TimeOfDay);
            var doesUserExistResponse = new DoesUserExistResponse
            {
                Exists = DoesUserExist.Execute(doesUserExistRequest.EmailAddress)
            };
            using (var queue = message.ResponseQueue)
            {
                var response = new msmq.Message();
                response.BodyStream = doesUserExistResponse.ToJsonStream();
                response.Label = doesUserExistResponse.GetMessageType();
                queue.Send(response);
            }
            Console.WriteLine("Returned: {0} for CheckUserExists for: {1}, at: {2}", doesUserExistResponse.Exists, doesUserExistRequest.EmailAddress, DateTime.Now.TimeOfDay);
        }

        private static void Unsubscribe(UnsubscribeCommand unsubscribeMessage)
        {
            Console.WriteLine("Starting unsubscribe for: {0}, at: {1}", unsubscribeMessage.EmailAddress, DateTime.Now.TimeOfDay);
            var workflow = new UnsubscribeWorkflow(unsubscribeMessage.EmailAddress);
            workflow.Run();
            Console.WriteLine("Unsubscribe complete for: {0}, at: {1}", unsubscribeMessage.EmailAddress, DateTime.Now.TimeOfDay);
        }
    }
}
