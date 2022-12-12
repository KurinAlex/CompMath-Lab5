namespace CompMath_Lab5
{
    public struct SquareMatrix
    {
        private readonly double[][] _matrix;

        public SquareMatrix(IEnumerable<IEnumerable<double>> matrix)
        {
            var source = matrix.Select(r => r.ToArray()).ToArray();

            if (source.Any(row => row.Length != source[0].Length))
            {
                throw new ArgumentException("Matrix has different row dimentions");
            }

            if (source.Length != source[0].Length)
            {
                throw new ArgumentException("Matrix is not square");
            }

            _matrix = ArrayHelper.Copy(source);
        }
        public SquareMatrix(string filePath) : this(ArrayHelper.ReadFromFile(filePath))
        {
        }
        public SquareMatrix(SquareMatrix matrix) : this(matrix._matrix)
        {
        }

        public int Order => _matrix.Length;
        public double this[int i, int j]
        {
            get => _matrix[i][j];
            set => _matrix[i][j] = value;
        }

        public SquareMatrix GetInverse()
        {
            int n = Order;
            int m = n * 2;
            double[][] matrix = _matrix
                .Select((row, i) => row
                    .Concat(Enumerable.Repeat(0.0, i))
                    .Append(1.0)
                    .Concat(Enumerable.Repeat(0.0, n - i - 1))
                    .ToArray())
                .ToArray();

            for (int k = 0; k < n; k++)
            {
                double f = matrix[k][k];
                for (int j = k; j < m; j++)
                {
                    matrix[k][j] /= f;
                }

                for (int i = 0; i < n; i++)
                {
                    if (i == k)
                    {
                        continue;
                    }

                    f = matrix[i][k] / matrix[k][k];
                    for (int j = k; j < m; j++)
                    {
                        matrix[i][j] -= matrix[k][j] * f;
                    }
                }
            }
            return new(matrix.Select(row => row.Skip(n)));
        }
        public override string ToString() =>
            ArrayHelper.ToString(_matrix);

        public static SquareMatrix GetIdentity(int n) =>
            new(Enumerable.Range(0, n)
                .Select(k =>
                    Enumerable.Repeat(0.0, k)
                    .Append(1.0)
                    .Concat(Enumerable.Repeat(0.0, n - k - 1))
                    .ToArray())
                .ToArray());

        public static SquareMatrix operator *(SquareMatrix left, SquareMatrix right)
        {
            int n = left.Order;

            if (n != right.Order)
            {
                throw new ArgumentException("Matrixes orders are not equal");
            }

            double[][] res = new double[n][];

            for (int i = 0; i < n; i++)
            {
                res[i] = new double[n];
                for (int j = 0; j < n; j++)
                {
                    for (int k = 0; k < n; k++)
                    {
                        res[i][j] += left[i, k] * right[k, j];
                    }
                }
            }

            return new SquareMatrix(res);
        }
        public static Vector operator *(SquareMatrix matrix, Vector vector)
        {
            if (matrix.Order != vector.Length)
            {
                throw new ArgumentException("Sizes of matrix and vector are not equal");
            }

            return new(matrix._matrix
                .Select(d => d
                    .Select((n, i) => (n, i))
                    .Sum(t => t.n * vector[t.i])));
        }
    }
}
