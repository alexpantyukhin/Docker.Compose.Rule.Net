using System.Collections.Generic;
using System.Linq;
using Docker.Compose.Rule.Net.Configuration;
using Docker.Compose.Rule.Net.Connection;

namespace Docker.Compose.Rule.Net.Execution
{
   public class AggressiveShutdownStrategy : IShutdownStrategy
   {
      public void Stop(IDockerCompose dockerCompose)
      {
         //throw new System.NotImplementedException();
      }

      public void Shutdown(IDockerCompose dockerCompose, Docker docker)
      {
         var runningContainers = dockerCompose.Ps();

         //log.info("Shutting down {}", runningContainers.stream().map(ContainerName::semanticName).collect(toList()));
         if (RemoveContainersCatchingErrors(docker, runningContainers)) {
            return;
         }
         
         //log.debug("First shutdown attempted failed due to btrfs volume error... retrying");
         if (RemoveContainersCatchingErrors(docker, runningContainers)) {
            return;
         }
         
//         log.warn("Couldn't shut down containers due to btrfs volume error, "
//                  + "see https://circleci.com/docs/docker-btrfs-error/ for more info.");

         //log.info("Pruning networks");
         //docker.pruneNetworks();
      }

      private bool RemoveContainersCatchingErrors(Docker docker, List<ContainerName> runningContainers)
      {
         try {
            RemoveContainers(docker, runningContainers);
            return true;
         } catch (DockerExecutionException exception) {
            return false;
         }
      }
      
      private static void RemoveContainers(Docker docker, List<ContainerName> running) {
         var rawContainerNames = running.Select(r =>r.RawName).ToList();

//         docker.rm(rawContainerNames);
//         log.debug("Finished shutdown");
      }
   }
}