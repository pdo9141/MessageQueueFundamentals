using System.ServiceModel;
using System.ServiceModel.MsmqIntegration;

namespace MsmqContract
{
    [ServiceContract]
    [ServiceKnownType(typeof(int))]
    public interface IMsmqIntegrationContract
    {
        [OperationContract(IsOneWay = true, Action = "*")]
        void ProcessMessage(MsmqMessage<int> value);
    }
}
