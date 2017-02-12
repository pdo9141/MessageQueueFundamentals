using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MsmqContract;

namespace MsmqService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MsmqService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select MsmqService.svc or MsmqService.svc.cs at the Solution Explorer and start debugging.
    public class MsmqService : IMsmqContract
    {
        public void ProcessMessage(string message)
        {
            //throw new NotImplementedException();
        }
    }
}
