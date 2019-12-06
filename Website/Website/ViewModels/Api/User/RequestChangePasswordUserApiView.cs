namespace ViewModels.Api.User
{
    public class RequestChangePasswordUserApiView
    {
        public string Id { get; set; }

        public string OldPassword { get; set; }

        public string OldPasswordConfirmation { get; set; }

        public string NewPassword { get; set; }

        public string NewPasswordConfirmation { get; set; }
    }
}
