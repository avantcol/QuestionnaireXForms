

using System.Collections.ObjectModel;
using Android.App;
using Android.Views;
using Android.Widget;
using QuestionnaireXForms.Domain;
using View = Android.Views.View;

namespace QuestionnaireXForms.Droid
{
    /// <summary>
    /// This adapter uses a view defined in /Resources/Layout/NativeAndroidListViewCell.axml
    /// as the cell layout
    /// </summary>
    public class NativeAndroidListViewAdapter : BaseAdapter<Question>
    {
        readonly Activity _context;
        ObservableCollection<Question> _tableItems;

        public ObservableCollection<Question> Items {
            set { 
                _tableItems = new ObservableCollection<Question>( value );
            }
        }

        public NativeAndroidListViewAdapter (Activity context, NativeListView view)
        {
            _context = context;
            _tableItems = new ObservableCollection<Question>( view.Items );
        }

        public override Question this [int position] {
            get { 
                return _tableItems [position];
            }
        }

        public override long GetItemId (int position)
        {
            return position;
        }

        public override int Count {
            get { return _tableItems.Count; }
        }

        public override View GetView (int position, View convertView, ViewGroup parent)
        {
            var item = _tableItems [position];

            var view = convertView;
            if (view == null) {
                // no view to re-use, create new
                view = _context.LayoutInflater.Inflate (Resource.Layout.NativeAndroidListViewCell, null);
            }
            view.FindViewById<TextView> (Resource.Id.Text1).Text = item.Question_;

            view.FindViewById<TextView> (Resource.Id.Text2).Text = item.UserAnserAsString;

            //Binding companyBinding = new Binding { Source = phone, Path = "Company" };
            //companyValueLabel.SetBinding(Label.TextProperty, companyBinding);
            
            // grab the old image and dispose of it
            /*           
            if (view.FindViewById<ImageView> (Resource.Id.Image).Drawable != null) {
                using (var image = view.FindViewById<ImageView> (Resource.Id.Image).Drawable as BitmapDrawable) {
                    if (image != null) {
                        if (image.Bitmap != null) {
                            //image.Bitmap.Recycle ();
                            image.Bitmap.Dispose ();
                        }
                    }
                }
            }
            */

            // If a new image is required, display it
            /*
            if (!String.IsNullOrWhiteSpace (item.ImageFilename)) {
                context.Resources.GetBitmapAsync (item.ImageFilename).ContinueWith ((t) => {
                    var bitmap = t.Result;
                    if (bitmap != null) {
                        view.FindViewById<ImageView> (Resource.Id.Image).SetImageBitmap (bitmap);
                        bitmap.Dispose ();
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext ());
            } else {
                // clear the image
                view.FindViewById<ImageView> (Resource.Id.Image).SetImageBitmap (null);
            }*/

            return view;
        }

    }
}