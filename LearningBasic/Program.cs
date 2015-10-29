namespace LearningBasic
{
    using System.Reflection;
    using LearningBasic.Compiling;
    using LearningBasic.Parsing;
    using LearningBasic.RunTime;

    class Program
    {
        private static void Main(string[] args)
        {
            var inputOutput = new ConsoleInputOutput();
            var rte = new RunTimeEnvironment(inputOutput);
            var scannerFactory = new BasicScannerFactory();
            var parser = new BasicParser(scannerFactory);
            var commandBuilder = new BasicCompiler();
            var readEvaluatePrintLoop = new ReadEvaluatePrintLoop<Tag>(rte, parser, commandBuilder);

            PrintSalute(inputOutput);
            Run(readEvaluatePrintLoop);
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
            if (attribute != null)
                inputOutput.WriteLine(attribute.Copyright);
        }

        private static void Run(ReadEvaluatePrintLoop<Tag> readEvaluatePrintLoop)
        {
            do
            {
                readEvaluatePrintLoop.TakeStep();
            }
            while (!readEvaluatePrintLoop.IsTerminated);
        }
    }
}
