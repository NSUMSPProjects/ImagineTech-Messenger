using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MessengerInterfaces
{
    public interface IClient
    {
        [OperationContract]
        void GetMessage(string message, string userName);
        [OperationContract]
        void GetUpdate(int value, string userName); // O for login, 1 for logoff
    }
}
