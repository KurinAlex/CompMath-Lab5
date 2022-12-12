using System.Text;

namespace CompMath_Lab5
{
    public class Program
    {
        const string InputMatrixFile = @"D:\Sources\University\2 course\CompMath\CompMath-Lab5\Input\Matrix.txt";
        const string OutputFileName = "Output.txt";

        const double Error = 1e-5;

        static readonly IEigenpairsFindingMethod[] methods = new IEigenpairsFindingMethod[]
        {
            new DotProductMethod(),
            new DanilevskyMethod()
        };

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;

            using (StreamWriter fileWriter = new(OutputFileName))
            {
                Writer writer = new(fileWriter);
                SquareMatrix a = new(InputMatrixFile);

                writer.WriteDivider();
                writer.WriteLine("Input matrix:");
                writer.WriteLine(a);
                writer.WriteDivider();

                foreach (var method in methods)
                {
                    writer.WriteDivider();
                    writer.WriteLine($"{method.Name} method:");
                    writer.WriteDivider();

                    var eigenpairs = method.GetEigenpairs(a, Error, writer);

                    writer.WriteLine("Eigenvalues and corresponding eigenvectors:");
                    writer.WriteDivider();

                    int i = 1;
                    foreach (var (eigenvalue, eigenvector) in eigenpairs)
                    {
                        writer.WriteLine($"λ_{i} = {eigenvalue:F6}");
                        writer.WriteLine($"ν_{i} = {eigenvector}");
                        writer.WriteDivider();
                        i++;
                    }
                }
            }
            Console.ReadKey();
        }
    }
}