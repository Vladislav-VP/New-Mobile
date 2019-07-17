using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject.Core.ViewModelResults
{
    public class DestructionResult<TEntity>
    {
        public TEntity Entity { get; set; }

        public bool IsDestroyed { get; set; }
    }
}
