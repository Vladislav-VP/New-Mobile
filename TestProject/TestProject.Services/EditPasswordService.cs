using System.Threading.Tasks;

using TestProject.Services.DataHandleResults;
using TestProject.Services.Helpers.Interfaces;
using TestProject.Services.Interfaces;
using TestProject.Services.Repositories.Interfaces;

namespace TestProject.Services
{
    public class EditPasswordService : IEditPasswordService
    {
        private readonly IUserRepository _userRepository;

        private readonly IValidationHelper _validationHelper;

        public EditPasswordService(IUserRepository userRepository, IValidationHelper validationHelper)
        {
            _userRepository = userRepository;
            _validationHelper = validationHelper;
        }

        public async Task ChangePassword(EditPasswordResult result)
        {
            result.IsSucceded = _validationHelper.IsObjectValid(result, nameof(result.OldPasswordConfirmation))
                && _validationHelper.IsObjectValid(result, nameof(result.NewPassword))
                && _validationHelper.IsObjectValid(result, nameof(result.NewPasswordConfirmation));

            if (result.IsSucceded)
            {
                result.Data.Password = result.NewPassword;
                await _userRepository.Update(result.Data);
            }
        }
    }
}
