using System;
using System.ServiceModel;
using MsmqContract;

namespace PoisonDeadLetterTestHost
{
    public class SayHelloService : ISayHelloService
    {
        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void SayHello(string to)
        {
            Console.Out.WriteLine("Hello {0}", to);
        }
    }
}
