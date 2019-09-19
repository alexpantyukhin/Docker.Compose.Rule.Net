using System;
using System.Diagnostics;
using System.Linq;

namespace Docker.Compose.Rule.Net.Execution
{
   public class Command
   {
      public Command()
      {
         
      }

      public string Execute(ErrorHandler errorHandler, params string[] commands)
      {
         var escapedArgs = commands.Select(c => c.Replace("\"", "\\\"")).ToArray();
         var process = new Process()
         {
            StartInfo = new ProcessStartInfo
            {
               FileName = "/bin/bash",
               Arguments = $"-c \"{escapedArgs}\"",
               RedirectStandardOutput = true,
               UseShellExecute = false,
               CreateNoWindow = true,
            }
         };

         process.Start();
         var result = process.StandardOutput.ReadToEnd();
         process.WaitForExit();

         return result;
      }
      
      public static ErrorHandler ThrowingOnError() 
      {
         throw new NotImplementedException();
      }
   }
}