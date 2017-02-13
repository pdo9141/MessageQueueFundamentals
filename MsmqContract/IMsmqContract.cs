using System.ServiceModel;

namespace MsmqContract
{
    [ServiceContract]
    public interface IMsmqContract
    {
        [OperationContract(IsOneWay = true)]
        void ProcessMessage(int value);
    }
}
