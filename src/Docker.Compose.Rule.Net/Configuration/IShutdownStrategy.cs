using Docker.Compose.Rule.Net.Execution;

namespace Docker.Compose.Rule.Net.Configuration
{
   public interface IShutdownStrategy
   {
      void Stop(IDockerCompose dockerCompose);
      void Shutdown(IDockerCompose dockerCompose, Execution.Docker docker);
   }

   public static class ShutdownStrategy
   {
      
   }
}