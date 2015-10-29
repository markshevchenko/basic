namespace LearningBasic
{
    using System.Reflection;
    using LearningBasic.Compiling;
    using LearningBasic.Parsing;
    using LearningBasic.RunTime;
    using LearningBasic.Console;

    class Program
    {
        private static void Main(string[] args)
        {
            var inputOutput = new ConsoleInputOutput();
            var runtimeSystem = new RunTimeEnvironment(inputOutput);
            var scannerFactory = new BasicScannerFactory();
            var parser = new BasicParser(scannerFactory);
            var commandBuilder = new BasicStatementCompiler();
            var repl = new ReadEvaluatePrintLoop(runtimeSystem, parser, commandBuilder);

            PrintSalute(inputOutput);
            repl.Run();
        }

        private static void PrintSalute(IInputOutput inputOutput)
        {
            var assembly = Assembly.GetExecutingAssembly();
            PrintTitleAndVersion(inputOutput, assembly);
            PrintCopyright(inputOutput, assembly);
        }

        private static void PrintTitleAndVersion(IInputOutput inputOutput, Assembly assembly)
        {
            var assemblyName = assembly.GetName();
            inputOutput.WriteLine("{0} {1}", assemblyName.Name, assemblyName.Version);
        }

        private static void PrintCopyright(IInputOutput inputOutput, Assembly assembly)
        {
            var attribute = assembly.GetCustomAttribute<AssemblyCopyrightAttribute>();
            if (attribute == null)
                return;

            inputOutput.WriteLine(attribute.Copyright);
        }
    }
}
