using System;
using System.Threading;

namespace Docker.Compose.Rule.Net.Execution
{
   public class Retryer
   {
      private readonly int _retryAttempts;
      private readonly TimeSpan _delay;

      public static TimeSpan StandardDelay = TimeSpan.FromSeconds(5);

      public Retryer(int retryAttempts, TimeSpan delay)
      {
         _retryAttempts = retryAttempts;
         _delay = delay;
      }

      public T RunWithRetries<T>(Func<T> operation)
      {
         DockerExecutionException lastExecutionException = null;
         for (var i = 0; i <= _retryAttempts; i++) {
            try {
               return operation();
            } catch (DockerExecutionException e) {
               lastExecutionException = e;
               //log.warn("Caught exception: {}", e.getMessage());
               //log.warn("Retrying after {}", delay);
               if (i < _retryAttempts) {
                  Thread.Sleep((int) Math.Floor(_delay.TotalMilliseconds));
               }
            }
         }

         return default(T);
      }
   }
}