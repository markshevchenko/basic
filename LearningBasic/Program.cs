namespace LearningBasic
{
    using System.Reflection;
    using LearningBasic.CodeGenerating;
    using LearningBasic.Parsing;

    class Program
    {
        private static void Main(string[] args)
        {
            var inputOutput = new ConsoleInputOutput();
            var runtimeSystem = new RunTimeEnvironment(inputOutput);
            var scannerFactory = new BasicScannerFactory();
            var parser = new BasicParser(scannerFactory);
            var commandBuilder = new BasicCommandBuilder();
            var repl = new Repl<Tag>(runtimeSystem, parser, commandBuilder);

            Salute(inputOutput);
            repl.Run();
        }

        private static void Salute(IInputOutput inputOutput)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var title = assembly.GetName().Name;
            var version = assembly.GetName().Version;
            var message = string.Format("{0} {1}", title, version);
            inputOutput.WriteLine(message);
        }
    }
}
