
using System;
using System.Collections.Generic;
using QuestionnaireXForms.Domain;
using Xamarin.Forms;

namespace QuestionnaireXForms
{
    public partial class DetailPage : ContentPage
    {
        private Dictionary<int, Question.AnswerType> _answerMap = new Dictionary<int,Question.AnswerType>();
            
        public DetailPage( object detail )
        {
            if ( ! (detail is Question ) )
                return;
            
            Question question = detail as Question;
            
            InitializeComponent();
            
            detailLabel.Text = question.Question_;
            
            
            Picker picker = new Picker
            {
                Title = "Color",
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
            };

            stackLayout.Children.Add( picker );
        }
        
        async void OnButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
