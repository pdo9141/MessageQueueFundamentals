using System;
using System.ServiceModel;
using MsmqContract;

namespace PoisonDeadLetterTestHost
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost serviceHost = null;
            ServiceHost poisonMessageServiceHost = null;

            try
            {
                var baseAddress = new Uri("net.msmq://localhost/private");
                serviceHost = new ServiceHost(typeof(SayHelloService), baseAddress);

                var netMsmqBinding = new NetMsmqBinding(NetMsmqSecurityMode.None)
                {
                    ReceiveRetryCount = 0,
                    ReceiveErrorHandling = ReceiveErrorHandling.Move,
                    MaxRetryCycles = 1,
                    RetryCycleDelay = TimeSpan.FromSeconds(60.0)
                };

                serviceHost.AddServiceEndpoint(typeof(ISayHelloService), netMsmqBinding, "/hello");
                serviceHost.Open();

                // Start poison Message Listener
                poisonMessageServiceHost = new ServiceHost(typeof(SayHelloPoisonMessageService));
                netMsmqBinding = new NetMsmqBinding(NetMsmqSecurityMode.None);

                poisonMessageServiceHost.AddServiceEndpoint(
                    typeof(ISayHelloService),
                    netMsmqBinding,
                    new Uri("net.msmq://localhost/private/hello;poison"));

                poisonMessageServiceHost.Open();
                // End poison Message Listener

                Console.Error.WriteLine("The service is listening for requests. Press Enter to exit.");
                Console.In.ReadLine();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("\nERROR: {0}\n\nPress Enter to exit.");
                Console.In.ReadLine();
            }
            finally
            {
                if (null != serviceHost && CommunicationState.Opened == serviceHost.State)
                {
                    serviceHost.Close();
                }

                if (null != poisonMessageServiceHost && CommunicationState.Opened == serviceHost.State)
                {
                    poisonMessageServiceHost.Close();
                }
            }
        }
    }
}
