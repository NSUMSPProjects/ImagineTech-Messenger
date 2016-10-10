using MessengerIntefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MessengerInterfaces
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMessengerService" in both code and config file together.
    [ServiceContract(CallbackContract = typeof(IClient))]
    public interface IMessengerService
    {
        [OperationContract]
        //void DoWork();
        void Test(string value);
    }
}
