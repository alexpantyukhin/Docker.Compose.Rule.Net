using System.Collections.Generic;
using System.Linq;

namespace Docker.Compose.Rule.Net.Connection
{
   public class Cluster
   {
      public string Ip { get; }
      public ContainerCache ContainerCache { get; }

      public Cluster(string ip, ContainerCache containerCache)
      {
         Ip = ip;
         ContainerCache = containerCache;
      }
      
      public Container Container(string name) {
         return ContainerCache.Container(name);
      }

      public List<Container> Containers(List<string> containerNames) {
         return containerNames.Select(Container).ToList();
      }

      public List<Container> AllContainers() {
         return ContainerCache.Containers();
      }
   }
}