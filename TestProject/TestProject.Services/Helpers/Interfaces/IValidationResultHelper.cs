namespace TestProject.Services.Helpers.Interfaces
{
    public interface IValidationResultHelper
    {
        void HandleValidationResult<T>(T obj, string propertyName = null);
    }
}
