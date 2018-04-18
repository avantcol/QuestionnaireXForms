

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
                    {"userAnswer", (int) question.UserAnswer},
                    {"description","test todo"}
                };
                jQuestions.Add( jObject);
            }

            JObject pollAnswers = new JObject
            {
                {"userId", App.User.id}, 
                {"questionnaireId", DataSource.QuestionnaireId},
                {"time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") },
                {"answers", jQuestions}
            };

            System.Console.WriteLine(pollAnswers.ToString());


            try
            {
                var client = new HttpClient(new NativeMessageHandler())
                {
                    BaseAddress = new Uri(App.BaseUrl)
                };
                IAnswerService answerService = RestService.For<IAnswerService>(client);
                Task<JArray> answersResponce = answerService.SendAnswers( pollAnswers.ToString() );
                answersResponce.Wait();

                System.Console.WriteLine(answersResponce.Result.ToString());

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }
        
    }

    public interface IAnswerService
    {
        [Get("/questionnaire/answers")]
        Task<JArray> SendAnswers( string answers );        
    }
}