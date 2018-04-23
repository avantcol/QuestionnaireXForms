
using Newtonsoft.Json.Linq;

namespace QuestionnaireXForms.Domain
{
    public class Question
    {
        public enum AnswerType
        {
            NoAnswer = 0,
            NotApply = 1,
            Ok = 2,
            Fail = 3,
        };

        public static AnswerType FromJSON(JToken jt)
        {
            switch (jt["id"].Value<int>())
            {
                case 0: return AnswerType.NoAnswer;
                case 1: return AnswerType.NotApply;
                case 2: return AnswerType.Ok;
                case 3: return AnswerType.Fail;
            }

            return AnswerType.NoAnswer;
        }

        public long Id_ { get; set; }
        public string Question_ { get; set; }
        public AnswerType[] AnswerTypes_ { get; set; }

        private AnswerType _userAnswer;
        public AnswerType UserAnswer { get; set; }

        public string Observation { get; set; }
        
        public string UserAnserAsString
        {
            get
            {
                if (UserAnswer == AnswerType.NoAnswer)
                    return "";
                return UserAnswer.ToString();
            }
        }
    }
}
