using System.Collections.Generic;

namespace Docker.Compose.Rule.Net.Configuration
{
   public interface IEnvironmentValidator
   {
      void ValidateEnvironmentVariables(Dictionary<string, string> dockerEnvironment);
   }
}