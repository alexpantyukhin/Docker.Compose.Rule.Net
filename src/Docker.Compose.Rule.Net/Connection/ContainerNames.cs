using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Docker.Compose.Rule.Net.Connection
{
   public class ContainerNames
   {
      private const string HEAD_PATTERN = "(\r|\n)";
      private const string BODY_PATTERN = "(\r|\n)+";
      
      public static List<ContainerName> ParseFromDockerComposePs(string psOutput) {
         //var rgx = new Regex(HEAD_PATTERN);
         //var breaks = rgx.Matches(psOutput);

         var psHeadAndBody = Regex.Split(psOutput, HEAD_PATTERN);
         if (psHeadAndBody.Length < 2) {
            return new List<ContainerName>();
         }

         var psBody = psHeadAndBody[1];
         return PsBodyLines(psBody).Select(ContainerName.FromPsLine).ToList();
      }

      private static IEnumerable<string> PsBodyLines(string psBody) {
         var lines = Regex.Split(psBody, BODY_PATTERN);
         return lines.Select(s => s.Trim()).Where(l => !string.IsNullOrEmpty(l));
      }
   }
}