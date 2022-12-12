using System.Globalization;

namespace CompMath_Lab5
{
    public static class ArrayHelper
    {
        public static T[][] Copy<T>(T[][] source)
        {
            int height = source.Length;
            T[][] result = new T[height][];

            for (int i = 0; i < height; i++)
            {
                result[i] = new T[source[i].Length];
                source[i].CopyTo(result[i], 0);
            }
            return result;
        }

        public static double[][] ReadFromFile(string filePath)
        {
            return File.ReadAllLines(filePath)
                .Select(line =>
                    line
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(n => double.Parse(n, CultureInfo.InvariantCulture))
                    .ToArray()
                ).ToArray();
        }

        public static string ToStringVector(IEnumerable<double> vector)
        {
            return $"( {string.Join("; ", vector.Select(n => $"{n,9:F6}"))} )";
        }

        public static string ToString(double[][] matrix)
        {
            return string.Join(
                Environment.NewLine,
                matrix.Select(row =>
                    string.Join(' ', row.Select(n => $"{n,11:F6}"))
                )
            );
        }
    }
}
