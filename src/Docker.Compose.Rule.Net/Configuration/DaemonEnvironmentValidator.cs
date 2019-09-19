using System.Collections.Generic;
using System.Linq;

namespace Docker.Compose.Rule.Net.Configuration
{
   public class DaemonEnvironmentValidator : IEnvironmentValidator
   {
      private static List<string> _illegalVariables = new List<string>()
      {
         EnvironmentVariables.DOCKER_TLS_VERIFY, EnvironmentVariables.DOCKER_HOST, EnvironmentVariables.DOCKER_CERT_PATH
      };
      private static readonly DaemonEnvironmentValidator _instance = new DaemonEnvironmentValidator();

      private DaemonEnvironmentValidator()
      {
         
      }

      public static DaemonEnvironmentValidator Instance()
      {
         return _instance;
      }
      
      public void ValidateEnvironmentVariables(Dictionary<string, string> dockerEnvironment)
      {
         var invalidVariables = _illegalVariables.Where(iv => dockerEnvironment.Keys.Contains(iv)).ToList();

         var errorMessage =
            $"These variables were set: {string.Join(",", invalidVariables)}, They cannot be set when connecting to a local docker daemon.";

         Preconditions.CheckState(!invalidVariables.Any(), errorMessage);
      }
   }
}