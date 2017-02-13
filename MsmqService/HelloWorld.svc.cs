using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MsmqService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "HelloWorld" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select HelloWorld.svc or HelloWorld.svc.cs at the Solution Explorer and start debugging.
    public class HelloWorld : IHelloWorld
    {
        public void DoWork(string message)
        {
            var sSource = "MsmqService";
            var sLog = "Application";
            var sEvent = String.Format("ProcessMessage Message: {0}", message);

            if (!EventLog.SourceExists(sSource))
                EventLog.CreateEventSource(sSource, sLog);

            EventLog.WriteEntry(sSource, sEvent);
        }
    }
}
