using System;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;

namespace GM.App
{
    public class Program
    {
        public static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        [Option(Description = "The url prefix", ShortName = "p", LongName = "prefix", ValueName = "http://+:5555/")]
        public string Prefix { get; }

        private void OnExecute()
        {
            var prefix = Prefix ?? "http://+:5555/";

            using (var serv = new StatServer(prefix))
            {
                serv.Run();
            }

        }
    }
}