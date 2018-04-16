using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ModernHttpClient;
using Newtonsoft.Json.Linq;
using QuestionnaireXForms.Services;
using Refit;
using Xamarin.Forms;

namespace QuestionnaireXForms
{
	public partial class MainPage : ContentPage
	{
		public NativeListView GetListView()
		{
			return nativeListView;
		}

		public MainPage() 
		{
			InitializeComponent();
			
			var toolbarItem = new ToolbarItem
			{
				Text = "Logout"
			};
			toolbarItem.Clicked += OnLogoutButtonClicked;
			ToolbarItems.Add(toolbarItem);

			Title = "Questionnaire";
			
			async void OnLogoutButtonClicked(object sender, EventArgs e)
			{
				App.IsUserLoggedIn = false;
				Navigation.InsertPageBefore(new LoginPage(), this);
				await Navigation.PopAsync();
			}

			if (App.IsUserLoggedIn && App.User != null)
			{
				var client = new HttpClient(new NativeMessageHandler()) 
				{ 
					BaseAddress = new Uri(App.BaseUrl) 
				};
				PollService httpbinApiService = RestService.For<PollService>(client);
				Task<JArray> questions = httpbinApiService.GetQuestions( App.User.id );
				questions.Wait();
				
				System.Console.WriteLine( questions.Result.ToString() );
				
				nativeListView.Items = DataSource.GetList ( questions.Result );
			}

		}
		
		async void OnItemSelected (object sender, SelectedItemChangedEventArgs e)
		{
			await Navigation.PushModalAsync (new DetailPage (e.SelectedItem, this));
		}
	}
}
