using System;
using System.Linq;

namespace Docker.Compose.Rule.Net.Connection
{
   public class ContainerName
   {
      public string RawName { get; }

      public string SemanticName { get; }
      
      public ContainerName(string rawName, string semanticName)
      {
         RawName = rawName;
         SemanticName = semanticName;
      }

      public static ContainerName FromPsLine(string psLine)
      {
         var lineComponents = psLine.Split(" ");
         var rawName = lineComponents[0];

         if (ProbablyCustomName(rawName))
         {
            return new ContainerName(rawName, rawName);
         }
         
         var semanticName = WithoutDirectory(WithoutScaleNumber(rawName));
         return new ContainerName(rawName, semanticName);
      }

      private static bool ProbablyCustomName(string rawName)
      {
         return !(rawName.Split("_").Length >= 3);
      }
      
      private static string WithoutDirectory(string rawName) {
         var skipped = rawName.Split("_")
            .Skip(1);

         return string.Join("_", skipped);
      }

      public static string WithoutScaleNumber(string rawName) {
         var components = rawName.Split("_");
         var limited = components.Take(components.Length - 1);

         return string.Join("_", limited);
      }
   }
}