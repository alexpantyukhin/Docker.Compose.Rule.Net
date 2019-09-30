using System;
using Docker.Compose.Rule.Net.Connection;
using Docker.Compose.Rule.Net.Connection.Waiting;
using Docker.Compose.Rule.Net.Execution;
using NSubstitute;
using NUnit.Framework;

namespace Docker.Compose.Rule.Tests.Connection.Waiting
{
   [TestFixture]
   public class ClusterWaitShould
   {
      private static readonly TimeSpan Duration = TimeSpan.FromSeconds(1);
      private static readonly string Ip = "192.168.100.100";

      private ContainerCache _containerCache;
      private Func<Cluster, SuccessOrFailure> _clusterHealthCheck;
      private Cluster _cluster;

      [SetUp]
      public void Setup()
      {
         var dockerCompose = Substitute.For<IDockerCompose>();
         _containerCache = new ContainerCache(new Net.Execution.Docker(new Command(Substitute.For<IExecutable>())), dockerCompose);
         _clusterHealthCheck = null;
         _cluster = new Cluster(Ip, _containerCache);
      }

      [Test]
      public void return_when_a_cluster_is_ready()
      {
         var wait = new ClusterWait(
            cluster => (_cluster == cluster) ? SuccessOrFailure.Success() : SuccessOrFailure.Failure(""),
            Duration);

         wait.WaitUntilReady(_cluster);
      }
      
      [Test]
      public void check_until_a_cluster_is_ready()
      {
         var wait = new ClusterWait(
            ClusterWaitFailureSuccess,
            Duration);
         
         wait.WaitUntilReady(_cluster);
         Assert.AreEqual(1, _state);
      }

      private int _state= 0;
      private SuccessOrFailure ClusterWaitFailureSuccess(Cluster cluster)
      {
         if (_state == 0)
         {
            _state = 1;
            return SuccessOrFailure.Failure("failure!");
         }

         return SuccessOrFailure.Success();
      }
      
      [Test]
      [Timeout(2000)]
      public void timeout_if_the_cluster_is_not_healthy() {
         
         var wait = new ClusterWait(
            cluster => SuccessOrFailure.Failure(""),
            Duration);
         Assert.Throws<InvalidOperationException>(() => { wait.WaitUntilReady(_cluster); });
      }
   }
}