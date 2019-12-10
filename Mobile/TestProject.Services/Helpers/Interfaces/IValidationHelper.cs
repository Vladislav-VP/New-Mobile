namespace TestProject.Services.Helpers.Interfaces
{
    public interface IValidationHelper
    {
        bool IsObjectValid<T>(T obj, bool validaateAllProperties = true);
    }
}
