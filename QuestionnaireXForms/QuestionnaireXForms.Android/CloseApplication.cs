
using System;
using Android.App;
using QuestionnaireXForms.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(CloseApplication))]
namespace QuestionnaireXForms.Droid
{
    public class CloseApplication : ICloseApplication
    {
        public void Close()
        {
            try
            {
                if (Forms.Context == null) return;
                var activity = (Activity)Forms.Context;
                activity.FinishAffinity();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }
        }
    }
}
