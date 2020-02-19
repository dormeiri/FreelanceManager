using System;

namespace FreelanceManager.Exceptions
{

    [Serializable]
    public class CommandNotFoundException : CommandLineException
    {
        public CommandNotFoundException(string command) : base($"Command not found: {command}") { }
    }
}
