using System.Collections.Generic;
using System.Linq;

namespace Docker.Compose.Rule.Net.Configuration
{
   public class AdditionalEnvironmentValidator
   {
      private static List<string> _illegalVariables = new List<string>()
      {
         EnvironmentVariables.DOCKER_TLS_VERIFY, EnvironmentVariables.DOCKER_HOST, EnvironmentVariables.DOCKER_CERT_PATH
      };

      public static Dictionary<string, string> Validate(Dictionary<string, string> additionalEnvironment)
      {
         var invalidVariables = additionalEnvironment.Keys.Intersect(_illegalVariables).ToList();
         var errorMessage =
            $"The following variables: {string.Join(",", invalidVariables)}, cannot exist in your additional environment variable block as they will interfere with Docker.";

         Preconditions.CheckState(!invalidVariables.Any(), errorMessage);
         return additionalEnvironment;
      }
   }
}