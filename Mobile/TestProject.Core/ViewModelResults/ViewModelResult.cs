namespace TestProject.Core.ViewModelResults
{
    public class ViewModelResult<TEntity>
    {
        public TEntity Entity { get; set; }

        public bool IsSucceded { get; set; }
    }
}
