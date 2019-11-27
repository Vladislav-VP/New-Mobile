using DataAccess.Repositories;
using Entities;
using ViewModels.UI;

namespace Services.UI
{
    public class UsersService
    {
        private readonly UserRepository _userRepository;

        public UsersService()
        {
            _userRepository = new UserRepository();
        }

        public ResponseLoginUserView Login(RequestLoginUserView user)
        {
            var response = new ResponseLoginUserView();
            User retrievedUser = _userRepository.Find(user.Name, user.Password);
            if (retrievedUser == null)
            {
                response.Message = "Incorrect username or password";
                return response;
            }
            user.Id = retrievedUser.Id;
            response.IsSuccess = true;
            return response;
        }

        public HomeInfoUserView GetUserHomeInfo(int id)
        {
            User retrievedUser = _userRepository.Find(id);
            if (retrievedUser == null)
            {
                return null;
            }
            var user = new HomeInfoUserView()
            {
                Id = id,
                Name = retrievedUser.Name,
                ImageBytes = retrievedUser.ImageBytes,
                ImageUrl = retrievedUser.ImageUrl
            };
            return user;
        }
    }
}
