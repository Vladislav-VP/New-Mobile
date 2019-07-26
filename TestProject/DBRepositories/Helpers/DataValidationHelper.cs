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
        public bool UserInfoIsValid(User user, out List<ValidationResult> validationResults)
        {
            ValidationContext context = new ValidationContext(user);
            validationResults = new List<ValidationResult>();

            context.MemberName = nameof(user.Name);
            var userNameIsValid = Validator.TryValidateProperty(user.Name, context, validationResults);
            context.MemberName = nameof(user.Password);
            var passwordIsValid = Validator.TryValidateProperty(user.Password, context, validationResults);

            return userNameIsValid && passwordIsValid;
        }

        public bool TodoItemIsValid(TodoItem todoItem, out List<ValidationResult> validationResults)
        {
            ValidationContext context = new ValidationContext(todoItem);
            validationResults = new List<ValidationResult>();

            return Validator.TryValidateObject(todoItem, context, validationResults);
        }
    }
}
