using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;

namespace QuestionnaireXForms.Domain
{
    public class Question : INotifyPropertyChanged
    {
        public enum AnswerType
        {
            NotApply=1, Ok=2, Fail=3, NoAnswer
        };

        public static AnswerType FromJSON(JToken jt)
        {
            switch ( jt["id"].Value<int>() )
            {
                case 1: return AnswerType.NotApply;
                case 2: return AnswerType.Ok;
                case 3: return AnswerType.Fail;
            }

            return AnswerType.NoAnswer;
        }

        public long Id_ { get; set; }
        public string Question_ { get; set; }
        public AnswerType[] AnswerTypes_ { get; set; }

        private AnswerType _userAnswer;
        public AnswerType UserAnswer
        {
            get => _userAnswer;
            set
            {
                _userAnswer = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, 
                        new PropertyChangedEventArgs("UserAnswer"));// Throw!!
                }
            }
        }

        public string UserAnserAsString
        {
            get
            {
                if (UserAnswer == AnswerType.NoAnswer)
                    return "";
                return UserAnswer.ToString();
            }
        }
        
        /*
        protected bool ChangeAndNotify<T>(ref T property, T value, [CallerMemberName] string propertyName = "")
        {
            if (!EqualityComparer<T>.Default.Equals(property, value))
            {
                property = value;
                NotifyPropertyChanged(propertyName);
                return true;
            }


            return false;
        }
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        */
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        
    }
}