using System;
using System.Collections.Generic;
using Docker.Compose.Rule.Net.Configuration;
using NUnit.Framework;

namespace Docker.Compose.Rule.Tests.Configuration
{
   [TestFixture]
   public class RemoteEnvironmentValidatorShould
   {
      [Test]
      public void throw_exception_if_docker_host_is_not_set()
      {
         var variables = new Dictionary<string, string>()
         {
            {"SOME_VARIABLE", "SOME_VALUE"},
         };

         Assert.Throws<InvalidOperationException>(() =>
         {
            RemoteEnvironmentValidator.Instance().ValidateEnvironmentVariables(variables);
         });
      }

      [Test]
      public void throw_exception_if_docker_cert_path_is_missing_and_tls_is_on()
      {
         var variables = new Dictionary<string, string>()
         {
            {EnvironmentVariables.DOCKER_HOST, "tcp://192.168.99.100:2376"},
            {EnvironmentVariables.DOCKER_TLS_VERIFY, "1"},
         };

         Assert.Throws<InvalidOperationException>(() =>
         {
            RemoteEnvironmentValidator.Instance().ValidateEnvironmentVariables(variables);
         });
      }

      [Test]
      public void validate_environment_with_all_valid_variables_set_without_tls()
      {
         var variables = new Dictionary<string, string>()
         {
            {EnvironmentVariables.DOCKER_HOST, "tcp://192.168.99.100:2376"},
            {"SOME_VARIABLE", "SOME_VALUE"},
         };

         RemoteEnvironmentValidator.Instance().ValidateEnvironmentVariables(variables);
      }

      [Test]
      public void validate_environment_with_all_valid_variables_set_with_tls()
      {
         var variables = new Dictionary<string, string>()
         {
            {EnvironmentVariables.DOCKER_HOST, "tcp://192.168.99.100:2376"},
            {EnvironmentVariables.DOCKER_TLS_VERIFY, "1"},
            {EnvironmentVariables.DOCKER_CERT_PATH, "/path/to/certs"},
            {"SOME_VARIABLE", "SOME_VALUE"},
         };

         RemoteEnvironmentValidator.Instance().ValidateEnvironmentVariables(variables);
      }
   }
}