﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace QuestionnaireXForms
{
	public partial class App : Application
	{
		public static bool IsUserLoggedIn { get; set; }

		public App ()
		{
			InitializeComponent();

			//MainPage = new QuestionnaireXForms.MainPage();
			//MainPage = new QuestionnaireXForms.MainPageCS();
			
			
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