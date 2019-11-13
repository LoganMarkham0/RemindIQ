using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RemindIQ.Models;

namespace RemindIQ.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificationPage : ContentView
    {
        public NotificationPage(string rem)
        {
            InitializeComponent();

            TapCountLabel.Text = $"test of location '{rem}' being within range";
        }
    }
}