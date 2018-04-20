using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ModernHttpClient;
using Newtonsoft.Json.Linq;
using Org.Json;
using Plugin.Media;
using QuestionnaireXForms.Domain;
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

		public ObservableCollection<PhotoContainer> Images { get; set; }
		
		public MainPage()
		{
			InitializeComponent();

			Images = new ObservableCollection<PhotoContainer>();// {new PhotoContainer() { Title = "initial item" }};

			BindingContext = this;
			
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
				AnswerService.SendAnswers();
			}

			CameraButton.Clicked += CameraButton_Clicked;

			if (App.IsUserLoggedIn && App.User != null)
			{
				var client = new HttpClient(new NativeMessageHandler())
				{
					BaseAddress = new Uri(App.BaseUrl)
				};
				QuestionnaireService httpbinApiService = RestService.For<QuestionnaireService>(client);
				Task<JObject> questions = httpbinApiService.GetQuestions(App.User.id);
				questions.Wait();

				System.Console.WriteLine(questions.Result.ToString());

				nativeListView.Items = DataSource.GetList(questions.Result);


			}

		}

		async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			await Navigation.PushModalAsync(new DetailPage(e.SelectedItem, this));
		}

		async void CameraButton_Clicked(object sender, EventArgs e)
		{
			try
			{

				await CrossMedia.Current.Initialize();

				if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
				{
					await DisplayAlert("No Camera", ":( No camera available.", "OK");
					return;
				}

				var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
				{
					Directory = "Sample",
					Name = "_.jpg"
				});

				if (file != null)
				{
					DataSource.Photos.Add( file );
					//PhotoImage.Source = ImageSource.FromStream(() => file.GetStream());
					Images.Add( new PhotoContainer() { Title = "" , Image = ImageSource.FromStream(() => file.GetStream())} );

				}
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception);
			}
		}
		
	}
}
