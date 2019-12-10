using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using TestProject.Services.Helpers.Interfaces;

namespace TestProject.Services.Helpers
{
    public class ValidationHelper : IValidationHelper
    {
        private readonly IDialogsHelper _dialogsHelper;

        public ValidationHelper(IDialogsHelper dialogsHelper)
        {
            _dialogsHelper = dialogsHelper;
        }

        public bool IsObjectValid<T>(T obj, bool validateAllProperties = true)
        {
            var context = new ValidationContext(obj);
            ICollection<ValidationResult> errors = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(obj, context, errors, validateAllProperties);

            if (!isValid)
            {
                HandleValidationResult(errors);
            }            

            return isValid;
        }

        private void HandleValidationResult(ICollection<ValidationResult> errors)
        {
            ValidationResult error = errors.FirstOrDefault();
            if (error != null)
            {
                _dialogsHelper.DisplayAlertMessage(error.ErrorMessage);
            }
        }
    }
}
