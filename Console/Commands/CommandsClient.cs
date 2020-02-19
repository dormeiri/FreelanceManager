using FreelanceManager.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FreelanceManager.Commands
{
    public class CommandsClient
    {
        private readonly Dictionary<string, ICommand> commandsDictionary;

        public CommandsClient()
        {
            commandsDictionary = GetCommands().ToDictionary(c => c.Name, c => c);
        }

        public ICommand GetCommand(string commandName)
        {
            if (!commandsDictionary.TryGetValue(commandName, out var command))
            {
                throw new CommandNotFoundException(commandName);
            }

            return command;
        }

        public bool IsExitCommand(ICommand command)
        {
            return command is ExitCommand;
        }

        private IEnumerable<ICommand> GetCommands()
        {
            return from t in Assembly.GetExecutingAssembly().GetTypes()
                   where t.IsClass && typeof(ICommand).IsAssignableFrom(t)
                   select (ICommand)Activator.CreateInstance(t);
        }
    }
}
