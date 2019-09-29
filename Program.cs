using System;
using CarnotaurusV2.Program_Manager;
using NLog;

namespace CarnotaurusV2
{
    class Program
    {
        static void InitializeLogger(){
            var config = new NLog.Config.LoggingConfiguration();
            var console = new NLog.Targets.ConsoleTarget("logconsole");
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, console);
            NLog.LogManager.Configuration = config;
        }
        static void Main(string[] args)
        {
            InitializeLogger();
            Prg_Manager ola = new Prg_Manager();
        }
    }
}
