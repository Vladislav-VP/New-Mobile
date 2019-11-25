namespace TestProject.ApiModels.User
{
    public class RequestChangePasswordUserApiModel
    {
        public int Id { get; set; }

        public string OldPassword { get; set; }

        public string OldPasswordConfirmation { get; set; }

        public string NewPassword { get; set; }

        public string NewPasswordConfirmation { get; set; }
    }
}
