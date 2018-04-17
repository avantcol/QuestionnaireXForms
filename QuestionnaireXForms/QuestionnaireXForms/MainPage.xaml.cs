using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ModernHttpClient;
using Newtonsoft.Json.Linq;
using Plugin.Media;
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


			var submitItem = new ToolbarItem
			{
				Text = "Submit"
			};
			submitItem.Clicked += OnSubmitButtonClicked;
			ToolbarItems.Add(submitItem);
			
			Title = "Questionnaire";
			
			async void OnLogoutButtonClicked(object sender, EventArgs e)
			{
				App.IsUserLoggedIn = false;
				Navigation.InsertPageBefore(new LoginPage(), this);
				await Navigation.PopAsync();
			}

			async void OnSubmitButtonClicked(object sender, EventArgs e)
			{
				// todo
			}

			CameraButton.Clicked += CameraButton_Clicked;

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
		
		async void CameraButton_Clicked(object sender, EventArgs e)
		{
			System.Console.WriteLine("CameraButton_Clicked");
			
			await CrossMedia.Current.Initialize();
			
			if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
			{
				await DisplayAlert("No Camera", ":( No camera available.", "OK");
				return;
			}
			

			await DisplayAlert(">>>>>>>>>>>>>",">>>>>>>>>>",">>>>>>>");
			
			var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
			{
				Directory = "Sample",
				Name = "test.jpg"
			});

			if (file == null)
				return;
			
			await DisplayAlert("File Location", file.Path, "OK");
			
			//var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });

			//if (photo != null)
			//	PhotoImage.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
		}
	}
}
