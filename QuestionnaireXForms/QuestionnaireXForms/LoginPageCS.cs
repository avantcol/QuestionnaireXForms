using System;
using System.Net.Http;
using ModernHttpClient;
using QuestionnaireXForms.Services;
using Refit;
using Xamarin.Forms;

namespace QuestionnaireXForms
{
    public class LoginPageCS : ContentPage
    {
        Entry usernameEntry, passwordEntry;
        Label messageLabel;

        public LoginPageCS ()
        {
            //var toolbarItem = new ToolbarItem {
            //	Text = "Sign Up"
            //};
            //toolbarItem.Clicked += OnSignUpButtonClicked;
            //ToolbarItems.Add (toolbarItem);

            messageLabel = new Label ();
            usernameEntry = new Entry {
                Placeholder = "username"	
            };
            passwordEntry = new Entry {
                IsPassword = true
            };
            var loginButton = new Button {
                Text = "Login"
            };
            loginButton.Clicked += OnLoginButtonClicked;

            Title = "Login";
            Content = new StackLayout { 
                VerticalOptions = LayoutOptions.StartAndExpand,
                Children = {
                    new Label { Text = "Username" },
                    usernameEntry,
                    new Label { Text = "Password" },
                    passwordEntry,
                    loginButton,
                    messageLabel
                }
            };
        }

        /*
        async void OnSignUpButtonClicked (object sender, EventArgs e)
        {
            await Navigation.PushAsync (new SignUpPageCS ());
        }
        */

        async void OnLoginButtonClicked (object sender, EventArgs e)
        {
            var user = new User {
                Username = usernameEntry.Text,
                Password = passwordEntry.Text
            };

            /*
            var isValid = AreCredentialsCorrect (user);
            if (isValid) {
                App.IsUserLoggedIn = true;
                Navigation.InsertPageBefore (new MainPageCS (), this);
                await Navigation.PopAsync ();
            } else {
                messageLabel.Text = "Login failed";
                passwordEntry.Text = string.Empty;
            }
            */

            /*
            System.Console.WriteLine( ">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>" );

            
            var client = new HttpClient(new NativeMessageHandler()) 
            { 
                BaseAddress = new Uri("http://localhost:8080/gpserver/") 
            }; 
            LoginService httpbinApiService = RestService.For<LoginService>(client);
            User resUser = await httpbinApiService.Login(user.Username, user.Password);
            
            System.Console.WriteLine( user.id );
            */
        }

        bool AreCredentialsCorrect (User user)
        {
            return user.Username == Constants.Username && user.Password == Constants.Password;
        }

    }
}