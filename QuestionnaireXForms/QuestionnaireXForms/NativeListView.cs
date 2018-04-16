using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using QuestionnaireXForms.Domain;
using Xamarin.Forms;

namespace QuestionnaireXForms
{
    /// <summary>
    /// Xamarin.Forms representation for a custom-renderer that uses 
    /// the native list control on each platform.
    /// </summary>
    public class NativeListView : ListView, INotifyPropertyChanged
    {
        public static readonly BindableProperty ItemsProperty =
            BindableProperty.Create("Items", typeof(IEnumerable<Question>), typeof(NativeListView), new ObservableCollection<Question>());

        public ObservableCollection<Question> Items
        {
            get { return (ObservableCollection<Question>)GetValue(ItemsProperty); }
            set
            {
                SetValue(ItemsProperty, value);
                OnPropertyChanged();
            }
        }

        public event EventHandler<SelectedItemChangedEventArgs> ItemSelected;

        public void NotifyItemSelected(object item)
        {
            if (ItemSelected != null)
            {
                ItemSelected(this, new SelectedItemChangedEventArgs(item));
            }
        }
    }
}
