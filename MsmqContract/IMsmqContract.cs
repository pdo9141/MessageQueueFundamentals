using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MsmqContract
{
    [ServiceContract]
    public interface IMsmqContract
    {
        [OperationContract(IsOneWay = true)]
        void ProcessMessage(string message);
    }
}
