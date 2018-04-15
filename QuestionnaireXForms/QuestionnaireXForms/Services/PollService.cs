using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Refit;

namespace QuestionnaireXForms.Services
{
    public interface PollService
    {
        [Get("/questionnaire/polls")]
        Task<JArray> GetQuestions( long userId );
    }
}