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

      public static SuccessOrFailure FromBoolean(bool succeeded, string possibleFailureMessage)
      {
         if (succeeded) {
            return Success();
         } else {
            return Failure(possibleFailureMessage);
         }
      }

    public SuccessOrFailure MapFailure(Func<string, string> mapper) {
        if (this.Succeeded()) {
            return this;
        } else {
            return Failure(mapper(FailureMessage));
        }
    }
   }
}