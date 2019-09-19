using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Docker.Compose.Rule.Net.Configuration
{
   public class DockerComposeFiles
   {
      private readonly List<FileInfo> _dockerComposeFiles;

      public DockerComposeFiles(List<FileInfo> dockerComposeFiles)
      {
         _dockerComposeFiles = dockerComposeFiles;
      }

      public static DockerComposeFiles From(params string[] dockerComposeFilenames)
      {
         var files = dockerComposeFilenames.Select(dc => new FileInfo(dc)).ToList();

         ValidateAtLeastOneComposeFileSpecified(files);
         ValidateComposeFilesExist(files);

         return new DockerComposeFiles(files);
      }
      
      public List<string> ConstructComposeFileCommand()
      {
         return
            _dockerComposeFiles
               .Select(f => f.FullName)
               .Select(f => new List<string>() {"--file", f})
               .SelectMany(f => f)
               .ToList();
      }
      
      private static void ValidateAtLeastOneComposeFileSpecified(List<FileInfo> dockerComposeFiles) {
         //TODO: checkArgument(!dockerComposeFiles.isEmpty(), "A docker compose file must be specified.");
      }

      private static void ValidateComposeFilesExist(List<FileInfo> dockerComposeFiles) {
         var missingFiles = dockerComposeFiles.Where(f => f.Exists).Select(f => f.FullName).ToList();

         var errorMessage =
            $"The following docker-compose files: {string.Join(",", missingFiles)}, do not exist.";
         
         // TODO: checkState(missingFiles.isEmpty(), errorMessage);
         Preconditions.CheckState(!missingFiles.Any(), errorMessage);
      }
   }
}