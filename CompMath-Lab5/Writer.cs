namespace CompMath_Lab5
{
    public class Writer
    {
        private const int DividerLength = 101;
        private static readonly string s_divider = new('-', DividerLength);

        private readonly StreamWriter _fileWriter;

        public Writer(StreamWriter fileWriter) => _fileWriter = fileWriter;

        public void WriteLine(string? line)
        {
            Console.WriteLine(line);
            _fileWriter.WriteLine(line);
        }
        public void WriteLine(object obj) => WriteLine(obj.ToString());
        public void WriteDivider() => WriteLine(s_divider);
    }
}
