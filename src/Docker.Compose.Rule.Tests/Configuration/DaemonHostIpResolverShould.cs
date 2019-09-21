using Docker.Compose.Rule.Net.Configuration;
using NUnit.Framework;

namespace Docker.Compose.Rule.Tests.Configuration
{
   [TestFixture]
   public class DaemonHostIpResolverShould
   {
      [Test]
      public void return_local_host_with_null()
      {
         Assert.AreEqual(DaemonHostIpResolver.LOCALHOST, new DaemonHostIpResolver().ResolveIp(null));
      }

      [Test]
      public void return_local_host_with_blank()
      {
         Assert.AreEqual(DaemonHostIpResolver.LOCALHOST, new DaemonHostIpResolver().ResolveIp(""));
      }

      [Test]
      public void return_local_host_with_arbitrary()
      {
         Assert.AreEqual(DaemonHostIpResolver.LOCALHOST, new DaemonHostIpResolver().ResolveIp("arbitrary"));
      }
   }
}