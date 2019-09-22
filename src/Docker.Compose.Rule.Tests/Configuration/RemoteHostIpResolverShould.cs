using System;
using Docker.Compose.Rule.Net.Configuration;
using NUnit.Framework;

namespace Docker.Compose.Rule.Tests.Configuration
{
   [TestFixture]
   public class RemoteHostIpResolverShould
   {
      private const string Ip = "192.168.99.100";
      private const int Port = 2376;

      [Test]
      public void result_in_error_blank_when_resolving_invalid_docker_host() {
         Assert.Throws<InvalidOperationException>(() => { new RemoteHostIpResolver().ResolveIp(""); });
      }

      [Test]
      public void result_in_error_null_when_resolving_invalid_docker_host()
      {
         Assert.Throws<InvalidOperationException>(() => { new RemoteHostIpResolver().ResolveIp(null); });
      }

      [Test]
      public void resolve_docker_host_with_port()
      {
         var dockerHost = $"{EnvironmentVariables.TCP_PROTOCOL}{Ip}:{Port}";
         Assert.AreEqual(Ip,new RemoteHostIpResolver().ResolveIp(dockerHost));
      }

      [Test]
      public void resolve_docker_host_without_port()
      {
         var dockerHost = $"{EnvironmentVariables.TCP_PROTOCOL}{Ip}";
         Assert.AreEqual(Ip,new RemoteHostIpResolver().ResolveIp(dockerHost));
      }
   }
}