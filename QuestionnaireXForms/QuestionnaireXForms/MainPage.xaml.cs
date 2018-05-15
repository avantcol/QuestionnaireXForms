
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using ModernHttpClient;
using Newtonsoft.Json.Linq;
using Plugin.Media;
using Plugin.Permissions.Abstractions;
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
			return NativeListView;
		}

		public ObservableCollection<PhotoContainer> Images { get; set; }
		
		public MainPage()
		{
			InitializeComponent();

			Images = new ObservableCollection<PhotoContainer>();// {new PhotoContainer() { Title = "initial item" }};

			BindingContext = this;
			
			var toolbarItem = new ToolbarItem
			{
				Text = "Exit"
			};
			toolbarItem.Clicked += OnLogoutButtonClicked;
			ToolbarItems.Add(toolbarItem);


			var submitItem = new ToolbarItem
			{
				Text = "Submit"
			};
			submitItem.Clicked += OnSubmitButtonClicked;
			ToolbarItems.Add(submitItem);


			var signatureMenuItem = new ToolbarItem
			{
				Text = "Sign"
			};
			signatureMenuItem.Clicked += OnSignButtonClicked;
			ToolbarItems.Add(signatureMenuItem);
			
			Title = "Questionnaire";

			async void OnLogoutButtonClicked(object sender, EventArgs e)
			{
				App.IsUserLoggedIn = false;
				Navigation.InsertPageBefore(new LoginPage(), this);
				await Navigation.PopAsync();

				var closer = DependencyService.Get<ICloseApplication>();
				closer?.Close();
			}

			async void OnSignButtonClicked(object sender, EventArgs e)
			{
				await Navigation.PushModalAsync(new SignaturePage());
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

				QuestionnaireService httpService1 = RestService.For<QuestionnaireService>(client);
				Task<JObject> questions = httpService1.GetQuestions( App.User.id, App.User.quUserSession );
				questions.Wait();
				System.Console.WriteLine(questions.Result.ToString());
				NativeListView.Items = DataSource.GetList(questions.Result);

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
				await Utils.CheckPermissions(Permission.Camera);

				/*
				var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
				if (status != PermissionStatus.Granted)
				{
					if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Camera))
					{
						await DisplayAlert("Camera Permission", "Allow SavR to access your camera", "OK");
					}

					var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera });
					status = results[Permission.Camera];
				}
				*/
				
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
