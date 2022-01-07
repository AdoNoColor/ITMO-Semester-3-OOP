namespace Banks.Operations
{
    public interface IOperation
    {
        public string Id { get; set; }
        public bool IsCancelled { get; set; }
        public abstract void CancelOperation();
    }
}