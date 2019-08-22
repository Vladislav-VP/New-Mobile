using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using TestProject.Services.Helpers.Interfaces;

namespace TestProject.Services.Helpers
{
    public class ValidationHelper : IValidationHelper
    {
        private ValidationContext _context;

        private List<ValidationResult> _validationErrors;

        public List<ValidationResult> ValidationErrors
        {
            get => _validationErrors;
        }

        public bool ObjectIsValid<T>(T obj, string propertyName = null)
        {
            Initialize<T>(obj);
            if (!string.IsNullOrEmpty(propertyName))
            {
                _context.MemberName = propertyName;
                Type type = obj.GetType();
                var propertyValue = type
                    .GetProperty(propertyName)
                    .GetValue(obj);
                return Validator.TryValidateProperty(propertyValue, _context, _validationErrors);
            }

            return Validator.TryValidateObject(obj, _context, _validationErrors);
        }

        private void Initialize<T>(T obj)
        {
            _context = new ValidationContext(obj);
            _validationErrors = new List<ValidationResult>();
        }
    }
}
