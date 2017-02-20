using System;
using System.ServiceModel;
using MsmqContract;

namespace PoisonDeadLetterTestHost
{
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
    public class SayHelloPoisonMessageService : ISayHelloService
    {
        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void SayHello(string to)
        {
            Console.Error.WriteLine("POISON MESSAGE: {0}", to);
        }
    }
}
