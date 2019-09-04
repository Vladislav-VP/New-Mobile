using TestProject.Core.ViewModelResults.Interfaces;

namespace TestProject.Core.ViewModelResults
{
    public class CreationResult<TEntity> : IViewModelResult<TEntity>
    {
        public TEntity Entity { get; set; }

        public bool IsCreated { get; set; }
    }
}
