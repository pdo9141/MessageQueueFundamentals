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
            if (to.Equals("Phillip", StringComparison.OrdinalIgnoreCase))
            {
                Console.Error.WriteLine("ERROR: Phillip is not welcome.");
                throw new Exception("Phillip is not welcome.");
            }

            Console.Out.WriteLine("Hello {0}", to);
        }
    }
}
