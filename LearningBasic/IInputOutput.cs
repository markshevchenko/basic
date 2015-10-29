namespace LearningBasic
{
    /// <summary>
    /// 
    /// </summary>
    public interface IInputOutput
    {
        string ReadLine();

        void Write(string s);

        void Write(string format, params object[] args);

        void WriteLine(string s);

        void WriteLine(string format, params object[] args);

        void WriteLine();
    }
}
