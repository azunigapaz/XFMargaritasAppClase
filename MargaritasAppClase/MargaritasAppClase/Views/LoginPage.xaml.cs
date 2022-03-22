using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MargaritasAppClase.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            TapRegistroPage_Tapped();
            TapForgotPassPage_Tapped();
        }

        private void btniniciarsesion_Clicked(object sender, EventArgs e)
        {

        }

        private void TapRegistroPage_Tapped() => lbl_registropage.GestureRecognizers.Add(new TapGestureRecognizer()
        {
            Command = new Command(() =>
            {
                Navigation.PushAsync(new Views.RegistroPage());
            })
        });

        private void TapForgotPassPage_Tapped() => lbl_forgotpasspage.GestureRecognizers.Add(new TapGestureRecognizer()
        {
            Command = new Command(() =>
            {
                Navigation.PushAsync(new Views.ForgotPassPage());
            })
        });
    }
}