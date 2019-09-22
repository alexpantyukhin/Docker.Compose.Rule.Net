using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Docker.Compose.Rule.Net.Configuration
{
   public class ProjectName
   {
      private readonly string _projectName;

      private ProjectName(string projectName)
      {
         if (projectName.Trim().Length == 0)
         {
            throw new ArgumentException("ProjectName must not be blank.");
         }

         if (!projectName.All(char.IsLetterOrDigit))
         {
            throw new ArgumentException("ProjectName must not be blank.");
         }
         
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

      public static ProjectName FromString(string name) {
         return new ProjectName(name);
      }

      public string AsString()
      {
         return _projectName;
      }
   }
}