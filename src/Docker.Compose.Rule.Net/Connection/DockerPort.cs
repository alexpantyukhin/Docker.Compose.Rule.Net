using System;
using System.Net;
using System.Net.Sockets;
using Docker.Compose.Rule.Net.Connection.Waiting;

namespace Docker.Compose.Rule.Net.Connection
{
   public class DockerPort
   {
      private readonly PortMapping _portMapping;
      public string Ip { get; }

      public DockerPort(string ip, PortMapping portMapping)
      {
         _portMapping = portMapping;
         Ip = ip;
      }
      
      public bool IsListeningNow
      {
         get
         {
            var ipAddress = IPAddress.Parse(Ip);
            TcpClient client = new TcpClient();

            try
            {
               client.Client.Connect(ipAddress, ExternalPort);
               // TODO:  
               // log.trace("External Port '{}' on ip '{}' was open", getExternalPort(), ip);
               return true;
            }
            catch (SocketException e)
            {
               return false;
            }
            finally
            {
               client.Client.Close();
            }
         }
      }

      public int InternalPort => _portMapping.InternalPort;

      public int ExternalPort => _portMapping.ExternalPort;

      public SuccessOrFailure IsHttpRespondingSuccessfully(Func<DockerPort,string> urlFunction, in bool andCheckStatus)
      {
         throw new NotImplementedException();
      }
   }
}