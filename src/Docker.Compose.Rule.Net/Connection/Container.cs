using System;
using Docker.Compose.Rule.Net.Connection.Waiting;
using Docker.Compose.Rule.Net.Execution;

namespace Docker.Compose.Rule.Net.Connection
{
   public class Container
   {
      private readonly string _containerName;
      private readonly Execution.Docker _docker;
      private readonly IDockerCompose _dockerCompose;

      private Ports _portsMappings;

      public Container(string containerName, Execution.Docker docker, IDockerCompose dockerCompose)
      {
         _containerName = containerName;
         _docker = docker;
         _dockerCompose = dockerCompose;
      }
      
      //public ContainerName => _containerName;
      
      public string GetContainerName()
      {
         return _containerName;
      }
      
      private Ports PortsMappings
      {
         get
         {
            if (_portsMappings == null)
            {
               _portsMappings = GetDockerPorts();
            }

            return _portsMappings;
         }
      }

      public State State
      {
         get
         {
            var id = _dockerCompose.Id(this);
            if (id == null)
            {
               return State.DOWN;
            }

            return _docker.State(id);
         }
      }


      private Ports GetDockerPorts() {
//         try {
            return _dockerCompose.Ports(_containerName);
//         } catch (IOException | InterruptedException e) {
//            throw Throwables.propagate(e);
//         }
      }

      public SuccessOrFailure PortIsListeningOnHttp(in int internalPort, Func<DockerPort,string> urlFunction)
      {
         throw new NotImplementedException();
      }

      public SuccessOrFailure PortIsListeningOnHttpAndCheckStatus2xx(in int internalPort, Func<DockerPort,string> urlFunction)
      {
         throw new NotImplementedException();
      }

      public SuccessOrFailure AreAllPortsOpen()
      {
         throw new NotImplementedException();
      }
   }
}