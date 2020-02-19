using System;

namespace FreelanceManager.Commands
{
    public class CommandLine
    {
        private const string ARGS_SEPERATOR = " ";

        private readonly string _argsStr;

        public string Name { get; }


        public CommandLine(string line)
        {
            var split = line.Split(ARGS_SEPERATOR, count: 2, StringSplitOptions.RemoveEmptyEntries);

            Name = split[0];
            _argsStr = split.Length > 1 ? split[1] : "";
        }


        public string[] GetArgs()
        {
            return _argsStr.Split(ARGS_SEPERATOR, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
