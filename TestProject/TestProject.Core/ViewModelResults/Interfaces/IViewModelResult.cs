namespace TestProject.Core.ViewModelResults.Interfaces
{
    public interface IViewModelResult<TEntity>
    {
        TEntity Entity { get; set; }
    }
}
