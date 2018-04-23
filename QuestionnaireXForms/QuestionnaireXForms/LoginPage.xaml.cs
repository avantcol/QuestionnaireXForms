
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

            Console.WriteLine( ">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>" );

            try
            {
                var client = new HttpClient(new NativeMessageHandler()) 
                { 
                    BaseAddress = new Uri(App.BaseUrl) 
                };
                LoginService httpbinApiService = RestService.For<LoginService>(client);
                User resUser = await httpbinApiService.Login( new LoginRequestForm(user.Username, user.Password) );
            
                Console.WriteLine( resUser.id );

                if (resUser.id != 0)
                {
                    App.SessionId = resUser.quUserSession;
                    App.IsUserLoggedIn = true;
                    App.User = resUser;
                    Navigation.InsertPageBefore (new MainPage (), this);
                    await Navigation.PopAsync ();
                }
                else
                {
                    App.IsUserLoggedIn = false;
                    messageLabel.Text = "Login failed";
                    passwordEntry.Text = string.Empty;
                    App.User = null;
                    App.SessionId = null;
                }
                
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                //throw;
            }
                
        }
    }
}
