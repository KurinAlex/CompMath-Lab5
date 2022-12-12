namespace CompMath_Lab5
{
    public struct Vector
    {
        private readonly double[] _vector;

        public Vector(IEnumerable<double> vector) => _vector = vector.ToArray();

        public int Length => _vector.Length;
        public double Norm => Math.Sqrt(_vector.Sum(x => x * x));
        public double this[int i] => _vector[i];

        public Vector GetNormalized() => this / Norm;
        public override string ToString() => ArrayHelper.ToStringVector(_vector);

        public static Vector operator /(Vector vector, double scale)
            => new(vector._vector.Select(n => n / scale));
        public static double operator *(Vector left, Vector right) =>
            left._vector.Zip(right._vector).Sum(p => p.First * p.Second);
    }
}
