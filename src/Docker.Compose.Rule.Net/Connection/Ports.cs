using System;
using System.Collections.Generic;
using Docker.Compose.Rule.Net.Connection.Waiting;

namespace Docker.Compose.Rule.Net.Connection
{
   public class Ports
   {
      private static readonly string PORT_PATTERN = "((\\d+).(\\d+).(\\d+).(\\d+)):(\\d+)->(\\d+)/tcp";
      private static readonly  int IP_ADDRESS = 1;
      private static readonly  int EXTERNAL_PORT = 6;
      private static readonly int INTERNAL_PORT = 7;

      private readonly List<DockerPort> _ports;

      public Ports(List<DockerPort> ports)
      {
         _ports = ports;
      }

      public Ports(DockerPort port)
      {
         _ports = new List<DockerPort>() {port};
      }

      public List<DockerPort> GetPorts()
      {
         return _ports;
      }

      public static Ports ParseFromDockerComposePs(string psOutput, string dockerMachineIp) {
         //Preconditions.checkArgument(!string.IsNullOrEmpty(psOutput), "No container found");
         //Matcher matcher = PORT_PATTERN.matcher(psOutput);
         var ports = new List<DockerPort>();
//         while (matcher.find()) {
//            String matchedIpAddress = matcher.group(IP_ADDRESS);
//            String ip = matchedIpAddress.equals(NO_IP_ADDRESS) ? dockerMachineIp : matchedIpAddress;
//            int externalPort = Integer.parseInt(matcher.group(EXTERNAL_PORT));
//            int internalPort = Integer.parseInt(matcher.group(INTERNAL_PORT));
//
//            ports.add(new DockerPort(ip, externalPort, internalPort));
//         }
         return new Ports(ports);
      }
   }

   public class DockerPort
   {
      public bool IsListeningNow
      {
         get
         {
            throw new NotImplementedException();
         }
      }

      public int GetInternalPort
      {
         get
         {
            throw new NotImplementedException();
         }
      }

      public SuccessOrFailure IsHttpRespondingSuccessfully(Func<DockerPort,string> urlFunction, in bool andCheckStatus)
      {
         throw new NotImplementedException();
      }
   }
}