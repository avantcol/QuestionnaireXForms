
using Android.App;
using QuestionnaireXForms;
using Xamarin.Forms;

[assembly: Dependency(typeof(ICloseApplication))]
namespace QuestionnaireXForms.Droid
{
    public class CloseApplication : ICloseApplication
    {
        public void closeApplication()
        {
            if (Forms.Context == null) return;
            var activity = (Activity)Forms.Context;
            activity.FinishAffinity();
        }
    }
}
