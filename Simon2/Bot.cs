using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace Simon2
{
    public class Bot
    {
        static Logger log = LogManager.GetLogger("IrcChatLog");

        TcpClient IRCConnection = null;
        Server RemoteServer;
        User Identity;

        NetworkStream ns = null;
        StreamReader sr = null;
        StreamWriter sw = null;

        public Bot(Server server, User user)
        {
            RemoteServer = server;
            Identity = user;

            try
            {
                IRCConnection = new TcpClient(RemoteServer.Host, RemoteServer.Port);
            }
            catch
            {
                Console.WriteLine("Connection Error");
            }

            try
            {
                ns = IRCConnection.GetStream();
                sr = new StreamReader(ns);
                sw = new StreamWriter(ns);

                SendData("USER", Identity.UserCredential);
                SendData("NICK", Identity.NickName);
                SendData("JOIN", RemoteServer.PreferedChannels[0]);

                IRCWork();
            }
            catch
            {
                Console.WriteLine("Communication error");
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                }
                if (sw != null)
                {
                    sw.Close();
                }
                if (ns != null)
                {
                    ns.Close();
                }
                if (IRCConnection != null)
                {
                    IRCConnection.Close();
                }
            }
        }

        public void SendData(string cmd, string param)
        {
            string command = (cmd + " " + param).Trim();

            sw.WriteLine(command);
            sw.Flush();
            log.Info(command);
            Console.WriteLine(command);
        }

        public void IRCWork()
        {
            IrcMessage message;
            
            while (true)
            {
                message = new IrcMessage(sr.ReadLine());
                Console.WriteLine(message.RawMessage);
                log.Info(message.RawMessage);

                if (message.Success)
                {
                    switch (message.Command)
                    {
                        case IrcCommands.PING:
                            SendData(IrcCommands.PONG, String.Join(",", message.Trailing));
                            break;
                        case IrcCommands.JOIN:
                            break;
                        case IrcCommands.QUIT:
                            var quote = (new ResourceManager("ExitQuotes", Assembly.GetExecutingAssembly())).GetString("String"+ new Random(DateTime.Now.Millisecond).Next(55).ToString());
                            break;
                        case IrcCommands.PRIVMSG:
                            InterpretMessage(message);
                            break;
                    }
                }
            }
        }

        private void InterpretMessage(IrcMessage message)
        {
        }
    }
}
