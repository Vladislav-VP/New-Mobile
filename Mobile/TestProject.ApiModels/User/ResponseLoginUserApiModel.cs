namespace TestProject.ApiModels.User
{
    public class ResponseLoginUserApiModel
    {        
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public string Token { get; set; }
    }
}
