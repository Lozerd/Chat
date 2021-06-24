using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Chat
{
    class ServerUser
    {
        public int Id { get; set; }

        public string username { get; set; }

        public string password { get; set; }

        public OperationContext operationContext { get; set; }
    }
}
