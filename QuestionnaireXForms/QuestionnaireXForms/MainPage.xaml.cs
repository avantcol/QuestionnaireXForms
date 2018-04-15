using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QuestionnaireXForms
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
			
			var toolbarItem = new ToolbarItem
			{
				Text = "Logout"
			};
			toolbarItem.Clicked += OnLogoutButtonClicked;
			ToolbarItems.Add(toolbarItem);

			Title = "Main Page";
			Content = new StackLayout
			{
				Children =
				{
					new Label
					{
						Text = "Main app content goes here",
						HorizontalOptions = LayoutOptions.Center,
						VerticalOptions = LayoutOptions.CenterAndExpand
					}
				}
			};

			async void OnLogoutButtonClicked(object sender, EventArgs e)
			{
				App.IsUserLoggedIn = false;
				Navigation.InsertPageBefore(new LoginPage(), this);
				await Navigation.PopAsync();
			}

		}
	}
}
