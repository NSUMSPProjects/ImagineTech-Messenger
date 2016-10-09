using MessengerInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MessengerServer
{
    // InstanceContextMode is single so that all clients will have access to one instance and everyone can know who
    // is connected to the service
    // ConcurrencyMode is multiple because we want a multithreaded server
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class MessengerService : IMessengerService
    {
        public void DoWork()
        {
        }
    }
}
