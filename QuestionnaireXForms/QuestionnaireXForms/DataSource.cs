using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using QuestionnaireXForms.Domain;
using Xamarin.Forms;

namespace QuestionnaireXForms
{
    public class DataSource
    {
        public static List<Question> GetList( JArray questions )
        {
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
            }
            
            var l = new List<Question>();
            return l;
        }
    }
}