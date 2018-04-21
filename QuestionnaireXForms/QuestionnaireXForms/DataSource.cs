using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json.Linq;
using Plugin.Media.Abstractions;
using QuestionnaireXForms.Domain;
using Xamarin.Forms;

namespace QuestionnaireXForms
{
    public static class DataSource
    {
        private static ObservableCollection<Question> _questions = new ObservableCollection<Question>();

        public static ObservableCollection<Question> GetQuestions()
        {
            return _questions;
        }

        public static long QuestionnaireId { get; set; }
        public static string Description { get; set; }
        public static string Name { get; set; }

        public static Image SignatureImage { get; set; }
        
        public static List<MediaFile> Photos { get; set; } = new List<MediaFile>();


        public static ObservableCollection<Question> GetList( JObject questionnaire )
        {
            QuestionnaireId = questionnaire["id"].Value<long>();
            Description = questionnaire["description"].Value<string>();
            Name = questionnaire["name"].Value<string>();
            
            JArray jQuestions = questionnaire["questions"].Value<JArray>();
            
            var l = new ObservableCollection<Question>();
            foreach (var jQuestion in jQuestions )
            {
                JArray jAnswerTypes = jQuestion["answerTypes"].Value<JArray>();
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