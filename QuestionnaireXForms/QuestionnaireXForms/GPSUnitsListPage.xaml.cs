
using System;
using System.Net.Http;
using System.Threading.Tasks;
using ModernHttpClient;
using Newtonsoft.Json.Linq;
using QuestionnaireXForms.Services;
using Refit;
using Xamarin.Forms;

namespace QuestionnaireXForms
{
    public partial class GPSUnitsListPage : ContentPage
    {
        public GPSUnitsListPage()
        {
            InitializeComponent();
            
            BindingContext = this;
			
            var toolbarItem = new ToolbarItem
            {
                Text = "Exit"
            };
            toolbarItem.Clicked += OnLogoutButtonClicked;
            ToolbarItems.Add(toolbarItem);
            async void OnLogoutButtonClicked(object sender, EventArgs e)
            {
                App.IsUserLoggedIn = false;
                Navigation.InsertPageBefore(new LoginPage(), this);
                await Navigation.PopAsync();

                var closer = DependencyService.Get<ICloseApplication>();
                closer?.Close();
            }

            if (App.IsUserLoggedIn && App.User != null)
            {
                var client = new HttpClient(new NativeMessageHandler())
                {
                    BaseAddress = new Uri(App.BaseUrl)
                };

                UnitListService httpService2 = RestService.For<UnitListService>(client);
                Task<JObject> gpsUnits = httpService2.GetGPSUnits( App.User.id, App.User.quUserSession );
                gpsUnits.Wait();
                System.Console.WriteLine(gpsUnits.Result.ToString());
                GpsUnitsNativeListView.Items = DataSource.GetGPSUnitList(gpsUnits.Result);
            }
        }
        
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //await Navigation.PushModalAsync(new DetailPage(e.SelectedItem, this));
        }
    }
}
