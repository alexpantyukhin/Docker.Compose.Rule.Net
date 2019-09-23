using System.Linq;
using Docker.Compose.Rule.Net.Connection;
using NUnit.Framework;

namespace Docker.Compose.Rule.Tests.Connection
{
   [TestFixture]
   public class ContainerNameShould
   {
      [Test]
      public void parse_a_semantic_and_raw_name_correctly_from_a_single_line()
      {
         var actual = Net.Connection.ContainerName.FromPsLine("dir_db_1 other line contents");

         Assert.AreEqual("dir_db_1", actual.RawName);
         Assert.AreEqual("db", actual.SemanticName);
      }

      [Test]
      public void can_handle_custom_container_names()
      {
         var name = Net.Connection.ContainerName.FromPsLine(
             "test-1.container.name   /docker-entrypoint.sh postgres   Up      5432/tcp");

         Assert.AreEqual("test-1.container.name", name.RawName);
         Assert.AreEqual("test-1.container.name", name.SemanticName);
      }

      [Test]
      public void result_in_no_container_names_when_ps_output_is_empty()
      {
         var names = ContainerNames.ParseFromDockerComposePs("\n----\n");
         Assert.AreEqual(0, names.Count);
      }

      [Test]
      public void result_in_a_single_container_name_when_ps_output_has_a_single_container() {
         var names = ContainerNames.ParseFromDockerComposePs("\n----\ndir_db_1 other line contents");
         var containerName = ContainerName("dir", "db", "1");
         Assert.IsTrue(
            names.Any(c => c.RawName == containerName.RawName && c.SemanticName == containerName.SemanticName));
      }

      [Test]
      public void allow_windows_newline_characters() {
         var names = ContainerNames.ParseFromDockerComposePs("\r\n----\r\ndir_db_1 other line contents");
         var containerName = ContainerName("dir", "db", "1");
         Assert.IsTrue(
           names.Any(c => c.RawName == containerName.RawName && c.SemanticName == containerName.SemanticName));
      }

      [Test]
      public void allow_containers_with_underscores_in_their_name() {
         var names = ContainerNames.ParseFromDockerComposePs("\n----\ndir_left_right_1 other line contents");
         var containerName = ContainerName("dir", "left_right", "1");
         Assert.IsTrue(
            names.Any(c => c.RawName == containerName.RawName && c.SemanticName == containerName.SemanticName));
      }

      [Test]
      public void result_in_two_container_names_when_ps_output_has_two_containers() {
         var names = ContainerNames.ParseFromDockerComposePs("\n----\ndir_db_1 other line contents\ndir_db2_1 other stuff");
         var containerName1 = ContainerName("dir", "db", "1");
         var containerName2 = ContainerName("dir", "db2", "1");
         Assert.IsTrue(
            names.Any(c => c.RawName == containerName1.RawName && c.SemanticName == containerName1.SemanticName));
         Assert.IsTrue(
            names.Any(c => c.RawName == containerName2.RawName && c.SemanticName == containerName2.SemanticName));
      }

      [Test]
      public void ignore_an_empty_line_in_ps_output() {
         var names = ContainerNames.ParseFromDockerComposePs("\n----\ndir_db_1 other line contents\n\n");
         var containerName = ContainerName("dir", "db", "1");
         Assert.IsTrue(
            names.Any(c => c.RawName == containerName.RawName && c.SemanticName == containerName.SemanticName));
      }

      [Test]
      public void ignore_a_line_with_ony_spaces_in_ps_output() {
         var names = ContainerNames.ParseFromDockerComposePs("\n----\ndir_db_1 other line contents\n   \n");
         var containerName = ContainerName("dir", "db", "1");
         Assert.IsTrue(
            names.Any(c => c.RawName == containerName.RawName && c.SemanticName == containerName.SemanticName));
      }

      private static ContainerName ContainerName(string project, string semantic, string number)
      {
         return new ContainerName(project + "_" + semantic + "_" + number, semantic);
      }
   }
}