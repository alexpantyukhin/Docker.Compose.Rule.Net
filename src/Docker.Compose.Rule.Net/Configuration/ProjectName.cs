using System;
using System.Collections.Generic;

namespace Docker.Compose.Rule.Net.Configuration
{
   public class ProjectName
   {
      private readonly string _projectName;

      private ProjectName(string projectName)
      {
         _projectName = projectName;
      }

      public List<string> ConstructComposeFileCommand() {
         return new List<string>() {"--project-name", _projectName};
      }

      public static ProjectName Random()
      {
         // TODO:
         return new ProjectName(Guid.NewGuid().ToString().Substring(0, 8));
      }

      public static ProjectName fromString(String name) {
         return new ProjectName(name);
      }
   }
}