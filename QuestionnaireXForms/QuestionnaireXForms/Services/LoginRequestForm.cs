namespace QuestionnaireXForms.Services
{
    public class LoginRequestForm
    {
        public LoginRequestForm(string user, string password)
        {
            this.user = user;
            this.password = password;
        }

        public string user { get; set; }
        public string password { get; set; }
    }
}