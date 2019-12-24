using ViewModels;

namespace Services.Interfaces
{
    public interface IValidationService
    {
        ResponseValidation IsValid(object instance, bool validateAllProperties = true);
    }
}
