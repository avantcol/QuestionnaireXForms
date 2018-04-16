
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Android;
using Android.Widget;
using QuestionnaireXForms.Domain;
using Xamarin.Forms;

namespace QuestionnaireXForms
{
    public partial class DetailPage : ContentPage
    {
        private Dictionary<int, Question.AnswerType> _answerMap = new Dictionary<int,Question.AnswerType>();

        private MainPage _parentPage;
        
        public DetailPage( object detail, MainPage parentPage )
        {
            if ( ! (detail is Question ) )
                return;

            _parentPage = parentPage;
            
            Question question = detail as Question;
            
            InitializeComponent();
            
            detailLabel.Text = question.Question_;
            
            
            Picker picker = new Picker
            {
                Title = question.UserAnserAsString,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            int i = 0;
            foreach ( var answerType in question.AnswerTypes_ )
            {
                picker.Items.Add( answerType.ToString() );
                _answerMap[i] = answerType;
                ++i;
            }

            picker.SelectedIndexChanged += (sender, args) =>
            {
                if (picker.SelectedIndex == -1)
                {
                    question.UserAnswer = Question.AnswerType.NotApply;
                }
                else
                {
                    question.UserAnswer = _answerMap[ picker.SelectedIndex ];
                }
                
                Navigation.PopModalAsync();

                //_parentPage.GetListView().Items = null;
                _parentPage.GetListView().Items = new ObservableCollection<Question>(); 
                _parentPage.GetListView().Items = DataSource.GetQuestions();

                //this.FindByName<NativeListView> ("nativeListView").NotifyItemSelected( question);// Text = question.UserAnserAsString;

                /*
                try
                {
                    _parentPage.GetListView().NotifyItemSelected( sender );
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                */

            };

            stackLayout.Children.Add( picker );

        }
        
        /*
        async void OnButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        */
    }
}
