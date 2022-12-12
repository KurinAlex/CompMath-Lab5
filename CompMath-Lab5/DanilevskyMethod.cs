namespace CompMath_Lab5
{
    public class DanilevskyMethod : IEigenpairsFindingMethod
    {
        public string Name => "Danilevsky";

        public IEnumerable<(double, Vector)> GetEigenpairs(SquareMatrix a, double error, Writer writer)
        {
            int n = a.Order;
            SquareMatrix s = SquareMatrix.GetIdentity(n);
            SquareMatrix p = new(a);

            writer.WriteDivider();
            writer.WriteLine("Transformation process:");
            writer.WriteDivider();

            for (int k = n - 2; k >= 0; k--)
            {
                SquareMatrix m = SquareMatrix.GetIdentity(n);
                SquareMatrix mInverse = SquareMatrix.GetIdentity(n);
                for (int i = 0; i < n; i++)
                {
                    m[k, i] = i == k ? 1.0 / p[k + 1, k] : -p[k + 1, i] / p[k + 1, k];
                    mInverse[k, i] = p[k + 1, i];
                }

                p = mInverse * p * m;
                s *= m;

                writer.WriteLine(p);
                writer.WriteDivider();
            }

            var coefficients = Enumerable.Range(0, p.Order)
                .Select(k => -p[0, k])
                .Reverse()
                .Append(1.0);

            Polynomial polynomial = new(coefficients);

            writer.WriteLine("Characteristic polynomial:");
            writer.WriteLine(polynomial);
            writer.WriteDivider();

            var eigenvalues = polynomial.GetRoots(error);

            return GetEigenpairs(eigenvalues, s);
        }

        private static IEnumerable<(double, Vector)> GetEigenpairs(IEnumerable<double> eigenvalues, SquareMatrix s)
        {
            int n = s.Order;
            List<(double, Vector)> res = new(n);
            foreach (double eigenvalue in eigenvalues)
            {
                Vector y = new(Enumerable.Range(1, n).Select(k => Math.Pow(eigenvalue, n - k)));
                res.Add((eigenvalue, (s * y).GetNormalized()));
            }
            return res;
        }
    }
}
