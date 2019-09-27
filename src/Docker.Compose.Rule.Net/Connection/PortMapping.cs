namespace Docker.Compose.Rule.Net.Connection
{
   public class PortMapping
   {
      public int ExternalPort { get; }
      public int InternalPort { get; }

      public PortMapping(int externalPort, int internalPort)
      {
         ExternalPort = externalPort;
         InternalPort = internalPort;
      }
   }
}