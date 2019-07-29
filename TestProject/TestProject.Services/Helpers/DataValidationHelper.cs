using System;
using System.Collections.Generic;
using System.Text;
using TestProject.Entities;
using Acr.UserDialogs;
using TestProject.Services.Resources;
using System.Threading.Tasks;
using TestProject.Configurations;
using System.Text.RegularExpressions;
using TestProject.Services.Repositories;
using System.ComponentModel.DataAnnotations;

namespace TestProject.Services.Helpers
{
    public class DataValidationHelper
    {
        private ValidationContext _context;

        private List<ValidationResult> _validationErrors;

        public List<ValidationResult> ValidationErrors
        {
            get => _validationErrors;
        }

        public bool UserNameIsValid(User user)
        {
            Initialize(user, nameof(user.Name));

            return Validator.TryValidateProperty(user.Name, _context, _validationErrors);
        }

        public bool PasswordIsValid(User user)
        {
            Initialize(user, nameof(user.Password));

            return Validator.TryValidateProperty(user.Password, _context, _validationErrors);
        }

        public bool TodoItemIsValid(TodoItem todoItem)
        {
            Initialize(todoItem);

            return Validator.TryValidateObject(todoItem, _context, _validationErrors);
        }

        private void Initialize(object objToValidate, string propertyName = null)
        {
            _context = new ValidationContext(objToValidate);
            if (!string.IsNullOrEmpty(propertyName))
            {
                _context.MemberName = propertyName;
            }

            _validationErrors = new List<ValidationResult>();
        }
    }
}
