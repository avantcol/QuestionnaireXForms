using System.Threading.Tasks;
using Refit;

namespace QuestionnaireXForms.Services
{
    public interface LoginService
    {
        [Post("/questionnaire/login")]
        Task<User> Login( [Body] LoginRequestForm user);
    }
}
