namespace TestProject.Services.DataHandleResults
{
    public class DataHandleResult<T>
    {
        public T Data { get; set; }

        public bool IsSucceded { get; set; }
    }
}
