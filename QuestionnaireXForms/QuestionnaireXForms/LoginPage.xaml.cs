
using System;
using System.Net.Http;
using ModernHttpClient;
using QuestionnaireXForms.Services;
using Refit;
using Xamarin.Forms;

namespace QuestionnaireXForms
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage ()
        {
            InitializeComponent ();
        }
       
        /*
        async void OnSignUpButtonClicked (object sender, EventArgs e)
        {
            await Navigation.PushAsync (new SignUpPage ());
        }
        */

        async void OnLoginButtonClicked (object sender, EventArgs e)
        {
            var user = new User {
                Username = usernameEntry.Text,
                Password = passwordEntry.Text
            };

            System.Console.WriteLine( ">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>" );

            try
            {
                string baseUrl = "http://10.0.0.23:8080/gpserver"; 
                var client = new HttpClient(new NativeMessageHandler()) 
                { 
                    BaseAddress = new Uri(baseUrl) 
                };
                LoginService httpbinApiService = RestService.For<LoginService>(client);
                User resUser = await httpbinApiService.Login(  new LoginRequestForm(user.Username, user.Password) );
            
                System.Console.WriteLine( resUser.id );

                if (resUser != null && resUser.id != 0)
                {
                    App.IsUserLoggedIn = true;
                    Navigation.InsertPageBefore (new MainPage (), this);
                    await Navigation.PopAsync ();
                }
                else
                {
                    App.IsUserLoggedIn = false;
                    messageLabel.Text = "Login failed";
                    passwordEntry.Text = string.Empty;
                }
                
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                //throw;
            }
                
        }

        bool AreCredentialsCorrect (User user)
        {
            return true;//user.Username == Constants.Username && user.Password == Constants.Password;
        }
    }
}
