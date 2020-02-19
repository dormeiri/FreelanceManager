using System;

namespace FreelanceManager.Exceptions
{

    [Serializable]
    public class InvalidNumberOfArgumentsException : CommandLineException
    {
        public InvalidNumberOfArgumentsException() : base($"Invalid number of arguments") { }
    }
}
