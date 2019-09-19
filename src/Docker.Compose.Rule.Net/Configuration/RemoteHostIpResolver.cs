using System;

namespace Docker.Compose.Rule.Net.Configuration
{
   public class RemoteHostIpResolver: IHostIpResolver
   {
      public string ResolveIp(string dockerHost)
      {
         if (string.IsNullOrEmpty(dockerHost))
         {
            throw new ArgumentException("DOCKER_HOST cannot be blank/null");
         }

         var ind = dockerHost.IndexOf(EnvironmentVariables.TCP_PROTOCOL, StringComparison.Ordinal);
         var val = dockerHost.Substring(ind).TrimStart(EnvironmentVariables.TCP_PROTOCOL.ToCharArray());
         var ind2 = val.IndexOf(':');

         return val.Substring(0, ind2);
         
//         return string.Empty;
         // TODO:
//         return Optional.ofNullable(emptyToNull(dockerHost))
//            .map(host -> StringUtils.substringAfter(host, TCP_PROTOCOL))
//            .map(ipAndMaybePort -> StringUtils.substringBefore(ipAndMaybePort, ":"))
//            .orElseThrow(() -> new IllegalArgumentException("DOCKER_HOST cannot be blank/null"));
      }
   }
}