using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TCT.FitApp.Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
            Authenticate();
        }


        private async void Authenticate()
        {
            if (App.LoggedInUser == null)
            {
                if (App.ReturnPage == ReturnPage.Login)
                {
                    var waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
                    var page = new LoginPage { Title = "Login" };
                    page.Disappearing += (sender2, e2) =>
                    {
                        waitHandle.Set();
                    };
                    await Navigation.PushModalAsync(new NavigationPage(page));
                    await Task.Run(() => waitHandle.WaitOne());
                }
                else if (App.ReturnPage == ReturnPage.Register)
                {
                    var waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
                    var page = new RegisterPage() { Title = "Register" };
                    page.Disappearing += (sender2, e2) =>
                    {
                        waitHandle.Set();
                    };
                    await Navigation.PushModalAsync(new NavigationPage(page));
                    await Task.Run(() => waitHandle.WaitOne());
                }
                Authenticate();
                return;
            }
            Title = App.LoggedInUser.Name;

        }
    }
}