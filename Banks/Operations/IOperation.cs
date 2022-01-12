namespace Banks.Operations
{
    public interface IOperation
    {
        string Id { get; set; }
        bool IsCancelled { get; set; }
        void CancelOperation();
    }
}