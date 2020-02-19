using DataAccess;
using System;
using System.IO;
using System.Reflection;

namespace FreelanceManager.Desktop
{
    public class ContextProducer
    {
        public static Lazy<Context> Ctx = new Lazy<Context>(GetContext);

        private static Context GetContext()
        {
            return new Context(GetContextDirectory());
        }

        private static string GetContextDirectory()
        {
            var myDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var assemblyName = Assembly.GetEntryAssembly().GetName().Name;

            return Path.Combine(myDocs, assemblyName);
        }
    }
}
