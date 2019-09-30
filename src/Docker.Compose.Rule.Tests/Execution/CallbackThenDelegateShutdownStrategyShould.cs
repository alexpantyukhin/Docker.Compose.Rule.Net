using System;
using Docker.Compose.Rule.Net.Configuration;
using Docker.Compose.Rule.Net.Execution;
using NSubstitute;
using NUnit.Framework;

namespace Docker.Compose.Rule.Tests.Execution
{
   [TestFixture]
   public class CallbackThenDelegateShutdownStrategyShould
   {
      [Test]
      public void call_callback_then_call_delegate_on_stop()
      {
         var deleg = Substitute.For<IShutdownStrategy>();
         var dockerCompose = Substitute.For<IDockerCompose>();
         var callBack = Substitute.For<Action>();

         ShutdownStrategy.CallbackAndThen(callBack, deleg).Stop(dockerCompose);

         callBack.Received().Invoke();
         deleg.Received().Stop(dockerCompose);
         //TODO: verifyNoMoreInteractions(callback, delegate);
      }

      [Test]
      public void call_delegate_even_when_callback_throws_on_stop()
      {
         var deleg = Substitute.For<IShutdownStrategy>();
         var exception = new Exception();
         Action callBack = () => { throw exception; };

         var dockerCompose = Substitute.For<IDockerCompose>();

         try
         {
            ShutdownStrategy.CallbackAndThen(callBack, deleg).Stop(dockerCompose);
            Assert.Fail("expected exception");
         }
         catch (Exception e)
         {
            Assert.AreEqual(exception, e);
         }

         deleg.Received().Stop(dockerCompose);
         //TODO: verifyNoMoreInteractions(callback, delegate);
      }

      [Test]
      public void call_down_on_shutdown()
      {
         var deleg = Substitute.For<IShutdownStrategy>();
         var dockerCompose = Substitute.For<IDockerCompose>();
         var callBack = Substitute.For<Action>();
         var docker = new Net.Execution.Docker(new Command(Substitute.For<IExecutable>()));

         ShutdownStrategy.CallbackAndThen(callBack, deleg).Shutdown(dockerCompose, docker);

         deleg.Received().Shutdown(dockerCompose, docker);
         //TODO: verifyNoMoreInteractions(callback, delegate);
      }
   }
}