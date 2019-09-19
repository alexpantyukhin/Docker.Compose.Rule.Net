using Docker.Compose.Rule.Net.Configuration;

namespace Docker.Compose.Rule.Net.Execution
{
   public class GracefulShutdownStrategy: IShutdownStrategy
   {
      public void Stop(IDockerCompose dockerCompose)
      {
         //log.debug("Stopping docker-compose cluster");
         dockerCompose.Stop();
         dockerCompose.Kill();
      }

      public void Shutdown(IDockerCompose dockerCompose, Docker docker)
      {
         //log.debug("Downing docker-compose cluster");
         dockerCompose.Down();
      }
   }
}