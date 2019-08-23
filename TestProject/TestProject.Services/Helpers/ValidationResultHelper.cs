using MvvmCross;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TestProject.Services.Helpers.Interfaces;

namespace TestProject.Services.Helpers
{
    public class ValidationResultHelper : IValidationResultHelper
    {
        private readonly IValidationHelper _validationHelper;

        private readonly IDialogsHelper _dialogsHelper;

        public ValidationResultHelper()
        {
            _validationHelper = Mvx.IoCProvider.Resolve<IValidationHelper>();
            _dialogsHelper = Mvx.IoCProvider.Resolve<IDialogsHelper>();
        }

        public void HandleValidationResult<T>(T obj, string propertyName = null)
        {
            ICollection<ValidationResult> errors = _validationHelper.ValidateObject<T>(obj, propertyName);

            ValidationResult error = errors.FirstOrDefault();
            if (error != null)
            {
                _dialogsHelper.DisplayAlertMessage(error.ErrorMessage);
            }
        }        
    }
}
