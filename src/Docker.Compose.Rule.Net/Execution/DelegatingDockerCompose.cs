using System.Collections.Generic;
using System.IO;
using Docker.Compose.Rule.Net.Connection;

namespace Docker.Compose.Rule.Net.Execution
{
   public class DelegatingDockerCompose : IDockerCompose
   {
      public DelegatingDockerCompose(IDockerCompose dockerCompose)
      {
         DockerCompose = dockerCompose;
      }
      
      public void Pull()
      {
         DockerCompose.Pull();
      }

      public void Build()
      {
         DockerCompose.Build();
      }

      public void Up()
      {
         DockerCompose.Up();
      }

      public void Down()
      {
         DockerCompose.Down();
      }

      public void Stop()
      {
         DockerCompose.Stop();
      }

      public void Kill()
      {
         DockerCompose.Kill();
      }

      public void Rm()
      {
         DockerCompose.Rm();
      }

      public void Up(Container container)
      {
         DockerCompose.Up();
      }

      public void Start(Container container)
      {
         DockerCompose.Start(container);
      }

      public void Stop(Container container)
      {
         DockerCompose.Stop();
      }

      public void Kill(Container container)
      {
         DockerCompose.Kill();
      }

      public string Exec(DockerComposeExecOption dockerComposeExecOption, string containerName,
         DockerComposeExecArgument dockerComposeExecArgument)
      {
         return DockerCompose.Exec(dockerComposeExecOption, containerName, dockerComposeExecArgument);
      }

      public string Run(DockerComposeRunOption dockerComposeRunOption, string containerName,
         DockerComposeRunArgument dockerComposeRunArgument)
      {
         return DockerCompose.Run(dockerComposeRunOption, containerName, dockerComposeRunArgument);
      }

      public List<ContainerName> Ps()
      {
         return DockerCompose.Ps();
      }

      public string Id(Container container)
      {
         return DockerCompose.Id(container);
      }

      public string Config()
      {
         return DockerCompose.Config();
      }

      public List<string> Services()
      {
         return  DockerCompose.Services();
      }

      public bool WriteLogs(string container, Stream output)
      {
         return DockerCompose.WriteLogs(container, output);
      }

      public Ports Ports(string service)
      {
         return DockerCompose.Ports(service);
      }

      protected IDockerCompose DockerCompose { get; }
   }
}