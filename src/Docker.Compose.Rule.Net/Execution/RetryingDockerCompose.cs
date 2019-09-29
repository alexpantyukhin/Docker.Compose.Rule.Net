namespace Docker.Compose.Rule.Net.Execution
{
   public class RetryingDockerCompose: DelegatingDockerCompose
   {
      private readonly Retryer _retryer;

      public RetryingDockerCompose(int retryAttempts, IDockerCompose dockerCompose) : this(
         new Retryer(retryAttempts, Retryer.StandardDelay), dockerCompose)
      {
      }

      public RetryingDockerCompose(Retryer retryer, IDockerCompose dockerCompose) : base(dockerCompose)
      {
         _retryer = retryer;
      }

      public override void Up()
      {
         _retryer.RunWithRetries(() =>
         {
            ((DelegatingDockerCompose) this).Up();
            return (object) null;
         });
      }
   }
}