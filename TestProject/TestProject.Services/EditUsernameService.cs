using System.Threading.Tasks;

using TestProject.Entities;
using TestProject.Resources;
using TestProject.Services.DataHandleResults;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Interfaces;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Services
{
    public class EditUsernameService : IEditUsernameService
    {
        private readonly IValidationHelper _validationHelper;

        private readonly IUserRepository _userRepository;

        private readonly IDialogsHelper _dialogsHelper;

        public EditUsernameService(IValidationHelper validationHelper, 
            IUserRepository userRepository, IDialogsHelper dialogsHelper)
        {
            _validationHelper = validationHelper;
            _userRepository = userRepository;
            _dialogsHelper = dialogsHelper;
        }

        public async Task EditUsername(EditUsernameResult result)
        {
            string userName = result.Data.Name.Trim();

            var userToCheck = new User
            {
                Name = userName,
                Password = result.Data.Password
            };

            bool isUserNameValid = _validationHelper.IsObjectValid(userToCheck);
            if (!isUserNameValid)
            {
                return;
            }

            User retrievedUser = await _userRepository.GetUser(userName);
            if (retrievedUser != null && retrievedUser.Id != result.Data.Id)
            {
                _dialogsHelper.DisplayAlertMessage(Strings.UserAlreadyExistsMessage);
                return;
            }

            result.Data.Name = userName;
            await _userRepository.Update(result.Data);
            result.IsSucceded = true;
        }
    }
}
