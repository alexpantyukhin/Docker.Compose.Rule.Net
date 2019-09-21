using System;
using System.Collections.Generic;
using Docker.Compose.Rule.Net.Configuration;
using KellermanSoftware.CompareNetObjects;
using NUnit.Framework;

namespace Docker.Compose.Rule.Tests.Configuration
{
   [TestFixture]
   public class AdditionalEnvironmentValidatorShould
   {
      [Test]
      public void throw_exception_when_additional_environment_variables_contain_docker_variables()
      {
         var variables = new Dictionary<string, string>()
         {
            {"DOCKER_HOST", "tcp://some-host:2376"},
            {"SOME_VARIABLE", "Some Value"}
         };
         
         //exception.expect(IllegalStateException.class);
         //exception.expectMessage("The following variables");
         //exception.expectMessage("DOCKER_HOST");
         //exception.expectMessage("cannot exist in your additional environment");

         Assert.Throws<InvalidOperationException>(() =>
         {
            AdditionalEnvironmentValidator.Validate(variables);
         });
      }
      
      [Test]
      public void validate_arbitrary_environment_variables() {
         var variables = new Dictionary<string, string>()
         {
            {"SOME_VARIABLE", "Some Value"}
         };
         
         var compareLogic = new CompareLogic();
         Assert.True(compareLogic.Compare(AdditionalEnvironmentValidator.Validate(variables), variables).AreEqual);
      }
   }
}