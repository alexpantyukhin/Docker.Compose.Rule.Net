using System.Diagnostics;

namespace Docker.Compose.Rule.Net.Execution
{
   public interface IExecutable
   {
      string CommandName();
      
      Process Execute(params string[] commands);
   }
}