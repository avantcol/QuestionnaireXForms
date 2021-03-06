﻿
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Newtonsoft.Json.Linq;
using Plugin.Media.Abstractions;
using QuestionnaireXForms.Domain;

namespace QuestionnaireXForms
{
    public static class DataSource
    {
        public static GPSUnit SelectedUnit { get; set; } = null;

        private static ObservableCollection<Question> _questions = new ObservableCollection<Question>();
        public static ObservableCollection<Question> GetQuestions()
        {
            return _questions;
        }


        private static ObservableCollection<GPSUnit> _gpsUnits = new ObservableCollection<GPSUnit>();
        public static ObservableCollection<GPSUnit> GetGPSUnits()
        {
            return _gpsUnits;
        }
        
        public static long QuestionnaireId { get; set; }
        public static string Description { get; set; }
        public static string Name { get; set; }

        //public static Image SignatureImage { get; set; }
        
        public static Stream SignatureStream { get; set; }
        
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

        public static ObservableCollection<GPSUnit> GetGPSUnitList(JObject jsUnits)
        {
            _gpsUnits = new ObservableCollection<GPSUnit>();
            JArray jUnits = jsUnits["units"].Value<JArray>();
            foreach (var ju in jUnits )
            {
                GPSUnit unit = new GPSUnit
                {
                    Id = ju["id"].Value<long>(),
                    Name = ju["name"]?.Value<string>(),
                    Plate = ju["plate"]?.Value<string>()
                };
                
                _gpsUnits.Add( unit );
            }

            return _gpsUnits;
        }
    }
}