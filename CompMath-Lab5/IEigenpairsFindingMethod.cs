namespace CompMath_Lab5
{
    public interface IEigenpairsFindingMethod
    {
        string Name { get; }
        IEnumerable<(double, Vector)> GetEigenpairs(SquareMatrix a, double error, Writer writer);
    }
}
