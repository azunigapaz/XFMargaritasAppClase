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
    public partial class RegistroPage : ContentPage
    {
        public RegistroPage()
        {
            InitializeComponent();
            TapGestureRecognizer_Tapped();
        }

        private async void btnregistrarusuario_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.ConfirmarUsuarioPage());
        }

        private void TapGestureRecognizer_Tapped()
        {
            lbl_loginpage.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    Navigation.PushAsync(new Views.LoginPage());
                })
            });
        }
    }
}