using System.Collections.Generic;
using Docker.Compose.Rule.Net.Configuration;
using NUnit.Framework;

namespace Docker.Compose.Rule.Tests.Configuration
{
   [TestFixture]
   public class DockerTypeShould
   {
      [Test]
      public void return_remote_as_first_valid_type_if_environment_is_illegal_for_daemon() {
         var variables = new Dictionary<string, string>
         {
            {EnvironmentVariables.DOCKER_HOST, "tcp://192.168.99.100:2376"},
            {EnvironmentVariables.DOCKER_TLS_VERIFY, "1"},
            {EnvironmentVariables.DOCKER_CERT_PATH, "/path/to/certs"}
         }; 

         Assert.AreEqual(DockerType.REMOTE, DockerType.GetFirstValidDockerTypeForEnvironment(variables));
      }

      [Test]
      public void return_daemon_as_first_valid_type_if_environment_is_illegal_for_remote()
      {
         var variables = new Dictionary<string, string>();
         Assert.AreEqual(DockerType.DAEMON, DockerType.GetFirstValidDockerTypeForEnvironment(variables));
      }

      [Test]
      public void return_absent_as_first_valid_type_if_environment_is_illegal_for_all() {
         var variables = new Dictionary<string, string>()
            {
               {EnvironmentVariables.DOCKER_TLS_VERIFY, "1"},
            };
         Assert.AreEqual(null, DockerType.GetFirstValidDockerTypeForEnvironment(variables));
      }
   }
}