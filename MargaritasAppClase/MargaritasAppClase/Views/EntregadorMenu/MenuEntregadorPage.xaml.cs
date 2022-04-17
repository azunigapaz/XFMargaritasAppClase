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

namespace MargaritasAppClase.Views.EntregadorMenu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuEntregadorPage : ContentPage
    {
        List<EntregadorListPedidosModel> listaordenesrepartidor = null;

        string correo = Application.Current.Properties["correo"].ToString();

        public MenuEntregadorPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetOrdenesRepartidorList();
        }

        private async void GetOrdenesRepartidorList()
        {
            try
            {
                var AccesoInternet = Connectivity.NetworkAccess;

                if (AccesoInternet == NetworkAccess.Internet)
                {
                    slrepartidormainpage.IsVisible = true;
                    spinnerrepartidormainpage.IsRunning = true;

                    listaordenesrepartidor = new List<EntregadorListPedidosModel>();
                    listaordenesrepartidor = await ProductsApiController.ControllerObtenerListaOrdenesEntregador(correo);

                    if (listaordenesrepartidor.Count > 0)
                    {
                        listview_mainproductosentregador.ItemsSource = null;
                        listview_mainproductosentregador.ItemsSource = listaordenesrepartidor;
                    }
                    else
                    {
                        await DisplayAlert("Notificación", $"Lista vacía, ingrese datos", "Ok");
                    }

                    slrepartidormainpage.IsVisible = false;
                    spinnerrepartidormainpage.IsRunning = false;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void btnverorden_Clicked(object sender, EventArgs e)
        {

        }

        private void btncambiarstatusorden_Clicked(object sender, EventArgs e)
        {

        }

        private void btnverubiacionorden_Clicked(object sender, EventArgs e)
        {

        }
    }
}