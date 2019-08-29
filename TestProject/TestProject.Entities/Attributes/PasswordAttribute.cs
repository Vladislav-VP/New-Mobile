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
        private string _passwordCharacterPattern;

        private int _minPasswordLength;

        private readonly string _propeprtyName;

        public PasswordAttribute(string propertyName, string passwordCharacterPattern, int minPasswordLength)
        {
            _propeprtyName = propertyName;
            _passwordCharacterPattern = passwordCharacterPattern;
            _minPasswordLength = minPasswordLength;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo property = validationContext.ObjectType.GetProperty(_propeprtyName);

            string propertyValue = property.GetValue(validationContext.ObjectInstance, null).ToString();

            Regex regex = new Regex(_passwordCharacterPattern);
            MatchCollection matches = regex.Matches(propertyValue);
            if (matches.Count != propertyValue.Length)
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