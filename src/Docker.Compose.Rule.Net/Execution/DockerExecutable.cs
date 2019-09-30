using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Docker.Compose.Rule.Net.Execution
{
   public class DockerExecutable: IExecutable
   {
      private readonly IDockerConfiguration _dockerConfiguration;

      public DockerExecutable(IDockerConfiguration dockerConfiguration)
      {
         _dockerConfiguration = dockerConfiguration;
      }
      
      private static readonly DockerCommandLocations DockerComposeLocations = new DockerCommandLocations(
         Environment.GetEnvironmentVariable("DOCKER_LOCATION"),
         "/usr/local/bin/docker",
         "/usr/bin/docker"
      );
      
      public string CommandName()
      {
         return "docker";
      }

      protected string DockerPath()
      {
         var pathToUse = DockerComposeLocations.PreferredLocation();
         if (string.IsNullOrEmpty(pathToUse))
         {
            throw new InvalidOperationException("Could not find docker, looked in: ");
         }

         //TODO: log.debug("Using docker found at " + pathToUse);

         return pathToUse;
      }

      public Process Execute(params string[] commands)
      {
         var args = new List<string> {DockerPath()};
         args.AddRange(commands);
         
         var dockerConfProc = _dockerConfiguration.ConfiguredDockerComposeProcess();
         dockerConfProc.StartInfo.Arguments = string.Join(" ", commands);
         dockerConfProc.Start();

         return dockerConfProc;
      }
   }
}