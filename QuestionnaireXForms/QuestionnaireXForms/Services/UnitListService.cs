
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using QuestionnaireXForms.Domain;
using Refit;

namespace QuestionnaireXForms.Services
{
    public interface UnitListService
    {
        [Get("/questionnaire/list")]
        Task<JObject> GetGPSUnits( long userId, string quUserSession );
    }
}