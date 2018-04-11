using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simon2
{
    class Host
    {
        static void Main(string[] args)
        {
            var ServerList = new ServerLibrary()
            {
                Name = "EfNet",
                Servers = new Server[]
                {
                    new Server()
                    {
                        Name = "PrisonNet on EfNet",
                        Host = "irc.prison.net",
                        Port = 6667,
                        PreferedChannels = new string[] { "#XNA" }
                    },
                    new Server()
                    {
                        Name = "wright on Freenot",
                        Host = "wright.freenode.net",
                        Port = 6667,
                        PreferedChannels = new string[] { "#TestSimon" }
                    },
                }
            };

            var profile = new User()
            {
                NickName = "Simon-itanex",
                RealName = "Simon-itanex",
                Server = "SimonsBox",
                HostMask = "SimonsHost",
            };

            new Bot(ServerList.Servers[0], profile);
            
            Console.WriteLine("Bot quit");
            Console.ReadLine();
        }
    }
}
