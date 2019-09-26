using System;

namespace Docker.Compose.Rule.Net.Connection.Waiting
{
   public static class HealthChecks
   {
      public static Func<Container, SuccessOrFailure> ToRespondOverHttp(int internalPort, Func<DockerPort, string> urlFunction) {
         return container => container.PortIsListeningOnHttp(internalPort, urlFunction);
      }

      public static Func<Container, SuccessOrFailure> ToRespond2xxOverHttp(int internalPort, Func<DockerPort, string> urlFunction) {
         return container => container.PortIsListeningOnHttpAndCheckStatus2xx(internalPort, urlFunction);
      }

      public static Func<Container, SuccessOrFailure> ToHaveAllPortsOpen() {
         return c => c.AreAllPortsOpen();
      }
   }
}