using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Docker.Compose.Rule.Net.Execution;

namespace Docker.Compose.Rule.Net.Connection
{
   public class ContainerCache
   {
      private readonly Execution.Docker _docker;
      private readonly IDockerCompose _dockerCompose;
      private readonly ConcurrentDictionary<string, Container> _containers;

      public ContainerCache(Execution.Docker docker, IDockerCompose dockerCompose)
      {
         _docker = docker;
         _dockerCompose = dockerCompose;
         _containers = new ConcurrentDictionary<string, Container>();
      }

      public Container Container(string name)
      {
         return _containers.GetOrAdd(name, new Container(name, _docker, _dockerCompose));
      }

      public List<Container> Containers()
      {
         return _dockerCompose.Services().Select(Container).ToList();
      }
   }
}