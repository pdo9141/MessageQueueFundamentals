using System;
using System.ServiceModel;
using MsmqContract;

namespace PoisonDeadLetterTestClient
{
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
    public class SayHelloDeadLetterService : ISayHelloService
    {
        public void SayHello(string to)
        {
            Console.Error.WriteLine("DEAD LETTER: {0}", to);
        }
    }
}
