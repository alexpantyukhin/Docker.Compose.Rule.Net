namespace Docker.Compose.Rule.Net.Execution
{
   public interface ErrorHandler
   {
      void Handle(int exitCode, string output, string commandName, params string[] commands);
   }
}