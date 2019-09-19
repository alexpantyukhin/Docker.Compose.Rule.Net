using System.Collections.Generic;
using System.Linq;

namespace Docker.Compose.Rule.Net.Configuration
{
   public class RemoteEnvironmentValidator: IEnvironmentValidator
   {
      private static List<string> _secureVariables = new List<string>()
      {
         EnvironmentVariables.DOCKER_TLS_VERIFY, EnvironmentVariables.DOCKER_CERT_PATH
      };
      
      private static readonly RemoteEnvironmentValidator VALIDATOR = new RemoteEnvironmentValidator();

      public static RemoteEnvironmentValidator Instance() {
         return VALIDATOR;
      }

      private RemoteEnvironmentValidator() {}
      
      public void ValidateEnvironmentVariables(Dictionary<string, string> dockerEnvironment)
      {
         var missingVariables = GetMissingEnvVariables(dockerEnvironment).ToList();

         var errorMessage =
            $"Missing required environment variables: : {string.Join(",", missingVariables)} . Please run `docker-machine env <machine-name>` and ensure they are set on the DockerComposition.";

         Preconditions.CheckState(!missingVariables.Any(), errorMessage);
      }

      private IEnumerable<string> GetMissingEnvVariables(Dictionary<string, string> dockerEnvironment)
      {
         var requiredVariables = (new List<string>() {EnvironmentVariables.DOCKER_HOST}).Union(SecureVariablesRequired(dockerEnvironment));
         return requiredVariables
            .Where(v => dockerEnvironment.ContainsKey(v) && string.IsNullOrEmpty(dockerEnvironment[v])).ToList();
      }

      private IEnumerable<string> SecureVariablesRequired(Dictionary<string,string> dockerEnvironment)
      {
         return CertVerificationEnabled(dockerEnvironment) ? _secureVariables : new List<string>();
      }

      private bool CertVerificationEnabled(Dictionary<string,string> dockerEnvironment)
      {
         return dockerEnvironment.ContainsKey(EnvironmentVariables.DOCKER_TLS_VERIFY);
      }
   }
}