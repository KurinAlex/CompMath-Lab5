namespace CompMath_Lab5
{
    public struct Polynomial
    {
        private readonly IEnumerable<double> _coefficients;

        public Polynomial(IEnumerable<double> coefficients) => _coefficients = coefficients;

        private double At(double x) => _coefficients.Select((c, i) => c * Math.Pow(x, i)).Sum();
        public IEnumerable<double> GetRoots(double error)
        {
            List<double> roots = new();
            const double step = 0.1;
            double max = 1.0 + _coefficients.SkipLast(1).Max(c => Math.Abs(c)) / Math.Abs(_coefficients.Last());
            double min = -max;

            while (min < max)
            {
                double a = min;
                double b = min + step;

                while (At(a) * At(b) > 0 && b < max)
                {
                    a += step;
                    b += step;
                }
                if (b > max)
                {
                    break;
                }

                min = b;

                while (Math.Abs(b - a) >= error || Math.Abs(At(b)) >= error)
                {
                    double valueAtA = At(a);
                    double valueAtB = At(b);
                    (a, b) = (b, (a * valueAtB - b * valueAtA) / (valueAtB - valueAtA));
                }

                roots.Add(b);
            }
            return roots;
        }
        public override string ToString() =>
            string.Join(" + ", _coefficients.Select((c, i) => $"{c:F6} * λ ^ {i}").Reverse());
    }
}
