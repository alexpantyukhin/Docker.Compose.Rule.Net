using System;
using Docker.Compose.Rule.Net.Connection;
using Docker.Compose.Rule.Net.Connection.Waiting;
using Docker.Compose.Rule.Net.Execution;
using NSubstitute;
using NUnit.Framework;

namespace Docker.Compose.Rule.Tests.Connection.Waiting
{
   public class HttpHealthCheckShould
   {
      private static Func<DockerPort, string> _urlFunction = port => null;
      public const int Port = 1234;
      private Container _container;

      [SetUp]
      public void Setup()
      {
         var docker = new Net.Execution.Docker(new Command());
         var compose = Substitute.For<IDockerCompose>();
         _container = new Container("test", docker, compose);
      }
      
      [Test]
      public void be_healthy_when_the_port_is_listening_over_http() {
         _container.PortIsListeningOnHttp(Port, _urlFunction).Returns(SuccessOrFailure.Success());

         var isHealthy = HealthChecks.ToRespondOverHttp(Port, _urlFunction)(_container);
         
         Assert.True(isHealthy.Succeeded());
      }
      
      [Test]
      public void be_unhealthy_when_all_ports_are_not_listening() {
         _container.PortIsListeningOnHttp(Port, _urlFunction).Returns(SuccessOrFailure.Failure("not listening"));

         var isHealthy = HealthChecks.ToRespondOverHttp(Port, _urlFunction)(_container);
         
         Assert.True(isHealthy.Failed());
      }
   }
}