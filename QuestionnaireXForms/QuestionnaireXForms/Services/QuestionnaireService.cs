using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Refit;

namespace QuestionnaireXForms.Services
{
    public interface QuestionnaireService
    {
        [Get("/questionnaire/polls")]
        Task<JObject> GetQuestions( long userId, string quUserSession );
    }
}