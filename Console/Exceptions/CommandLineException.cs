using System;

namespace FreelanceManager.Exceptions
{

    [Serializable]
    public class CommandLineException : Exception
    {
        public CommandLineException(string message) : base(message) { }
    }
}
