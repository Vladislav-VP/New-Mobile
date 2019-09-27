using System.Threading.Tasks;

using TestProject.Entities;
using TestProject.Resources;
using TestProject.Services.DataHandleResults;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Interfaces;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IValidationHelper _validationHelper;

        private readonly IUserRepository _userRepository;

        private readonly IUserStorageHelper _storage;

        private readonly IDialogsHelper _dialogsHelper;

        public RegistrationService(IValidationHelper validationHelper, IDialogsHelper dialogsHelper,
            IUserRepository userRepository, IUserStorageHelper storage)
        {
            _validationHelper = validationHelper;
            _userRepository = userRepository;
            _storage = storage;
            _dialogsHelper = dialogsHelper;
        }

        public async Task RegisterUser(RegistrationResult result)
        {
            bool isUserDataValid = _validationHelper.IsObjectValid(result.Data, nameof(result.Data.Name))
                && _validationHelper.IsObjectValid(result.Data, nameof(result.Data.Password));
            if (!isUserDataValid)
            {
                return;
            }

            result.Data.Name = result.Data.Name.Trim();
            User retrievedUser = await _userRepository.GetUser(result.Data.Name);
            if (retrievedUser != null)
            {
                _dialogsHelper.DisplayAlertMessage(Strings.UserAlreadyExistsMessage);
                return;
            }

            await AddUser(result.Data);
            result.IsSucceded = true;
        }

        private async Task AddUser(User user)
        {
            await _userRepository.Insert(user);
            await _storage.Save(user.Id);
        }
    }
}
