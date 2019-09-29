using System.Collections.Generic;
using System.Linq;
using Docker.Compose.Rule.Net.Configuration;
using Docker.Compose.Rule.Net.Connection;

namespace Docker.Compose.Rule.Net.Execution
{
   public class AggressiveShutdownWithNetworkCleanupStrategy: IShutdownStrategy
   {
      public void Stop(IDockerCompose dockerCompose)
      {
      }

      public void Shutdown(IDockerCompose dockerCompose, Docker docker)
      {
         var runningContainers = dockerCompose.Ps();

         //log.info("Shutting down {}", runningContainers.stream().map(ContainerName::semanticName).collect(toList()));
         RemoveContainersCatchingErrors(docker, runningContainers);
         RemoveNetworks(dockerCompose, docker);
      }

      private void RemoveContainersCatchingErrors(Docker docker, List<ContainerName> runningContainers)
      {
         try {
            RemoveContainers(docker, runningContainers);
            
         } catch (DockerExecutionException exception) {
            //log.error("Error while trying to remove containers: {}", exception.getMessage());
         }
      }
      
      private static void RemoveContainers(Docker docker, List<ContainerName> running) {
         var rawContainerNames = running.Select(r =>r.RawName).ToList();

         docker.Rm(rawContainerNames);
//         log.debug("Finished shutdown");
      }
      
      private static void RemoveNetworks(IDockerCompose dockerCompose, Docker docker) {
         dockerCompose.Down();
         docker.PruneNetworks();
      }
   }
}