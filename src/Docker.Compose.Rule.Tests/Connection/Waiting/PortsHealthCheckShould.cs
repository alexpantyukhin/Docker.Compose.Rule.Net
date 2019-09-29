using System;
using Docker.Compose.Rule.Net.Connection;
using Docker.Compose.Rule.Net.Connection.Waiting;
using NUnit.Framework;

namespace Docker.Compose.Rule.Tests.Connection.Waiting
{
   [TestFixture]
   public class PortsHealthCheckShould
   {
      private Func<Container, SuccessOrFailure>  _healthCheck = HealthChecks.ToHaveAllPortsOpen();
      //private Container container = mock(Container.class);
      
      
   }
}