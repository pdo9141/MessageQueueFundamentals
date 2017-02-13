using System;
using System.Threading;
using System.ServiceModel;
using System.Diagnostics;
using System.ServiceModel.MsmqIntegration;
using MsmqContract;

namespace MsmqService
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.PerCall)]
    public class MsmqService : IMsmqContract
    {
        public void ProcessMessage(MsmqMessage<string> message)
        {
            var sSource = "MsmqService";
            var sLog = "Application";
            var sEvent = String.Format("ProcessMessage Event: {0}", message.Body);

            if (!EventLog.SourceExists(sSource))
                EventLog.CreateEventSource(sSource, sLog);

            EventLog.WriteEntry(sSource, sEvent);

            Thread.Sleep(45000);
        }
    }
}
