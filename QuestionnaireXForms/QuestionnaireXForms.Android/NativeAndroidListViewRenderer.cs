

using System.Linq;
using QuestionnaireXForms;
using QuestionnaireXForms.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer (typeof(NativeListView), typeof(NativeAndroidListViewRenderer))]
namespace QuestionnaireXForms.Droid
{
    public class NativeAndroidListViewRenderer : ListViewRenderer
    {
        protected override void OnElementChanged (ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged (e);

            if (e.OldElement != null) {
                // unsubscribe
                Control.ItemClick -= OnItemClick;
            }

            if (e.NewElement != null) {
                // subscribe
                Control.Adapter = new NativeAndroidListViewAdapter (Forms.Context as Android.App.Activity, e.NewElement as NativeListView);
                Control.ItemClick += OnItemClick;
            }
        }

        protected override void OnElementPropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged (sender, e);

            if (e.PropertyName == NativeListView.ItemsProperty.PropertyName) {
                Control.Adapter = new NativeAndroidListViewAdapter (Forms.Context as Android.App.Activity, Element as NativeListView);
            }
        }

        void OnItemClick (object sender, Android.Widget.AdapterView.ItemClickEventArgs e)
        {			
            ((NativeListView)Element).NotifyItemSelected (((NativeListView)Element).Items.ToList () [e.Position - 1]);
        }
    }
}
