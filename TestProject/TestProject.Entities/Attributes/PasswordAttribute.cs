using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.RegularExpressions;

using TestProject.Resources;

namespace TestProject.Entities.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PasswordAttribute : ValidationAttribute
    {
        private string _invalidPasswordCharacterPattern;

        private int _minPasswordLength;

        private readonly string _propeprtyName;

        public PasswordAttribute(string propertyName, string invalidPasswordCharacterPattern, int minPasswordLength)
        {
            _propeprtyName = propertyName;
            _invalidPasswordCharacterPattern = invalidPasswordCharacterPattern;
            _minPasswordLength = minPasswordLength;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo property = validationContext.ObjectType.GetProperty(_propeprtyName);

            string propertyValue = property.GetValue(validationContext.ObjectInstance, null).ToString();

            Regex regex = new Regex(_invalidPasswordCharacterPattern);
            if (regex.IsMatch(propertyValue))
            {
                return new ValidationResult(Strings.InvalidPasswordFormatMessage);
            }

            if (propertyValue.Length < _minPasswordLength)
            {
                return new ValidationResult(Strings.TooShortPasswordMessage);
            }

            return ValidationResult.Success;
        }

    }
}