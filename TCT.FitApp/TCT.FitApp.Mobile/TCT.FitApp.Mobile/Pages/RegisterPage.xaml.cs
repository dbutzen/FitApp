using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TCT.FitApp.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void btnRegister_Clicked(object sender, EventArgs e)
        {
            App.LoggedInUser = new Models.User { Name = "Test" };
            App.ReturnPage = ReturnPage.Home;
            Navigation.PopModalAsync();
        }

        private void btnLogin_Clicked(object sender, EventArgs e)
        {
            App.ReturnPage = ReturnPage.Login;
            Navigation.PopModalAsync();
        }
    }
}