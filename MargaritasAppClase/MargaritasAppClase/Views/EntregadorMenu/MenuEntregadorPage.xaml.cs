using MargaritasAppClase.Controller;
using MargaritasAppClase.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
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
            var item = (sender as Button).BindingContext as EntregadorListPedidosModel;

            string correoCliente, ordenNumero, fecha, direccion, audio;
            correoCliente = item.id_cliente;
            ordenNumero = item.ult_cor_pedido;
            fecha = item.fh_entrega;
            direccion = item.direccion;
            audio = item.audio;

            await Navigation.PushAsync(new EntregadorDetallePedidoPage(ordenNumero, fecha, direccion, correoCliente, audio));
        }

        private async void btncambiarstatusorden_Clicked(object sender, EventArgs e)
        {
            var item = (sender as Button).BindingContext as EntregadorListPedidosModel;
            string ordenCorrelativo = item.ult_cor_pedido;
            string orden = item.id_pedido;
            string cliente = item.id_cliente;
            string estado = item.ID_Estado;

            var alert = await DisplayAlert("Margaritas App", "Seleccione el estatus de la orden", "Orden Entregada", "En Proceso");

            if (alert)
            {
                if (estado == "3")
                {
                    CambiarStatusOrdenModel save = new CambiarStatusOrdenModel
                    {
                        Action = "est",
                        ID_Cliente = cliente,
                        Correl = ordenCorrelativo,
                        Estado = "4"
                    };

                    Uri RequestUri = new Uri("https://webfacturacesar.000webhostapp.com/Margarita/methods/orders/");

                    var client = new HttpClient();
                    var json = JsonConvert.SerializeObject(save);
                    var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(RequestUri, contentJson);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        String jsonx = response.Content.ReadAsStringAsync().Result;
                        JObject jsons = JObject.Parse(jsonx);
                        String Mensaje = jsons["msg"].ToString();
                        await DisplayAlert("Success", "Orden " + orden + " Entregada", "Ok");
                        GetOrdenesRepartidorList();


                        var notificacion = new NotificationRequest
                        {
                            BadgeNumber = 1,
                            Title = "Status de Orden",
                            Description = "Orden " + orden + " entregada, gracias por su preferencia",
                            ReturningData = "Dummy Data",
                            NotificationId = 1337,

                        };
                        await NotificationCenter.Current.Show(notificacion);

                    }
                    else
                    {
                        await DisplayAlert("Error", "Estamos en mantenimiento", "Ok");
                    }
                }
                else
                {
                    await DisplayAlert("Aviso", "Solo se pueden cerrar ordenes en proceso", "Ok");
                }
            }
            else
            {
                if(estado == "2")
                {
                    CambiarStatusOrdenModel save = new CambiarStatusOrdenModel
                    {
                        Action = "est",
                        ID_Cliente = cliente,
                        Correl = ordenCorrelativo,
                        Estado = "3"
                    };

                    Uri RequestUri = new Uri("https://webfacturacesar.000webhostapp.com/Margarita/methods/orders/");

                    var client = new HttpClient();
                    var json = JsonConvert.SerializeObject(save);
                    var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(RequestUri, contentJson);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        String jsonx = response.Content.ReadAsStringAsync().Result;
                        JObject jsons = JObject.Parse(jsonx);
                        String Mensaje = jsons["msg"].ToString();
                        await DisplayAlert("Success", "Orden " + orden + " en Proceso", "Ok");
                        GetOrdenesRepartidorList();

                        var notificacion = new NotificationRequest
                        {
                            BadgeNumber = 1,
                            Title = "Status de Orden",
                            Description = "Orden " + orden + " en proceso, favor estar pendiente",
                            ReturningData = "Dummy Data",
                            NotificationId = 1337,

                        };
                        await NotificationCenter.Current.Show(notificacion);

                    }
                    else
                    {
                        await DisplayAlert("Error", "Estamos en mantenimiento", "Ok");
                    }
                }
                else
                {
                    await DisplayAlert("Aviso", "Estatus solo es valido, para ordenes asignadas", "Ok");
                }


            }
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