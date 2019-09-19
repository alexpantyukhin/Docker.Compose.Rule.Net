using Docker.Compose.Rule.Net.Configuration;

namespace Docker.Compose.Rule.Net.Execution
{
   public class SkipShutdownStrategy: IShutdownStrategy
   {
      public void Stop(IDockerCompose dockerCompose)
      {
      }

      public void Shutdown(IDockerCompose dockerCompose, Docker docker)
      {
//         log.warn("\n"
//                  + "******************************************************************************************\n"
//                  + "* docker-compose-rule has been configured to skip docker-compose shutdown:               *\n"
//                  + "* this means the containers will be left running after tests finish executing.           *\n"
//                  + "* If you see this message when running on CI it means you are potentially abandoning     *\n"
//                  + "* long running processes and leaking resources.                                          *\n"
//                  + "******************************************************************************************");
      }
   }
}