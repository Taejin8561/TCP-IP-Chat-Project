using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class NetInfo
    {
        public string Address { get; set; }
        public int Port { get; set; }

        public NetInfo(string address, int portNo)
        {
            Address = address;
            Port = portNo;
        }
    }
}
