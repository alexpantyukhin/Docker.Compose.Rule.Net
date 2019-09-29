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
      
      public virtual void Pull()
      {
         DockerCompose.Pull();
      }

      public virtual void Build()
      {
         DockerCompose.Build();
      }

      public virtual void Up()
      {
         DockerCompose.Up();
      }

      public virtual void Down()
      {
         DockerCompose.Down();
      }

      public virtual void Stop()
      {
         DockerCompose.Stop();
      }

      public virtual void Kill()
      {
         DockerCompose.Kill();
      }

      public virtual void Rm()
      {
         DockerCompose.Rm();
      }

      public virtual void Up(Container container)
      {
         DockerCompose.Up();
      }

      public virtual void Start(Container container)
      {
         DockerCompose.Start(container);
      }

      public virtual void Stop(Container container)
      {
         DockerCompose.Stop();
      }

      public virtual void Kill(Container container)
      {
         DockerCompose.Kill();
      }

      public virtual string Exec(DockerComposeExecOption dockerComposeExecOption, string containerName,
         DockerComposeExecArgument dockerComposeExecArgument)
      {
         return DockerCompose.Exec(dockerComposeExecOption, containerName, dockerComposeExecArgument);
      }

      public virtual string Run(DockerComposeRunOption dockerComposeRunOption, string containerName,
         DockerComposeRunArgument dockerComposeRunArgument)
      {
         return DockerCompose.Run(dockerComposeRunOption, containerName, dockerComposeRunArgument);
      }

      public virtual List<ContainerName> Ps()
      {
         return DockerCompose.Ps();
      }

      public virtual string Id(Container container)
      {
         return DockerCompose.Id(container);
      }

      public virtual string Config()
      {
         return DockerCompose.Config();
      }

      public virtual List<string> Services()
      {
         return  DockerCompose.Services();
      }

      public virtual bool WriteLogs(string container, Stream output)
      {
         return DockerCompose.WriteLogs(container, output);
      }

      public virtual Ports Ports(string service)
      {
         return DockerCompose.Ports(service);
      }

      protected IDockerCompose DockerCompose { get; }
   }
}