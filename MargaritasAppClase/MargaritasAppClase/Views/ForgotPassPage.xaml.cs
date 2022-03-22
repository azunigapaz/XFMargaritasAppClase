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
    public partial class ForgotPassPage : ContentPage
    {
        public ForgotPassPage()
        {
            InitializeComponent();
        }

        private async void btnenviarcorreo_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CorreoCodePage());
        }
    }
}