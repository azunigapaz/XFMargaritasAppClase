using MargaritasAppClase.Controller;
using MargaritasAppClase.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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

        byte[] newBytes = null;
        string id = "", nombre = "", apellido = "", telefono = "", foto = "";


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

        private async void btnverorden_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EntregadorDetallePedidoPage());
            //var item = (sender as Button).BindingContext as EntregadorListPedidosModel;
            //await DisplayAlert("Aviso", item.id_pedido.ToString(), "Ok");

            //string correoCliente, ordenNumero;
            //correoCliente = item.id_cliente.ToString();
            //ordenNumero = item.id_pedido.ToString();
        }

        private void btncambiarstatusorden_Clicked(object sender, EventArgs e)
        {

        }

        private async void btnverubiacionorden_Clicked(object sender, EventArgs e)
        {

            var item = (sender as Button).BindingContext as EntregadorListPedidosModel;
            double lat = Convert.ToDouble(item.latitud.ToString());
            double lon = Convert.ToDouble(item.longitud.ToString());
            await Navigation.PushAsync(new MapaEntregadorPage(lat, lon));
        }

        private async void btnverperfilcliente_Clicked(object sender, EventArgs e)
        {
            var item = (sender as Button).BindingContext as EntregadorListPedidosModel;
            string correoClientePedido = item.id_cliente.ToString();
            await Navigation.PushAsync(new VerPerfilClientePage(correoClientePedido));
        }

       

    }
}