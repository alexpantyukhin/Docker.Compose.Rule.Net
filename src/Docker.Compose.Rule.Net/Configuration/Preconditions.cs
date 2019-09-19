using System;

namespace Docker.Compose.Rule.Net.Configuration
{
   public static class Preconditions
   {
      public static void CheckState(bool expression,  string errorMessage) {
         if (!expression)
         {
            throw new InvalidOperationException(errorMessage);
         }
      }
   }
}