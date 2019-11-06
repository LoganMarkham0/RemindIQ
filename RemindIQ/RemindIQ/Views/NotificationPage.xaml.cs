using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RemindIQ.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificationPage : ContentView
    {
        public NotificationPage(int count)
        {
            InitializeComponent();

            TapCountLabel.Text = $"Tap count {count}";
        }
    }
}