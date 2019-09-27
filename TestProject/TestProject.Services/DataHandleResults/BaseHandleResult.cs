namespace TestProject.Services.DataHandleResults
{
    public abstract class BaseHandleResult<T>
    {
        public T Data { get; set; }

        public bool IsSucceded { get; set; }
    }
}
