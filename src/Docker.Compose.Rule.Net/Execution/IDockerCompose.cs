using System.Collections.Generic;
using System.IO;
using Docker.Compose.Rule.Net.Connection;

namespace Docker.Compose.Rule.Net.Execution
{
   public interface IDockerCompose
   {
      void Pull();
      void Build();
      void Up();
      void Down();
      void Stop();
      void Kill();
      void Rm();
      void Up(Container container) ;
      void Start(Container container) ;
      void Stop(Container container) ;
      void Kill(Container container) ;
      string Exec(DockerComposeExecOption dockerComposeExecOption, string containerName, DockerComposeExecArgument dockerComposeExecArgument) ;
      string Run(DockerComposeRunOption dockerComposeRunOption, string containerName, DockerComposeRunArgument dockerComposeRunArgument) ;
      List<ContainerName> Ps() ;
      string Id(Container container) ;
      string Config() ;
      List<string> Services() ;
      bool WriteLogs(string container, Stream output);
      Ports Ports(string service) ;
   }
}