
using System.Collections.Generic;
using System.Collections.ObjectModel;
using QuestionnaireXForms.Domain;
using Xamarin.Forms;

namespace QuestionnaireXForms
{
    public partial class DetailPage : ContentPage
    {
        private readonly Dictionary<int, Question.AnswerType> _answerMap = new Dictionary<int,Question.AnswerType>();

        private readonly Entry _textEntry;
        
        public DetailPage( object detail, MainPage parentPage )
        {
            if ( ! (detail is Question ) )
                return;

            var parentPage1 = parentPage;
            
            Question question = detail as Question;
            
            InitializeComponent();
            
            DetailLabel.Text = question.Question_;
            
            
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

                question.Observation = _textEntry.Text;
                
                Navigation.PopModalAsync();

                parentPage1.GetListView().Items = new ObservableCollection<Question>(); 
                parentPage1.GetListView().Items = DataSource.GetQuestions();

            };

            StackLayout.Children.Add( picker );

            _textEntry = new Entry(){HorizontalOptions = LayoutOptions.Fill};
            StackLayout.Children.Add( _textEntry );

        }
        
        /*
        async void OnButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
        */
    }
}
