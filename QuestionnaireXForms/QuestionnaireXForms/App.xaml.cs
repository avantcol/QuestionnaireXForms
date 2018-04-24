
using Plugin.CurrentActivity;
using QuestionnaireXForms.Services;
using Xamarin.Forms;

namespace QuestionnaireXForms
{
	public partial class App : Application
	{
		public static bool IsUserLoggedIn { get; set; }

		//public const string BaseUrl = "http://10.0.0.23:8080/gpserver";
		public const string BaseUrl = "https://coltrack.com";

		public static string SessionId { get; set; }
		
		public static User User { get; set; }
		
		public static string Description { get; set; }

		public App ()
		{
			InitializeComponent();

			if (!IsUserLoggedIn) {
				MainPage = new NavigationPage (new LoginPage ());
			} else {
				MainPage = new NavigationPage (new MainPage());
			}
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
		
		
	}
}
