﻿using System;
using System.Threading;
using System.ServiceModel;
using System.Diagnostics;
using MsmqContract;

namespace MsmqService
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.PerCall)]
    public class MsmqService : IMsmqContract
    {
        public void ProcessMessage(int value)
        {
            var sSource = "MsmqService";
            var sLog = "Application";
            var sEvent = String.Format("ProcessMessage Message: Msmq, Value: {0}", value.ToString());

            if (!EventLog.SourceExists(sSource))
                EventLog.CreateEventSource(sSource, sLog);

            EventLog.WriteEntry(sSource, sEvent);

            Thread.Sleep(45000);
        }
    }
}
