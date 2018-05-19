using System;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;

namespace GM.App
{
    public class Program
    {
        [Option(CommandOptionType.SingleOrNoValue, Description = "The url prefix")]
        public string Prefix { get; }

        public static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        private void OnExecuteAsync()
        {
            var prefix = Prefix ?? "http://+:5555/";

            using (var serv = new StatServer(prefix))
            {
                serv.Run();
            }
        }
    }
}