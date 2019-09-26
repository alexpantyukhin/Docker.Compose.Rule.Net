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

         var retryCount = (int) Math.Floor(Math.Max(_timeout.TotalMilliseconds / pollInterval.TotalMilliseconds, 1));

         // TODO: need to make HandleResult works
         var value =
            Policy
               .Handle<Exception>()
               .WaitAndRetry(retryCount, index => pollInterval)
               .Execute(WeHaveSuccess(cluster));

         if (value.Failed())
         {
            throw new InvalidOperationException(ServiceDidNotStartupExceptionMessage());
         }
      }

      private Func<SuccessOrFailure> WeHaveSuccess(Cluster cluster) {
         return () => {
            var successOrFailure = _clusterHealthCheck(cluster);
            _lastSuccessOrFailure = successOrFailure;
            if (successOrFailure.Failed())
            {
               throw new InvalidOperationException(ServiceDidNotStartupExceptionMessage());
            }
            return successOrFailure;
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