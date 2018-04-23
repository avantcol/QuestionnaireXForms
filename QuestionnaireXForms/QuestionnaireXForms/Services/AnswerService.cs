

using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using ModernHttpClient;
using Newtonsoft.Json.Linq;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using QuestionnaireXForms.Domain;
using Refit;

using Plugin.Permissions.Abstractions;

namespace QuestionnaireXForms.Services
{
    public static class AnswerService
    {
        private static async Task<Position> ReadGps()
        {
            try
            {
                var hasPermission = await Utils.CheckPermissions(Permission.Location);
                
                if (hasPermission)
                {
                    var locator = CrossGeolocator.Current;
                    locator.DesiredAccuracy =50.0;
                    Position position = await locator.GetPositionAsync(timeout: TimeSpan.FromSeconds(10));
                    //positionTask.Wait();

                    Console.WriteLine("==================================== loc=" + position );

                    //var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(Timeout.Value), null, IncludeHeading.IsToggled);

                    return position;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }
        
        public static async void SendAnswers()
        {
            if (!App.IsUserLoggedIn || App.User == null)
                return;

            ObservableCollection<Question> questions = DataSource.GetQuestions();

            JArray jQuestions = new JArray();
            foreach (var question in questions)
            {
                JObject jObject = new JObject
                {
                    {"id", question.Id_},
                    {"userAnswer", (int) question.UserAnswer}
                };
                if (question.Observation != null)
                {
                    jObject["description"] = question.Observation;
                }
                jQuestions.Add(jObject);
            }

            JObject pollAnswers = new JObject
            {
                {"userId", App.User.id},
                {"questionnaireId", DataSource.QuestionnaireId},
                {"time", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")},
                {"answers", jQuestions}
            };

            System.Console.WriteLine(pollAnswers.ToString());

            try
            {
                Position position = await ReadGps();

                if (position != null)
                {
                    pollAnswers["lat"] = position.Latitude;
                    pollAnswers["lon"] = position.Longitude;
                }

                Console.WriteLine("==================================== loc=" + pollAnswers["lat"] + " " + pollAnswers["lon"]  );

                
                var client = new HttpClient(new NativeMessageHandler())
                {
                    BaseAddress = new Uri(App.BaseUrl)
                };
                IAnswerService answerService = RestService.For<IAnswerService>(client);
                Task<JObject> answersResponce =
                    answerService.SendAnswers(pollAnswers.ToString(), App.User.quUserSession);
                answersResponce.Wait();

                System.Console.WriteLine(answersResponce.Result.ToString());

                System.Console.WriteLine("11111111111111111111111111111");

                System.Console.WriteLine(DataSource.Photos);


                JObject questionnaireAnswers = JObject.FromObject(answersResponce.Result);

                AttachmentService.UploadBitmapAsync(DataSource.Photos, questionnaireAnswers["id"].Value<long>());

                AttachmentService.UploadSignatureAsync(questionnaireAnswers["id"].Value<long>());

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
        Task<JObject> SendAnswers( string answers, string quUserSession );        
    }
}