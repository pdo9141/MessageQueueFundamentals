using System.ServiceModel;

namespace MsmqContract
{
    [ServiceContract]
    public interface ISayHelloService
    {
        [OperationContract(IsOneWay = true)]
        void SayHello(string to);
    }
}
