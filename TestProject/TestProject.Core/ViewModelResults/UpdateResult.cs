using TestProject.Core.ViewModelResults.Interfaces;

namespace TestProject.Core.ViewModelResults
{
    public class UpdateResult<TEntity> : IViewModelResult<TEntity>
    {
        public TEntity Entity { get; set; }

        public bool IsUpdated { get; set; }
    }
}
