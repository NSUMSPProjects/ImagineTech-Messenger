using MessengerInterfaces;
using System;
using System.Collections.Concurrent;
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
        public ConcurrentDictionary<string, ConnectedClient> _connectedClients = new
            ConcurrentDictionary<string, ConnectedClient>(); // thread safe

        public int Login(string userName)
        {
            // is anyone else logged in with my name?
            foreach(var client in _connectedClients)
            {
                if(client.Key.ToLower() == userName.ToLower())
                {
                    // if yes
                    return 1; // client will understand that someone is logged in
                }
            }

            var establishedUserConnection = OperationContext.Current.GetCallbackChannel<IClient>();

            ConnectedClient newClient = new ConnectedClient();
            newClient.connection = establishedUserConnection;
            newClient.UserName = userName;

            _connectedClients.TryAdd(userName, newClient);

            return 0; // successfully added in
        }

        public void SendMessageToAll(string message, string userName)
        {
            foreach (var client in _connectedClients)
            {
                if(client.Key.ToLower() != userName.ToLower())
                {
                    client.Value.connection.GetMessage(message, userName);
                }
            }
        }
    }
}
