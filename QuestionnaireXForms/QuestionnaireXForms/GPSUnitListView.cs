
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using QuestionnaireXForms.Domain;
using Xamarin.Forms;

namespace QuestionnaireXForms
{
    public class GPSUnitListView : ListView, INotifyPropertyChanged
    {
        public static readonly BindableProperty ItemsProperty =
            BindableProperty.Create("Items", typeof(IEnumerable<GPSUnit>), typeof(NativeListView), new ObservableCollection<GPSUnit>());

        public ObservableCollection<GPSUnit> Items
        {
            get { return (ObservableCollection<GPSUnit>)GetValue(ItemsProperty); }
            set
            {
                SetValue(ItemsProperty, value);
                OnPropertyChanged();
            }
        }

        /*
        public ObservableCollection<GPSUnit> Items
        {
            get => (ObservableCollection<GPSUnit>)GetValue(ItemsProperty);
            set
            {
                SetValue(ItemsProperty, value);
                OnPropertyChanged();
            }
        }*/

        public event EventHandler<SelectedItemChangedEventArgs> ItemSelected;

        public void NotifyItemSelected(object item)
        {
            ItemSelected?.Invoke(this, new SelectedItemChangedEventArgs(item));
        }
    }
}
