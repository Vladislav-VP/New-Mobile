using TestProject.Core.ViewModelResults.Interfaces;

namespace TestProject.Core.ViewModelResults
{
    public class DeletionResult<TEntity> : IViewModelResult<TEntity>
    {
        public TEntity Entity { get; set; }

        public bool IsDeleted { get; set; }
    }
}
