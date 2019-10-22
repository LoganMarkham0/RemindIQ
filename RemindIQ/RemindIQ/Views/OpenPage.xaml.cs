using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RemindIQ.Models;
using RemindIQ.ViewModels;


namespace RemindIQ.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OpenPage : ContentPage
    {
        RemindersViewModel ViewModel;
        public OpenPage()
        {
            InitializeComponent();

            BindingContext = ViewModel = new RemindersViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (ViewModel.Open.Count == 0)
                ViewModel.LoadOpenCommand.Execute(null);
        }
        /*
        private void Left_Swiped(object sender, SwipedEventArgs e)
        {
            //DELETE
        }
        private void Right_Swiped(object sender, SwipedEventArgs e)
        {
            //COMPLETE
        }
        private void Show_Reminder(object sender, EventArgs e)
        {
            //SHOW
        }
        */
    }
}