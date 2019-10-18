using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestProject.Services.Helpers.Interfaces
{
    public interface IValidationHelper
    {
        bool IsObjectValid<T>(T obj, string propertyName = null);
    }
}
