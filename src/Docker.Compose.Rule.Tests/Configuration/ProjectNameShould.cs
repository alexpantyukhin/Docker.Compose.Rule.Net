using System;
using Docker.Compose.Rule.Net.Configuration;
using NUnit.Framework;

namespace Docker.Compose.Rule.Tests.Configuration
{
   [TestFixture]
   public class ProjectNameShould
   {
      [Test]
      public void use_project_name_prefix_in_construct_compose_command() {
         var command = ProjectName.Random().ConstructComposeFileCommand();

         Assert.AreEqual(2, command.Count);
         Assert.AreEqual("--project-name", command[0]);
      }

      [Test]
      public void produce_different_names_on_successive_calls_to_random() {
         var firstCommand = ProjectName.Random().ConstructComposeFileCommand();
         var secondCommand = ProjectName.Random().ConstructComposeFileCommand();

         Assert.AreNotEqual(firstCommand[1], secondCommand[1]);
      }

      [Test]
      public void have_eight_characters_long_random() {
         var randomName = ProjectName.Random().ConstructComposeFileCommand()[1];

         Assert.AreEqual(8, randomName.Length);
      }

      [Test]
      public void should_pass_name_to_command_in_from_string_factory() {
         var command = ProjectName.FromString("projectname").ConstructComposeFileCommand();

         Assert.IsTrue(command.Contains("--project-name"));
         Assert.IsTrue(command.Contains("projectname"));
      }

      [Test]
      public void should_disallow_names_in_from_string_factory() {
         var command = ProjectName.FromString("projectname").ConstructComposeFileCommand();

         Assert.IsTrue(command.Contains("--project-name"));
         Assert.IsTrue(command.Contains("projectname"));
      }

      [Test]
      public void reject_blanks_in_from_string() {
         Assert.Throws<ArgumentException>(() => { ProjectName.FromString(" "); });
      }

      [Test]
      public void match_validation_behavior_of_docker_compose_cli() {
         Assert.Throws<ArgumentException>(() => { ProjectName.FromString("Crazy#Proj ect!Name"); });
      }

      [Test]
      public void should_return_the_project_name_when_asString_called() {
         var projectName = ProjectName.FromString("projectname").AsString();

         Assert.AreEqual("projectname", projectName);
      }
   }
}