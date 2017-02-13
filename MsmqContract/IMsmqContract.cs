using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.MsmqIntegration;
using System.Text;
using System.Threading.Tasks;

namespace MsmqContract
{
    [ServiceContract]
    [ServiceKnownType(typeof(string))]
    public interface IMsmqContract
    {
        [OperationContract(IsOneWay = true, Action = "*")]
        void ProcessMessage(MsmqMessage<string> message);
    }
}
