using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MargaritasAppClase.Views.TabbedMenu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PagoOrdenPage : ContentPage
    {
        public PagoOrdenPage()
        {
            InitializeComponent();
        }

        private void btnprocesarorden_Clicked(object sender, EventArgs e)
        {

        }

        private void btnrealizarpagoefectivo_Clicked(object sender, EventArgs e)
        {
            
        }

        private void btnrealizarpagotarjeta_Clicked(object sender, EventArgs e)
        {

        }

        private async void btnagregartarjetaorden_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListaTarjetasPage());
        }
    }
}