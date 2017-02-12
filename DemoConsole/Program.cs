using System;
using System.Messaging;
using System.Diagnostics;
using System.Collections.Generic;

namespace DemoConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                SendMessageToMsmq();
                //CreateQueues();
                //SendDefaultMessages();
                //SendDurableMessages();
                //SendTransactionalMessages();
                //SendMessagesOneTransaction();
                //SendSecureMessages();
                //PurgeMessagesOnAllQueues();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.Read();
        }

        private static void SendMessageToMsmq()
        {
            MessageQueue queue = new MessageQueue(@".\private$\msmqservice/msmqservice.svc");
            queue.Send("Hello MSMQ");
            Console.WriteLine("Message Sent");
        }

        private static void PurgeMessagesOnAllQueues()
        {
            var queues = new List<string>
            {
                @".\private$\default-queue",
                @".\private$\durable-queue",
                @".\private$\secure-queue",
                @".\private$\transactional-queue"
            };

            queues.ForEach(q => {
                var queue = new MessageQueue(q);
                queue.Purge();
            });

            Console.WriteLine("All Queues Purged");
        }

        private static void SendSecureMessages()
        {
            MessageQueue queue;
            Stopwatch stopwatch;

            queue = new MessageQueue(@".\private$\secure-queue");

            // The format name does not support the requested operation, requires Active Directory integration
            //queue.DefaultPropertiesToSend.UseEncryption = true;

            // Will get certificate error, requires Active Directory integration
            //queue.DefaultPropertiesToSend.UseAuthentication = true;

            stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < 1000; i++)
                queue.Send("Message: " + i);    // No error here but messages are not placed in queue

            Console.WriteLine("Secure ElapsedMilliseconds: {0}", stopwatch.ElapsedMilliseconds);
        }
        
        private static void SendMessagesOneTransaction()
        {
            MessageQueue queue;
            Stopwatch stopwatch;
            MessageQueueTransaction tx;

            queue = new MessageQueue(@".\private$\transactional-queue");
            stopwatch = Stopwatch.StartNew();

            tx = new MessageQueueTransaction();
            tx.Begin();
            for (int i = 0; i < 1000; i++)
                queue.Send("Message: " + i, tx);                
            
            tx.Commit();

            Console.WriteLine("Transactional ElapsedMilliseconds: {0}", stopwatch.ElapsedMilliseconds);
        }

        private static void SendTransactionalMessages()
        {
            MessageQueue queue;
            Stopwatch stopwatch;
            MessageQueueTransaction tx;

            queue = new MessageQueue(@".\private$\transactional-queue");
            stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < 1000; i++)
            {
                tx = new MessageQueueTransaction();
                tx.Begin();

                queue.Send("Message: " + i, tx);
                tx.Commit();
            }
            
            Console.WriteLine("Transactional ElapsedMilliseconds: {0}", stopwatch.ElapsedMilliseconds);
        }

        private static void SendDurableMessages()
        {
            MessageQueue queue;
            Stopwatch stopwatch;

            queue = new MessageQueue(@".\private$\durable-queue");
            queue.DefaultPropertiesToSend.Recoverable = true;   // Will be persisted to disk
            stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < 1000; i++)
                queue.Send("Message: " + i);

            Console.WriteLine("Durable ElapsedMilliseconds: {0}", stopwatch.ElapsedMilliseconds);
        }

        private static void SendDefaultMessages()
        {
            MessageQueue queue;
            Stopwatch stopwatch;

            queue = new MessageQueue(@".\private$\default-queue");
            stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < 1000; i++)
                queue.Send("Message: " + i);

            Console.WriteLine("Default ElapsedMilliseconds: {0}", stopwatch.ElapsedMilliseconds);
        }

        private static void CreateQueues()
        {
            //MessageQueue.Create(@".\private$\durable-queue");
            //MessageQueue.Create(@".\private$\default-queue");
            //MessageQueue.Create(@".\private$\transactional-queue", true);

            /*
            MessageQueue.Create(@".\private$\secure-queue", true);
            var secureQueue = new MessageQueue(@".\private$\secure-queue");
            secureQueue.Authenticate = true;
            secureQueue.EncryptionRequired = EncryptionRequired.Body;
            */
        }
    }
}
