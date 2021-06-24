using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Chat
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceChat : IServiceChat
    {
        List<ServerUser> users = new List<ServerUser>();

        int next_Id = 1;

        public int Connect(string name, string password)
        {
            ServerUser user = new ServerUser()
            {
                Id = next_Id,
                username = name,
                password = password,
                operationContext = OperationContext.Current
            };
            next_Id++;

            SendMessage($"{user.username} подключился к чату", 0);
            users.Add(user);

            return user.Id;
        }

        public void Disconnect(int id)
        {
            var user = users.FirstOrDefault(i=>i.Id == id);
            
            if (user != null)
            {
                users.Remove(user);
                SendMessage($"{user.username} покинул чат", 0);
            }
        }

        public void SendMessage(string message, int id)
        {
            foreach (var item in users)
            {
                string answer = "";

                var user = users.FirstOrDefault(i => i.Id == id);

                if (user != null)
                {
                    answer += $"{user.username}({DateTime.Now.ToShortTimeString()}): ";
                }


                answer += message;

                item.operationContext.GetCallbackChannel<IServiceChatCallback>().MessageCallback(answer);
            }
        }
        public void MessageCallback(string message)
        {
            throw new NotImplementedException();
        }
    }
}
