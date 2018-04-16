using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json.Linq;
using QuestionnaireXForms.Domain;
using Xamarin.Forms;

namespace QuestionnaireXForms
{
    public class DataSource
    {
        private static ObservableCollection<Question> _questions = new ObservableCollection<Question>();

        public static ObservableCollection<Question> GetQuestions()
        {
            return _questions;
        }

        public static ObservableCollection<Question> GetList( JArray questions )
        {
            var l = new ObservableCollection<Question>();
            foreach (var jQuestion in questions )
            {
                JArray jAnswerTypes = jQuestion["questions"].Value<JArray>();
                var question = new Question
                {
                    Id_ = jQuestion["id"].Value<long>(),
                    Question_ = jQuestion["question"].Value<string>(),
                    AnswerTypes_ = new Question.AnswerType[jAnswerTypes.Count]
                };
                int i = 0;
                foreach ( var jAnswerType in jAnswerTypes )
                {
                    question.AnswerTypes_[i] = Question.FromJSON(jAnswerType);
                    ++i;
                }
                
                l.Add( question );
            }

            _questions = l;
            
            return l;
        }
    }
}