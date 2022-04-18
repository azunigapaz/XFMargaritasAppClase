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
    public partial class DetallePedidoPage : ContentPage
    {
        string correo = Application.Current.Properties["correo"].ToString();
        string correlativo;
        public DetallePedidoPage(string correlativo)
        {
            this.correlativo = correlativo;
            InitializeComponent();
            GetOrdenDetalleList();
        }

        private void btnescucharaudioreferencia_Clicked(object sender, EventArgs e)
        {

        }


        private async void GetOrdenDetalleList()
        {
            try
            {
                var AccesoInternet = Connectivity.NetworkAccess;

                if (AccesoInternet == NetworkAccess.Internet)
                {
                    sl_detallepedido.IsVisible = true;
                    spinner_detallepedido.IsRunning = true;

                    List<ClienteListaPedidosDetalleModel> listaordendetalle = new List<ClienteListaPedidosDetalleModel>();
                    listaordendetalle = await ProductsApiController.ControllerObtenerListaOrdenesClienteDetalle(correo,correlativo);

                    if (listaordendetalle.Count > 0)
                    {
                        listview_detallepedido.ItemsSource = null;
                        listview_detallepedido.ItemsSource = listaordendetalle;
                    }
                    else
                    {
                        await DisplayAlert("Notificación", $"Lista vacía, ingrese datos", "Ok");
                    }

                    sl_detallepedido.IsVisible = false;
                    spinner_detallepedido.IsRunning = false;
                }
            }
            catch (Exception ex)
            {

            }

        }

    }
}