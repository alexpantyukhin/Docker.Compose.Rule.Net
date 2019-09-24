using System;
using System.Collections.Generic;
using System.Linq;

namespace Docker.Compose.Rule.Net.Connection.Waiting
{
   public class ClusterHealthCheck
   {
      public static Func<Cluster, SuccessOrFailure> ServiceHealthCheck(List<string> containerNames,
         Func<List<Container>, SuccessOrFailure> del) 
      {
         return TransformingHealthCheck(cluster => cluster.Containers(containerNames), del);
      }

      public static Func<Cluster, SuccessOrFailure> ServiceHealthCheck(string containerName,
         Func<Container, SuccessOrFailure> containerCheck)
      {
         return TransformingHealthCheck(cluster => cluster.Container(containerName), containerCheck);
      }

      private static Func<Cluster, SuccessOrFailure> TransformingHealthCheck<T>(Func<Cluster, T> transform, Func<T, SuccessOrFailure> healthCheck)
      {
         return (cluster) =>
         {
            var target = transform(cluster);
            return healthCheck(target);
         };
      }

      public static Func<Cluster, SuccessOrFailure> NativeHealthChecks() {
         return (Cluster cluster) => {
            try
            {
               var unhealthyContainers = cluster.AllContainers()
                  .Where(c => c.State == State.UNHEALTHY)
                  .Select(c => c.GetContainerName()).ToList();
               if (unhealthyContainers.Any()) {
                  return SuccessOrFailure.Failure(
                     "The following containers are not healthy: " + string.Join(", ", unhealthyContainers));
               }
               return SuccessOrFailure.Success();
            } catch (Exception e) {
               return SuccessOrFailure.FromException(e);
            }
         };
      }
   }
}