using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;
using TestProject.Resources;

namespace TestProject.Entities.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PasswordAttribute : ValidationAttribute
    {
        private const string InvalidPasswordCharacterPattern = @"\W";

        private const int MinPasswordLength = 6;

        private readonly string _propeprtyName;

        public PasswordAttribute(string propertyName)
        {
            _propeprtyName = propertyName;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(_propeprtyName);

            var propertyValue = property.GetValue(validationContext.ObjectInstance, null).ToString();

            Regex regex = new Regex(InvalidPasswordCharacterPattern);
            if (regex.IsMatch(propertyValue))
            {
                return new ValidationResult(Strings.InvalidPasswordFormatMessage);
            }

            if (propertyValue.Length < MinPasswordLength)
            {
                return new ValidationResult(Strings.TooShortPasswordMessage);
            }

            return ValidationResult.Success;
        }

    }
}