namespace CompMath_Lab5
{
    public class DotProductMethod : IEigenpairsFindingMethod
    {
        public string Name => "Dot product";

        public IEnumerable<(double, Vector)> GetEigenpairs(SquareMatrix a, double error, Writer writer)
        {
            writer.WriteDivider();
            writer.WriteLine("Computing maximum eigenvalue:");
            writer.WriteDivider();
            var maxEigenpair = GetMaxEigenpair(a, error, writer);

            writer.WriteLine("Computing maximun eigenvalue of inverse matrix:");
            writer.WriteDivider();
            var minEigenpair = GetMaxEigenpair(a.GetInverse(), error, writer);

            minEigenpair.Item1 = 1 / minEigenpair.Item1;

            return new[] { maxEigenpair, minEigenpair };
        }

        private static (double, Vector) GetMaxEigenpair(SquareMatrix a, double error, Writer writer)
        {
            double eigenvalue = 0.0;
            double eigenvalueOld;
            Vector y = new(Enumerable.Repeat(1.0, a.Order));
            Vector yOld;
            int i = 0;

            do
            {
                (yOld, y) = (y, a * y);
                (eigenvalueOld, eigenvalue) = (eigenvalue, y * y / (yOld * y));
                i++;

                writer.WriteLine($"{i} iteration:");
                writer.WriteLine($"λ_{i} = {eigenvalue:F6}");
                writer.WriteLine($"ν_{i} = {y.GetNormalized()}");
                writer.WriteDivider();
            } while (Math.Abs(eigenvalue - eigenvalueOld) >= error);

            return (eigenvalue, y.GetNormalized());
        }
    }
}
