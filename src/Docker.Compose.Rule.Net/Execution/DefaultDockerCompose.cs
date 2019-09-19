using System.Collections.Generic;
using System.IO;
using System.Linq;
using Docker.Compose.Rule.Net.Connection;

namespace Docker.Compose.Rule.Net.Execution
{
   public class DefaultDockerCompose : IDockerCompose
   {
      private Command _command;
      
      public void Pull()
      {
         _command.Execute(Command.ThrowingOnError(), "pull");
      }

      public void Build()
      {
         _command.Execute(Command.ThrowingOnError(), "build");
      }

      public void Up()
      {
         _command.Execute(Command.ThrowingOnError(), "up", "-d");
      }

      public void Down()
      {
         _command.Execute(Command.ThrowingOnError(), "down", "-volumes");
      }

      public void Stop()
      {
         _command.Execute(Command.ThrowingOnError(), "stop");
      }

      public void Kill()
      {
         _command.Execute(Command.ThrowingOnError(), "kill");
      }

      public void Rm()
      {
         _command.Execute(Command.ThrowingOnError(), "rm", "--force", "-v");
      }

      public void Up(Container container)
      {
         _command.Execute(Command.ThrowingOnError(), "up", "-d", container.GetContainerName());
      }

      public void Start(Container container)
      {
         _command.Execute(Command.ThrowingOnError(), "start", container.GetContainerName());
      }

      public void Stop(Container container)
      {
         _command.Execute(Command.ThrowingOnError(), "stop", container.GetContainerName());
      }

      public void Kill(Container container)
      {
         _command.Execute(Command.ThrowingOnError(), "kill", container.GetContainerName());
      }

      public string Exec(DockerComposeExecOption dockerComposeExecOption, string containerName,
         DockerComposeExecArgument dockerComposeExecArgument)
      {
         // TODO:
         //verifyDockerComposeVersionAtLeast(VERSION_1_7_0, "You need at least docker-compose 1.7 to run docker-compose exec");
         var fullArgs = ConstructFullDockerComposeExecArguments(dockerComposeExecOption, containerName, dockerComposeExecArgument);
         return _command.Execute(Command.ThrowingOnError(), fullArgs);
      }

      public string Run(DockerComposeRunOption dockerComposeRunOption, string containerName,
         DockerComposeRunArgument dockerComposeRunArgument)
      {
         var fullArgs = ConstructFullDockerComposeRunArguments(dockerComposeRunOption, containerName, dockerComposeRunArgument);
         return _command.Execute(Command.ThrowingOnError(), fullArgs);
      }

      public List<ContainerName> Ps()
      {
         throw new System.NotImplementedException();
      }

      public string Id(Container container)
      {
         return Id(container.GetContainerName());
      }

      public string Config()
      {
         return _command.Execute(Command.ThrowingOnError(), "config");
      }

      public List<string> Services()
      {
         var servicesOutput = _command.Execute(Command.ThrowingOnError(), "config", "--services");
         return servicesOutput.Split('\r', '\n').ToList();
      }

      public bool WriteLogs(string container, Stream output)
      {
         throw new System.NotImplementedException();
      }

      public Ports Ports(string service)
      {
         // TODO:fix ips!
         return Connection.Ports.ParseFromDockerComposePs(PsOutput(service), "");
      }
      
      private string Id(string containerName)  {
         return _command.Execute(Command.ThrowingOnError(), "ps", "-q", containerName);
      }
      
      private string[] ConstructFullDockerComposeExecArguments(DockerComposeExecOption dockerComposeExecOption, string containerName, DockerComposeExecArgument dockerComposeExecArgument)
      {
         var fullArgs = new List<string>();
         fullArgs.Add("exec");
         fullArgs.Add("-T");
         fullArgs.AddRange(DockerComposeExecOption.Options());
         fullArgs.Add(containerName);
         //fullArgs.AddRange(dockerComposeExecArgument.arguments());

         return fullArgs.ToArray();
      }
      
      private string[] ConstructFullDockerComposeRunArguments(DockerComposeRunOption dockerComposeRunOption, string containerName, DockerComposeRunArgument dockerComposeRunArgument)
      {
         throw new System.NotImplementedException();
      }
      
      private string PsOutput(string service){
         string psOutput = _command.Execute(Command.ThrowingOnError(), "ps", service);
         //validState(!Strings.isNullOrEmpty(psOutput), "No container with name '" + service + "' found");
         return psOutput;
      }
   }
}