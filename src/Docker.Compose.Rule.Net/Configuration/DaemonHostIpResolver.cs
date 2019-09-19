namespace Docker.Compose.Rule.Net.Configuration
{
   public class DaemonHostIpResolver: IHostIpResolver
   {
      public const string LOCALHOST = "127.0.0.1";

      public string ResolveIp(string dockerHost)
      {
         return LOCALHOST;
      }
   }
}