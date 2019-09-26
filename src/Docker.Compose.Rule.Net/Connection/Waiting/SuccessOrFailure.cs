using System;

namespace Docker.Compose.Rule.Net.Connection.Waiting
{
   public class SuccessOrFailure
   {
      public string FailureMessage { get; }

      private SuccessOrFailure(string failureMessage)
      {
         FailureMessage = failureMessage;
      }
      
      public static SuccessOrFailure Failure(string theFollowingContainersAreNotHealthy)
      {
         return new SuccessOrFailure(theFollowingContainersAreNotHealthy);
      }

      public static SuccessOrFailure Success()
      {
         return new SuccessOrFailure(null);
      }

      public static SuccessOrFailure FromException(Exception exception)
      {
         return SuccessOrFailure.Failure("Encountered an exception: " + exception.StackTrace);
      }
      
      public bool Failed()
      {
         return FailureMessage != null;
      }

      public bool Succeeded()
      {
         return !Failed();
      }
   }
}