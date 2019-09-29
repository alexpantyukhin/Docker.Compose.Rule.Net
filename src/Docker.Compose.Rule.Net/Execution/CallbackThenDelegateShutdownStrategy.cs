using System;
using Docker.Compose.Rule.Net.Configuration;

namespace Docker.Compose.Rule.Net.Execution
{
   public class CallbackThenDelegateShutdownStrategy : IShutdownStrategy
   {
      private readonly IShutdownStrategy _deleg;
      private readonly Action _callback;

      public CallbackThenDelegateShutdownStrategy(IShutdownStrategy deleg, Action callback)
      {
         _deleg = deleg;
         _callback = callback;
      }

      public void Stop(IDockerCompose dockerCompose)
      {
         try {
            _callback();
         } finally {
            _deleg.Stop(dockerCompose);
         }
      }

      public void Shutdown(IDockerCompose dockerCompose, Docker docker)
      {
         _deleg.Shutdown(dockerCompose, docker);
      }
   }
}