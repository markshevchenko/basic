namespace Basic
{
    using System;

    public class ConsoleInputOutput : IInputOutput
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void Write(string s)
        {
            Console.Write(s);
        }

        public void WriteLine()
        {
            Console.WriteLine();
        }

        public void WriteLine(string s)
        {
            Console.WriteLine(s);
        }
    }
}
