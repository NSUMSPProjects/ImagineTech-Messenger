using MessengerInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerServer
{
    public class ConnectedClient
    {
        public IClient connection;

        public string UserName { get; set; } // User name of the client
    }
}
