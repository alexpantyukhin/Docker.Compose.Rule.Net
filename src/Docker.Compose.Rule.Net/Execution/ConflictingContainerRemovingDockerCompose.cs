using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Docker.Compose.Rule.Net.Execution
{
   public class ConflictingContainerRemovingDockerCompose : DelegatingDockerCompose
   {
      private readonly Docker _docker;
      private readonly int _retryAttempts;
      private const string NameConflictPattern = "name \"([^\"]*)\" is already in use";

      public ConflictingContainerRemovingDockerCompose(IDockerCompose dockerCompose, Docker docker) : this(
         dockerCompose, docker, 1)
      {
      }

      public ConflictingContainerRemovingDockerCompose(IDockerCompose dockerCompose, Docker docker, int retryAttempts) :
         base(dockerCompose)
      {
         if (retryAttempts <= 0)
         {
            throw new ArgumentException($"retryAttempts must be at least 1, was {retryAttempts}");
         }

         _docker = docker;
         _retryAttempts = retryAttempts;
      }

      public void Up()
      {
         for (var currRetryAttempt = 0; currRetryAttempt <= _retryAttempts; currRetryAttempt++) {
            try {
               DockerCompose.Up();
               return;
            } catch (DockerExecutionException e) {
               var conflictingContainerNames = GetConflictingContainerNames(e.Message);
               if (!conflictingContainerNames.Any()) {
                  // failed due to reason other than conflicting containers, so re-throw
                  throw e;
               }

               //log.debug("docker-compose up failed due to container name conflicts (container names: {}). "
               //          + "Removing containers and attempting docker-compose up again (attempt {}).",
               //   conflictingContainerNames, currRetryAttempt + 1);
               RemoveContainers(conflictingContainerNames);
            }
         }
      }

      private List<string> GetConflictingContainerNames(string message)
      {
         var result = new List<string>();
         
         var regex = new Regex(NameConflictPattern, RegexOptions.IgnoreCase);
         var matcher = regex.Match(message);
         while (matcher.Success)
         {
            var group = matcher.Groups[0];
            result.Add(group.Captures.First().Value);
            matcher.NextMatch();
         }

         return result;
      }

      private void RemoveContainers(List<string> containerNames) {
         try {
            _docker.Rm(containerNames);
         } catch (DockerExecutionException e) {
            // there are cases such as in CircleCI where 'docker rm' returns a non-0 exit code and "fails",
            // but container is still effectively removed as far as conflict resolution is concerned. Because
            // of this, be permissive and do not fail task even if 'rm' fails.
            
            // TODO:
            // log.debug("docker rm failed, but continuing execution", e);
         }
      }
      
   }
}