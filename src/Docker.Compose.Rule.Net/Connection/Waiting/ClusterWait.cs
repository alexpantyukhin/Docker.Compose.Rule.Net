using System;
using System.Threading.Tasks;
using Polly;

namespace Docker.Compose.Rule.Net.Connection.Waiting
{
   public class ClusterWait
   {
      private readonly Func<Cluster, SuccessOrFailure> _clusterHealthCheck;
      private readonly TimeSpan _timeout;
      private SuccessOrFailure _lastSuccessOrFailure;

      public ClusterWait(Func<Cluster, SuccessOrFailure> clusterHealthCheck, TimeSpan timeout)
      {
         _clusterHealthCheck = clusterHealthCheck;
         _timeout = timeout;
      }

      public void WaitUntilReady(Cluster cluster)
      {
         var pollInterval = MinTimeSpan(TimeSpan.FromMilliseconds(500), _timeout.Divide(20));

         var retryCount = Math.Max(_timeout.Milliseconds / pollInterval.Milliseconds, 1);

         var value =
            Policy
               .HandleResult<bool>(v => v)
               .WaitAndRetryAsync(retryCount, index => pollInterval)
               .ExecuteAsync(() => new Task<bool>(WeHaveSuccess(cluster)))
               .Result;

         if (!value)
         {
            throw new InvalidOperationException(ServiceDidNotStartupExceptionMessage());
         }
      }

      private Func<bool> WeHaveSuccess(Cluster cluster) {
         return () => {
            var successOrFailure = _clusterHealthCheck(cluster);
            _lastSuccessOrFailure = successOrFailure;
            return successOrFailure.Succeeded();
         };
      }
      
      private string ServiceDidNotStartupExceptionMessage()
      {
         var healthCheckFailureMessage = _lastSuccessOrFailure != null
            ? _lastSuccessOrFailure.FailureMessage
            : "The healthcheck did not finish before the timeout";
         
         return "The cluster failed to pass a startup check: " + healthCheckFailureMessage;
      }
      
      private static TimeSpan MinTimeSpan(TimeSpan first, TimeSpan second) {
         if (first < second){
            return first;
         }

         return second;
      }
   }
}