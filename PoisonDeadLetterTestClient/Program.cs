using System;
using System.ServiceModel;
using MsmqContract;

namespace PoisonDeadLetterTestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ChannelFactory<ISayHelloService> channelFactory = null;
            ISayHelloService service = null;
            ServiceHost deadLetterServiceHost = null;

            try
            {
                // When creating the WCF binding to an MSMQ queue, the client program can specify also what queue to 
                // write the dead letter messages to.MSMQ also provides a global system dead letter queue, but it's better practice 
                // to use an application-specific dead letter queue, as multiple applications can send dead letter messages to the global queue. 
                var netMsmqBinding = new NetMsmqBinding(NetMsmqSecurityMode.None)
                {
                    DeadLetterQueue = DeadLetterQueue.Custom,
                    CustomDeadLetterQueue = new Uri("net.msmq://localhost/private/hellodeadletter"),
                    TimeToLive = TimeSpan.FromMinutes(2.0)
                };
                
                var remoteAddress = new EndpointAddress("net.msmq://localhost/private/hello");

                channelFactory = new ChannelFactory<ISayHelloService>(netMsmqBinding, remoteAddress);
                channelFactory.Open();

                // Start dead letter queue listener
                var baseAddress = new Uri("net.msmq://localhost/private");
                deadLetterServiceHost = new ServiceHost(typeof(SayHelloDeadLetterService), baseAddress);

                netMsmqBinding = new NetMsmqBinding(NetMsmqSecurityMode.None);
                deadLetterServiceHost.AddServiceEndpoint(
                    typeof(ISayHelloService),
                    netMsmqBinding,
                    "/hellodeadletter");

                deadLetterServiceHost.Open();
                // End dead letter queue listener

                service = channelFactory.CreateChannel();
                var clientChannel = service as IClientChannel;
                if (null != clientChannel)
                {
                    clientChannel.Open();
                }

                Console.Error.WriteLine("Enter a name to send. Enter a blank line to exit.");
                while (true)
                {
                    var name = Console.In.ReadLine();
                    if (string.IsNullOrWhiteSpace(name))
                    {
                        break;
                    }

                    service.SayHello(name);
                }
            }
            finally
            {
                var clientChannel = service as IClientChannel;
                if (null != clientChannel && CommunicationState.Opened == clientChannel.State)
                {
                    clientChannel.Close();
                    clientChannel.Dispose();
                }

                if (null != channelFactory && CommunicationState.Opened == channelFactory.State)
                {
                    channelFactory.Close();
                }

                if (null != deadLetterServiceHost && CommunicationState.Opened == channelFactory.State)
                {
                    deadLetterServiceHost.Close();
                }
            }
        }
    }
}
