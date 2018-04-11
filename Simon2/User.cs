using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simon2
{
    public class User
    {
        public string NickName;
        public string HostMask;
        public string Server;
        public string RealName;

        public string Password;

        // USER <username> <hostname> <servername> <realname> (RFC 1459)
        // USER <user> <mode> <unused> <realname> (RFC 2812)
        public string UserCredential
        {
            get
            {
                return String.Format("{0} {1} {2} :{3}", NickName, HostMask, Server, RealName);
            }
        }
    }
}
