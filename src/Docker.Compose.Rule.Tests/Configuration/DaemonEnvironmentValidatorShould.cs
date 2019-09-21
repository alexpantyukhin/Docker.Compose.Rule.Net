using System;
using System.Collections.Generic;
using Docker.Compose.Rule.Net.Configuration;
using NUnit.Framework;

namespace Docker.Compose.Rule.Tests.Configuration
{
   [TestFixture]
   public class DaemonEnvironmentValidatorShould
   {
      [Test]
      public void validate_successfully_when_docker_environment_does_not_contain_docker_variables() {
         var variables = new Dictionary<string, string>()
            {
               {"SOME_VARIABLE", "SOME_VALUE"},
               {"ANOTHER_VARIABLE", "ANOTHER_VALUE"}
            };

         // TODO: No assert, Why?
         DaemonEnvironmentValidator.Instance().ValidateEnvironmentVariables(variables);
      }

      [Test]
      public void throw_exception_when_docker_environment_contains_illegal_docker_variables()
      {
         var variables = new Dictionary<string, string>()
         {
            {EnvironmentVariables.DOCKER_HOST, "tcp://192.168.99.100:2376"},
            {EnvironmentVariables.DOCKER_TLS_VERIFY, "1"},
            {EnvironmentVariables.DOCKER_CERT_PATH, "/path/to/certs"},
         };
         
//         exception.expect(IllegalStateException.class);
//         exception.expectMessage("These variables were set:");
//         exception.expectMessage(DOCKER_HOST);
//         exception.expectMessage(DOCKER_CERT_PATH);
//         exception.expectMessage(DOCKER_TLS_VERIFY);
//         exception.expectMessage("They cannot be set when connecting to a local docker daemon");

         Assert.Throws<InvalidOperationException>(() =>
         {
            DaemonEnvironmentValidator.Instance().ValidateEnvironmentVariables(variables);
         });

      }
   }
}