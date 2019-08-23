using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using TestProject.Services.Helpers.Interfaces;

namespace TestProject.Services.Helpers
{
    public class ValidationHelper : IValidationHelper
    {

        private List<ValidationResult> _validationErrors;

        public List<ValidationResult> ValidationErrors
        {
            get => _validationErrors;
        }

        public bool IsObjectValid<T>(T obj, string propertyName = null)
        {
            bool isValid;
            var context = new ValidationContext(obj);

            if (!string.IsNullOrEmpty(propertyName))
            {                
                context.MemberName = propertyName;
                Type type = obj.GetType();
                object propertyValue = type
                    .GetProperty(propertyName)
                    .GetValue(obj);

                try
                {
                    Validator.ValidateProperty(propertyValue, context);
                    isValid = true;
                }
                catch (ValidationException)
                {
                    isValid = false;
                }

                return isValid;
            }

            try
            {
                Validator.ValidateObject(obj, context);
                isValid = true;
            }
            catch (ValidationException)
            {
                isValid = false;
            }

            return isValid;
        }

        public ICollection<ValidationResult> ValidateObject<T>(T obj, string propertyName = null)
        {
            var context = new ValidationContext(obj);
            ICollection<ValidationResult> errors = new List<ValidationResult>();

            if (!string.IsNullOrEmpty(propertyName))
            {
                context.MemberName = propertyName;
                Type type = obj.GetType();
                object propertyValue = type
                    .GetProperty(propertyName)
                    .GetValue(obj);
                Validator.TryValidateProperty(propertyValue, context, errors);

                return errors;
            }

            Validator.TryValidateObject(obj, context, errors);

            return errors;
        }

    }
}
