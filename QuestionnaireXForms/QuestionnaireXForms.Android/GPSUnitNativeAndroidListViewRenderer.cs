
using System.Linq;
using QuestionnaireXForms;
using QuestionnaireXForms.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer (typeof(GPSUnitListView), typeof(GPSUnitNativeAndroidListViewRenderer))]
namespace QuestionnaireXForms.Droid
{
    public class GPSUnitNativeAndroidListViewRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                // unsubscribe
                Control.ItemClick -= OnItemClick;
            }

            if (e.NewElement != null)
            {
                // subscribe
                Control.Adapter = new GPSUnitNativeAndroidListViewAdapter(Forms.Context as Android.App.Activity, e.NewElement as GPSUnitListView);
                Control.ItemClick += OnItemClick;
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == GPSUnitListView.ItemsProperty.PropertyName)
            {
                Control.Adapter =
                    new GPSUnitNativeAndroidListViewAdapter(Forms.Context as Android.App.Activity, Element as GPSUnitListView);
            }
        }

        void OnItemClick(object sender, Android.Widget.AdapterView.ItemClickEventArgs e)
        {
            ((GPSUnitListView) Element).NotifyItemSelected(((GPSUnitListView) Element).Items.ToList()[e.Position - 1]);
        }
    }
}