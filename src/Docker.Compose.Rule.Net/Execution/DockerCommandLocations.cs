using System.IO;
using System.Linq;

namespace Docker.Compose.Rule.Net.Execution
{
   public class DockerCommandLocations
   {
      private readonly string[] _possiblePaths;

      public DockerCommandLocations(params string[] possiblePaths)
      {
         _possiblePaths = possiblePaths;
      }

      public string PreferredLocation()
      {
         return _possiblePaths.FirstOrDefault(f => !string.IsNullOrEmpty(f)
                                                   && File.Exists(f));
      }
      
   }
}