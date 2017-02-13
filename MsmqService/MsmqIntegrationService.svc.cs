using System;
using System.Threading;
using System.ServiceModel;
using System.Diagnostics;
using System.ServiceModel.MsmqIntegration;
using MsmqContract;

namespace MsmqService
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.PerCall)]
    public class MsmqIntegrationService : IMsmqIntegrationContract
    {
        public void ProcessMessage(MsmqMessage<int> value)
        {
            var sSource = "MsmqIntegrationService";
            var sLog = "Application";
            var sEvent = String.Format("ProcessMessage Message: MsmqIntegration, Value: {0}", value.Body);

            if (!EventLog.SourceExists(sSource))
                EventLog.CreateEventSource(sSource, sLog);

            EventLog.WriteEntry(sSource, sEvent);

            Thread.Sleep(45000);
        }
    }
}
