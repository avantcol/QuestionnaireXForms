
using System;
using QuestionnaireXForms.Domain;
using Xamarin.Forms;

namespace QuestionnaireXForms
{
    public partial class DetailPage : ContentPage
    {
        public DetailPage( object detail )
        {
            InitializeComponent();
            
            if (detail is string)
            {
                detailLabel.Text = detail as string;
            }
            else if (detail is DataSource)
            {
                detailLabel.Text = (detail as Question).Question_;
            }
        }
        
        async void OnButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
