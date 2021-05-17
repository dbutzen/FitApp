using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCT.FitApp.Mobile.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TCT.FitApp.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActivityPage : ContentPage
    {
        DayItem dayItem = new DayItem();
        User user;

        public ActivityPage(User user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void btnHome_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void btnAdd_Clicked(object sender, EventArgs e)
        {
            
        }
    }
}