using System.Collections.Generic;

namespace Docker.Compose.Rule.Net.Configuration
{
   public class DockerType : IHostIpResolver, IEnvironmentValidator
   {
      private readonly IHostIpResolver _resolver;
      private readonly IEnvironmentValidator _validator;

      public DockerType(IHostIpResolver resolver, IEnvironmentValidator validator)
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
   }
}