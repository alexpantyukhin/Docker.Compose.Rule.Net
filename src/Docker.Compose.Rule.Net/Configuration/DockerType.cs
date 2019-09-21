using System;
using System.Collections.Generic;

namespace Docker.Compose.Rule.Net.Configuration
{
   public class DockerType : IHostIpResolver, IEnvironmentValidator
   {
      private readonly IHostIpResolver _resolver;
      private readonly IEnvironmentValidator _validator;

      public static DockerType DAEMON =
         new DockerType(DaemonEnvironmentValidator.Instance(), new DaemonHostIpResolver());

      public static DockerType REMOTE =
         new DockerType(RemoteEnvironmentValidator.Instance(), new RemoteHostIpResolver());

      private DockerType(IEnvironmentValidator validator, IHostIpResolver resolver)
      {
         _resolver = resolver;
         _validator = validator;
      }

      public void ValidateEnvironmentVariables(Dictionary<string, string> dockerEnvironment)
      {
         _validator.ValidateEnvironmentVariables(dockerEnvironment);
      }

      public string ResolveIp(string dockerHost)
      {
         return _resolver.ResolveIp(dockerHost);
      }

      public static DockerType GetFirstValidDockerTypeForEnvironment(Dictionary<string,string> variables)
      {
         var dockerTypes = new[] {DAEMON, REMOTE};

         foreach (var dockerType in dockerTypes)
         {
            try {
               dockerType.ValidateEnvironmentVariables(variables);
               return dockerType;
            } catch (InvalidOperationException) {
               // ignore and try next type
            }
         }

         return null;
      }
   }
}