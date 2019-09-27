using System;
using System.Linq;
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

      public SuccessOrFailure PortIsListeningOnHttp(int internalPort, Func<DockerPort,string> urlFunction)
      {
         return PortIsListeningOnHttp(internalPort, urlFunction, true);
      }

      public SuccessOrFailure PortIsListeningOnHttpAndCheckStatus2xx(int internalPort, Func<DockerPort,string> urlFunction)
      {
         return PortIsListeningOnHttp(internalPort, urlFunction, false);
      }
      
      public SuccessOrFailure PortIsListeningOnHttp(int internalPort, Func<DockerPort, string> urlFunction, bool andCheckStatus) {
         try
         {
            var port = _portsMappings.GetPorts().FirstOrDefault(p => p.InternalPort == internalPort);
            if (!port.IsListeningNow) {
               return SuccessOrFailure.Failure("Internal port " + internalPort + " is not listening in container " + _containerName);
            }
            return port.IsHttpRespondingSuccessfully(urlFunction, andCheckStatus)
               .MapFailure(failureMessage => internalPort + " does not have a http response from " + urlFunction(port) + ":\n" + failureMessage);
         } catch (Exception e) {
            return SuccessOrFailure.FromException(e);
         }
      }

      public SuccessOrFailure AreAllPortsOpen()
      {
         var unavailablePorts = PortsMappings
            .GetPorts()
            .Where(p => !p.IsListeningNow)
            .Select(p => p.InternalPort)
            .ToList();

         var allPortsOpen =  !unavailablePorts.Any();
         
         var failureMessage = "The following ports failed to open: " + unavailablePorts;
         return SuccessOrFailure.FromBoolean(allPortsOpen, failureMessage);
      }
   }
}