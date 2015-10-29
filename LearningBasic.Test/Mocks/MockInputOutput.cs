namespace LearningBasic.Test.Mocks
{
    public class MockInputOutput : IInputOutput
    {
        private readonly string inputString;

        public string OutputString { get; private set; }

        public MockInputOutput(string inputString)
        {
            this.inputString = inputString;
        }

        public string ReadLine()
        {
            return inputString;
        }

        public void Write(string s)
        {
            OutputString = s;
        }

        public void Write(string format, params object[] args)
        {
            OutputString = string.Format(format, args);
        }

        public void WriteLine()
        {
            OutputString = "\n";
        }

        public void WriteLine(string s)
        {
            OutputString = s + "\n";
        }

        public void WriteLine(string format, params object[] args)
        {
            OutputString = string.Format(format, args) + "\n";
        }
    }
}
