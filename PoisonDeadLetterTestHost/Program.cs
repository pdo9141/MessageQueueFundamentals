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
            try
            {
                var baseAddress = new Uri("net.msmq://localhost/private");
                serviceHost = new ServiceHost(
                    typeof(SayHelloService),
                    baseAddress);

                var netMsmqBinding = new NetMsmqBinding(NetMsmqSecurityMode.None);

                serviceHost.AddServiceEndpoint(
                    typeof(ISayHelloService),
                    netMsmqBinding,
                    "/hello");

                serviceHost.Open();

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
                if (null != serviceHost &&
                    CommunicationState.Opened == serviceHost.State)
                {
                    serviceHost.Close();
                }
            }
        }
    }
}
