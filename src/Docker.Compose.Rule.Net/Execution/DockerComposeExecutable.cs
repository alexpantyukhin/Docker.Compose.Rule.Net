using System;
using System.Collections.Generic;
using System.Diagnostics;
using Docker.Compose.Rule.Net.Configuration;

namespace Docker.Compose.Rule.Net.Execution
{
   public class DockerComposeExecutable: IExecutable
   {
      private readonly DockerComposeFiles _dockerComposeFiles;
      private readonly IDockerConfiguration _dockerConfiguration;

      public DockerComposeExecutable(DockerComposeFiles dockerComposeFiles, IDockerConfiguration dockerConfiguration)
      {
         _dockerComposeFiles = dockerComposeFiles;
         _dockerConfiguration = dockerConfiguration;
      }
      
      private static readonly DockerCommandLocations DockerComposeLocations = new DockerCommandLocations(
         Environment.GetEnvironmentVariable("DOCKER_COMPOSE_LOCATION"),
         "/usr/local/bin/docker-compose",
         "/usr/bin/docker-compose"
      );

      private static string DefaultDockerComposePath()
      {
         var pathToUse = DockerComposeLocations.PreferredLocation();

         if (string.IsNullOrEmpty(pathToUse))
         {
            throw new InvalidOperationException("Could not find docker-compose, looked in: ");
         }

         // TODO: log.debug("Using docker-compose found at " + pathToUse);

         return pathToUse;
      }
      
      private class VersionExecutable : IExecutable
      {
         public string CommandName()
         {
            return "docker-compose";
         }

         public Process Execute(params string[] commands)
         {
            var args = new List<string> {DefaultDockerComposePath()};
            args.AddRange(commands);
            return new Process()
            {
               StartInfo = new ProcessStartInfo
               {
                  FileName = "/bin/bash",
                  Arguments = string.Join(" ", args),
                  RedirectStandardOutput = true,
                  UseShellExecute = false,
                  CreateNoWindow = true,
               }
            };
         }
      }
      
      private static Version Version()
      {
         var dockerCompose = new Command(new VersionExecutable()); 
         var versionOutput = dockerCompose.Execute(Command.ThrowingOnError(), "-v");
         return DockerComposeVersion.ParseFromDockerComposeVersion(versionOutput);
      }

      public string CommandName()
      {
         return "docker-compose";
      }
      
      protected string DockerComposePath() {
         return DefaultDockerComposePath();
      }
      
      public ProjectName ProjectName() {
         return Configuration.ProjectName.Random();
      }

      public Process Execute(params string[] commands)
      {
         // TODO: DockerForMacHostsIssue.issueWarning();

         var args = new List<string> {DockerComposePath()};
         args.AddRange(ProjectName().ConstructComposeFileCommand()); 
         args.AddRange(_dockerComposeFiles.ConstructComposeFileCommand());
         args.AddRange(commands);

         var dockerConfProc = _dockerConfiguration.ConfiguredDockerComposeProcess();
         dockerConfProc.StartInfo.Arguments = string.Join(" ", commands);
         dockerConfProc.Start();

         return dockerConfProc;
      }
   }
}