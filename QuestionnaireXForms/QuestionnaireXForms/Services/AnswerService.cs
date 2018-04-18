

using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using ModernHttpClient;
using Newtonsoft.Json.Linq;
using QuestionnaireXForms.Domain;
using Refit;

namespace QuestionnaireXForms.Services
{
    public static class AnswerService
    {
        public static void SendAnswers()
        {
            if ( ! App.IsUserLoggedIn || App.User == null )
                return;
            
            ObservableCollection<Question> questions = DataSource.GetQuestions();
            
            JArray jQuestions = new JArray();
            foreach (var question in questions )
            {
                JObject jObject = new JObject
                {
                    {"id", question.Id_}, 
                    {"userAnswer", (int) question.UserAnswer}
                };
                jQuestions.Add( jObject);
            }

            JObject pollAnswers = new JObject
            {
                {"userId", App.User.id}, 
                {"answers", jQuestions}
            };

            System.Console.WriteLine(pollAnswers.ToString());
            
            
            var client = new HttpClient(new NativeMessageHandler())
            {
                BaseAddress = new Uri(App.BaseUrl)
            };
            IAnswerService answerService = RestService.For<IAnswerService>(client);
            Task<JArray> answersResponce = answerService.SendAnswers( pollAnswers.ToString() );
            answersResponce.Wait();

            System.Console.WriteLine(answersResponce.Result.ToString());

        }
        
    }

    public interface IAnswerService
    {
        [Get("/questionnaire/answers")]
        Task<JArray> SendAnswers( string answers );        
    }
}