using Newtonsoft.Json.Linq;

namespace QuestionnaireXForms.Domain
{
    public class Question
    {
        public enum AnswerType
        {
            NotApply=1, Ok=2, Fail=3
        };

        public static AnswerType FromJSON(JToken jt)
        {
            switch ( jt["id"].Value<int>() )
            {
                case 1: return AnswerType.NotApply;
                case 2: return AnswerType.Ok;
                case 3: return AnswerType.Fail;
            }

            return AnswerType.NotApply;
        }
        
        public long Id_ { get; set; }
        public string Question_ { get; set; }
        public AnswerType[] AnswerTypes_ { get; set; }
        
        public AnswerType UserAnswer { get; set; }
    }
}