using DataAccess;
using FreelanceManager.Commands;
using FreelanceManager.Exceptions;
using System;
using System.IO;
using System.Reflection;

namespace FreelanceManager
{
    internal class Program
    {
        private const string PROMPT_SIGN = "> ";

        private static void Main()
        {
            var commandsClient = new CommandsClient();
            var ctx = new Context(GetContextDirectory());

            Listener(ctx, commandsClient);
        }

        private static void Listener(Context ctx, CommandsClient commandsClient)
        {
            ICommand command;

            do
            {
                try
                {
                    Console.Write(PROMPT_SIGN);

                    var commandLine = new CommandLine(Console.ReadLine());
                    command = commandsClient.GetCommand(commandLine.Name);

                    command.Run(ctx, commandLine.GetArgs());
                }
                catch (CommandLineException ex)
                {
                    Console.WriteLine(ex.Message);
                    command = null;
                }

                Console.WriteLine();
            }
            while (!commandsClient.IsExitCommand(command));
        }

        private static string GetContextDirectory()
        {
            var myDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var assemblyName = Assembly.GetEntryAssembly().GetName().Name;

            return Path.Combine(myDocs, assemblyName);
        }
    }
}
