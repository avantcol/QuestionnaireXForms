using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuestionnaireXForms.Services;
using Xamarin.Forms;

namespace QuestionnaireXForms
{
	public partial class App : Application
	{
		public static bool IsUserLoggedIn { get; set; }

		public static readonly string BaseUrl = "http://10.0.0.23:8080/gpserver";

		public static string SessionID { get; set; }
		
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
