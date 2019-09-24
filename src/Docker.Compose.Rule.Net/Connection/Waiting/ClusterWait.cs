using System;
using System.Threading.Tasks;

namespace Docker.Compose.Rule.Net.Connection.Waiting
{
   public class ClusterWait
   {
      private readonly Func<Cluster, SuccessOrFailure> _clusterHealthCheck;
      private readonly TimeSpan _timeout;

      public ClusterWait(Func<Cluster, SuccessOrFailure> clusterHealthCheck, TimeSpan timeout)
      {
         _clusterHealthCheck = clusterHealthCheck;
         _timeout = timeout;
      }

      public Task WaitUntilReady()
      {
         var pollInterval = MinTimeSpan(TimeSpan.FromMilliseconds(500), _timeout.Divide(20));
         
         // TODO fix
         return Task.FromResult(0);
      }
      
//      private Func<bool> WeHaveSuccess(Cluster cluster,
//         AtomicReference<Optional<SuccessOrFailure>> lastSuccessOrFailure) {
//         return () -> {
//            SuccessOrFailure successOrFailure = clusterHealthCheck.isClusterHealthy(cluster);
//            lastSuccessOrFailure.set(Optional.of(successOrFailure));
//            return successOrFailure.succeeded();
//         };
//      }
      
      private static TimeSpan MinTimeSpan(TimeSpan first, TimeSpan second) {
         if (first < second){
            return first;
         }

         return second;
      }
   }
}