namespace Lab3._1.Core.Interfaces
{
    public interface IMenuItem
    {
        string Name { get; }
        decimal Price { get; }
        void Display(int depth);
    }
}