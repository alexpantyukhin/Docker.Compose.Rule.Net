using System.Collections.Generic;
using System.Linq;

namespace Docker.Compose.Rule.Net.Connection
{
   public abstract class Cluster
   {
      public abstract string IP();

      public abstract ContainerCache ContainerCache();

      public Container Container(string name) {
         return ContainerCache().Container(name);
      }

      public List<Container> Containers(List<string> containerNames) {
         return containerNames.Select(Container).ToList();
      }

      public List<Container> AllContainers() {
         return ContainerCache().Containers();
      }
   }
}