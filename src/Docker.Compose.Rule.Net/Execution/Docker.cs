using System;
using System.Collections.Generic;
using Docker.Compose.Rule.Net.Connection;

namespace Docker.Compose.Rule.Net.Execution
{
   public class Docker
   {
      private readonly Command _command;

      public Docker(Command command)
      {
         _command = command;
      }
      
      public void Rm(List<string> containerNames)
      {
         Rm(containerNames.ToArray());
      }

      public void Rm(params string[] containerNames)
      {
         var list = new List<string>() {"rm", "-f"};
         list.AddRange(containerNames);
         _command.Execute(Command.ThrowingOnError(), list.ToArray());
      }

      public void PruneNetworks()
      {
         throw new System.NotImplementedException();
      }

      public State State(string id)
      {
         // TODO: Implement
         throw new System.NotImplementedException();
      }
   }
}