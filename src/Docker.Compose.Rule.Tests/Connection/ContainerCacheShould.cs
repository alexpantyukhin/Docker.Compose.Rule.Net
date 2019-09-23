using Docker.Compose.Rule.Net.Connection;
using Docker.Compose.Rule.Net.Execution;
using NUnit.Framework;
using NSubstitute;

namespace Docker.Compose.Rule.Tests.Connection
{
   [TestFixture]
   public class ContainerCacheShould
   {
      private const string CONTAINER_NAME = "container";
      private  ContainerCache _containers;

      [SetUp]
      public void Setup()
      {
         var docker = Substitute.For<Net.Execution.Docker>();
         var dockerCompose = Substitute.For<IDockerCompose>();
         _containers = new ContainerCache(docker, dockerCompose);
      }

      [Test]
      public void return_a_container_with_the_specified_name_when_getting_a_new_container() {
         var container = _containers.Container(CONTAINER_NAME);

         Assert.AreEqual(CONTAINER_NAME, container.GetContainerName());
         // TODO: handle the same
         //assertThat(container, is(new Container(CONTAINER_NAME, docker, dockerCompose)));
      }

      [Test]
      public void return_the_same_object_when_getting_a_container_twice() {
         var container = _containers.Container(CONTAINER_NAME);
         var sameContainer = _containers.Container(CONTAINER_NAME);
         Assert.AreEqual(container, sameContainer);
      }
   }
}