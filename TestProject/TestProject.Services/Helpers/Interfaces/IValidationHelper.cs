using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestProject.Services.Helpers.Interfaces
{
    public interface IValidationHelper
    {
        bool TryValidateObject<T>(T obj, string propertyName = null);

        ICollection<ValidationResult> ValidateObject<T>(T obj, string propertyName = null);
    }
}
