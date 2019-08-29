using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using MvvmCross;

using TestProject.Services.Helpers.Interfaces;

namespace TestProject.Services.Helpers
{
    public class ValidationHelper : IValidationHelper
    {
        private readonly IDialogsHelper _dialogsHelper;

        public ValidationHelper()
        {
            _dialogsHelper = Mvx.IoCProvider.Resolve<IDialogsHelper>();
        }

        public bool IsObjectValid<T>(T obj, string propertyName = null)
        {
            bool isValid = false;
            var context = new ValidationContext(obj);
            ICollection<ValidationResult> errors = new List<ValidationResult>();

            if (!string.IsNullOrEmpty(propertyName))
            {
                context.MemberName = propertyName;
                Type type = obj.GetType();
                object propertyValue = type
                    .GetProperty(propertyName)
                    .GetValue(obj);
                isValid = Validator.TryValidateProperty(propertyValue, context, errors);
            }

            if (string.IsNullOrEmpty(propertyName))
            {
                isValid = Validator.TryValidateObject(obj, context, errors);
            }            

            HandleValidationResult(errors);
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
