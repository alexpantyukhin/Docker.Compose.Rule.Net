using Docker.Compose.Rule.Net.Configuration;

namespace Docker.Compose.Rule.Net.Execution
{
   public class KillDownShutdownStrategy : IShutdownStrategy
   {
      public void Stop(IDockerCompose dockerCompose)
      {
         //log.debug("Killing docker-compose cluster");
         dockerCompose.Kill();
      }

      public void Shutdown(IDockerCompose dockerCompose, Docker docker)
      {
         //log.debug("Downing docker-compose cluster");
         dockerCompose.Down();
      }
   }
}