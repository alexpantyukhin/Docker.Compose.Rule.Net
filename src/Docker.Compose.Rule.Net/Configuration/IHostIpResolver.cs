namespace Docker.Compose.Rule.Net.Configuration
{
   public interface IHostIpResolver
   {
      string ResolveIp(string dockerHost);
   }
}