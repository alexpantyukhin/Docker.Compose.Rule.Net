using System;
using System.IO;
using System.Net;
using Docker.Compose.Rule.Net.Configuration;
using NUnit.Framework;

namespace Docker.Compose.Rule.Tests.Configuration
{
   [TestFixture]
   public class DockerComposeFilesShould
   {
      [Test]
      public void throw_exception_when_compose_file_is_not_specified() {
         Assert.Throws<InvalidOperationException>(() => { DockerComposeFiles.From(); });
      }

      [Test]
      public void throw_exception_when_compose_file_does_not_exist()
      {
         Assert.Throws<InvalidOperationException>(() => { DockerComposeFiles.From("does-not-exist.yaml"); });
      }
      
      [Test]
      public void throw_correct_exception_when_there_is_a_single_missing_compose_file_with_an_existing_compose_file()
      {
         var path = Path.Join(Path.GetTempPath(), "docker-compose.yaml");
         var file = File.Create(path);
         file.Close();

         Assert.Throws<InvalidOperationException>(() => { DockerComposeFiles.From("does-not-exist.yaml", path); });
         File.Delete(path);
      }

      [Test]
      public void generate_docker_compose_file_command_correctly_for_single_compose_file()  {
         var path = Path.Join(Path.GetTempPath(), "docker-compose.yaml");
         var file = File.Create(path);
         file.Close();
         var dockerComposeFiles = DockerComposeFiles.From(path);

         var composeFileCommands = dockerComposeFiles.ConstructComposeFileCommand();
         Assert.True( composeFileCommands.Contains("--file"));
         Assert.True( composeFileCommands.Contains(path));
         File.Delete(path);
      }

      [Test]
      public void generate_docker_compose_file_command_correctly_for_multiple_compose_files() {
         var path1 = Path.Join(Path.GetTempPath(), "docker-compose1.yaml");
         var file1 = File.Create(path1);
         file1.Close();
         
         var path2 = Path.Join(Path.GetTempPath(), "docker-compose2.yaml");
         var file2 = File.Create(path2);
         file2.Close();

         var dockerComposeFiles = DockerComposeFiles.From(path1, path2);
         var composeFileCommands = dockerComposeFiles.ConstructComposeFileCommand();

         Assert.True( composeFileCommands.Contains("--file"));
         Assert.True( composeFileCommands.Contains(path1));
         Assert.True( composeFileCommands.Contains(path2));
      }
   }
}