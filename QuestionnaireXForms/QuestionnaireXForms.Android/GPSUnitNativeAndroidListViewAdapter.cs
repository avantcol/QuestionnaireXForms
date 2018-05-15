
using System.Collections.ObjectModel;
using Android.App;
using Android.Views;
using Android.Widget;
using QuestionnaireXForms.Domain;

namespace QuestionnaireXForms.Droid
{
    public class GpsUnitNativeAndroidListViewAdapter : BaseAdapter<GPSUnit>
    {
        readonly Activity _context;
        ObservableCollection<GPSUnit> _tableItems;

        public ObservableCollection<GPSUnit> Items {
            set => _tableItems = new ObservableCollection<GPSUnit>( value );
        }

        public GpsUnitNativeAndroidListViewAdapter (Activity context, GPSUnitListView view)
        {
            _context = context;
            _tableItems = new ObservableCollection<GPSUnit>( view.Items );
        }

        public override GPSUnit this [int position] => _tableItems [position];

        public override long GetItemId (int position)
        {
            return position;
        }

        public override int Count => _tableItems.Count;

        public override View GetView (int position, View convertView, ViewGroup parent)
        {
            var item = _tableItems [position];

            var view = convertView;
            if (view == null) {
                // no view to re-use, create new
                view = _context.LayoutInflater.Inflate (Resource.Layout.NativeAndroidListViewCell, null);
            }
            view.FindViewById<TextView> (Resource.Id.Text1).Text = item.DisplayName;

            /*
            view.FindViewById<TextView> (Resource.Id.Text2).Text = item.UserAnserAsString;
            */

            return view;
        }
    }
}
