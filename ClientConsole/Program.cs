using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var client = new MsmqServiceBridge.MsmqContractClient())
            {
                client.ProcessMessage(9);
            }
            
            using (var client = new HelloWorldBridge.HelloWorldClient("BasicHttpBinding_IHelloWorld"))
            {
                client.DoWork("BasicHttpBinding");
            }

            using (var client = new HelloWorldBridge.HelloWorldClient("WSHttpBinding_IHelloWorld"))
            {
                client.DoWork("WSHttpBinding");
            }

            using (var client = new HelloWorldBridge.HelloWorldClient("NetTcpBinding_IHelloWorld"))
            {
                client.DoWork("NetTcpBinding");
            }
        }
    }
}
