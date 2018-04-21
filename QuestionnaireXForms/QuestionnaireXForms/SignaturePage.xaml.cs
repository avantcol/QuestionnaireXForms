// $Id:$

using System;
using SignaturePad.Forms;
using Xamarin.Forms;

namespace QuestionnaireXForms
{
    public partial class SignaturePage : ContentPage
    {
        public SignaturePage()
        {
            InitializeComponent();
        }
        
        private async void OnChangeTheme (object sender, EventArgs e)
        {
            var action = await DisplayActionSheet ("Change Theme", "Cancel", null, "White", "Black", "Aqua");
            switch (action)
            {
                case "White":
                    SignatureView.BackgroundColor = Color.White;
                    SignatureView.StrokeColor = Color.Black;
                    SignatureView.ClearTextColor = Color.Black;
                    SignatureView.ClearText = "Clear Markers";
                    break;

                case "Black":
                    SignatureView.BackgroundColor = Color.Black;
                    SignatureView.StrokeColor = Color.White;
                    SignatureView.ClearTextColor = Color.White;
                    SignatureView.ClearText = "Clear Chalk";
                    break;

                case "Aqua":
                    SignatureView.BackgroundColor = Color.Aqua;
                    SignatureView.StrokeColor = Color.Red;
                    SignatureView.ClearTextColor = Color.Black;
                    SignatureView.ClearText = "Clear The Aqua";
                    break;
            }
        }

        private async void OnGetImage (object sender, EventArgs e)
        {
            var settings = new ImageConstructionSettings
            {
                Padding = 12,
                StrokeColor = Color.FromRgb (25, 25, 25),
                BackgroundColor = Color.FromRgb (225, 225, 225),
                DesiredSizeOrScale = 2f
            };
            var image = await SignatureView.GetImageStreamAsync (SignatureImageFormat.Png, settings);

            if (image != null)
            {
                /*
                DataSource.SignatureImage = new Image
                {
                    Aspect = Aspect.AspectFit,
                    Source = ImageSource.FromStream(() => image)
                };
                */
                DataSource.SignatureStream = image;
            }

            await Navigation.PopModalAsync();
        }

        private async void DismissClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
