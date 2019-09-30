using System.Diagnostics;

namespace Docker.Compose.Rule.Net.Execution
{
   public interface IDockerConfiguration
   {
      Process ConfiguredDockerComposeProcess();
   }
}