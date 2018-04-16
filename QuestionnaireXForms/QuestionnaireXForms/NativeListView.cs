using System;
using System.Collections.Generic;
using QuestionnaireXForms.Domain;
using Xamarin.Forms;

namespace QuestionnaireXForms
{
    /// <summary>
    /// Xamarin.Forms representation for a custom-renderer that uses 
    /// the native list control on each platform.
    /// </summary>
    public class NativeListView : ListView
    {
        public static readonly BindableProperty ItemsProperty =
            BindableProperty.Create("Items", typeof(IEnumerable<Question>), typeof(NativeListView), new List<Question>());

        public IEnumerable<Question> Items
        {
            get { return (IEnumerable<Question>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
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
