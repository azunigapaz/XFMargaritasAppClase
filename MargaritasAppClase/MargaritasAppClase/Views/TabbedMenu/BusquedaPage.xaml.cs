using MargaritasAppClase.Controller;
using MargaritasAppClase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MargaritasAppClase.Views.TabbedMenu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BusquedaPage : ContentPage
    {
        public BusquedaPage()
        {
            InitializeComponent();
        }

        private void btnubicacionpedido_Clicked(object sender, EventArgs e)
        {

        }

        private async void btnverdetallepedido_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DetallePedidoPage());
        }

        private async void btnevaluarservicio_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EvaluacionPage());
        }
    }
}