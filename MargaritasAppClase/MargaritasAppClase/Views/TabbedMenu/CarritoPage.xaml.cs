using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MargaritasAppClase.Controller;
using MargaritasAppClase.Models;
using Xamarin.Essentials;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using Newtonsoft.Json.Linq;

namespace MargaritasAppClase.Views.TabbedMenu
{    
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarritoPage : ContentPage
    {
        int cantidadProducto = 0;
        string correo = Application.Current.Properties["correo"].ToString();
        public CarritoPage()
        {
            InitializeComponent();
            //GetCarritoList();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetCarritoList();
        }

        private async void btnagregarproductocarrito_Clicked(object sender, EventArgs e)
        {
            //var item = (sender as Button).BindingContext as CarritoListModel;
            //await DisplayAlert("Success", "Item: " + item.desc_prod.ToString() + ", Modificado", "Ok");

            try
            {
                var item = (sender as Button).BindingContext as CarritoListModel;
                //await DisplayAlert("Aviso", item.Descripcion.ToString(), "Ok");

                int nuevacantidad = Convert.ToInt32(item.Cantidad.ToString()) + 1;
                double nuevoTotal = Convert.ToInt32(item.Precio.ToString()) * nuevacantidad;

                var listDetalle = new List<CarritoDetalleModel>();
                listDetalle.Add(new CarritoDetalleModel()
                {
                    ID_Producto = item.ID_Producto.ToString(),
                    Cantidad = nuevacantidad.ToString(),
                    Precio = item.Precio.ToString(),
                    Total = nuevoTotal.ToString("#.00")
                });

                SaveCarritoModel jsonObject = new SaveCarritoModel();
                jsonObject.Carrito = new List<CarritoEncabezadoModel>();

                double ldImpueto = 0.00, ldTotal = 0.00;
                ldImpueto = nuevoTotal * .15;
                ldTotal = nuevoTotal + ldImpueto;
                string totImpueto = "", totCarrito = "";
                totImpueto = ldImpueto.ToString("0.##");
                totCarrito = ldTotal.ToString("0.##");

                jsonObject.Carrito.Add(new CarritoEncabezadoModel()
                {

                    ID_Cliente = correo,
                    Fecha = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"),
                    SubTotal = nuevoTotal.ToString("#.00"),
                    ISV = totImpueto,
                    Total = totCarrito,
                    DetalleCarrito = listDetalle

                });


                Uri RequestUri = new Uri("https://webfacturacesar.000webhostapp.com/Margarita/methods/carrito/carritoapp.php");

                var client = new HttpClient();
                var json = JsonConvert.SerializeObject(jsonObject);

                var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(RequestUri, contentJson);

                if (response.StatusCode == HttpStatusCode.OK)
                {

                    String jsonx = response.Content.ReadAsStringAsync().Result;
                    JObject jsons = JObject.Parse(jsonx);
                    String Mensaje = jsons["msg"].ToString();
                    await DisplayAlert("Success", "Cantidad del item: " + item.desc_prod.ToString() + ", actualizada", "Ok");
                    GetCarritoList();

                }
                else
                {
                    await DisplayAlert("Error", "Estamos en mantenimiento", "Ok");
                }

                //await DisplayAlert("Aviso", json, "Ok");                

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "Ok");
            }

        }

        private async void btnrestarproductocarrito_Clicked(object sender, EventArgs e)
        {
            try
            {
                var item = (sender as Button).BindingContext as CarritoListModel;
                //await DisplayAlert("Aviso", item.Descripcion.ToString(), "Ok");

                if(Convert.ToInt32(item.Cantidad.ToString()) > 1)
                {
                    int nuevacantidad = Convert.ToInt32(item.Cantidad.ToString()) - 1;
                    double nuevoTotal = Convert.ToInt32(item.Precio.ToString()) * nuevacantidad;

                    var listDetalle = new List<CarritoDetalleModel>();
                    listDetalle.Add(new CarritoDetalleModel()
                    {
                        ID_Producto = item.ID_Producto.ToString(),
                        Cantidad = nuevacantidad.ToString(),
                        Precio = item.Precio.ToString(),
                        Total = nuevoTotal.ToString("#.00")
                    });

                    SaveCarritoModel jsonObject = new SaveCarritoModel();
                    jsonObject.Carrito = new List<CarritoEncabezadoModel>();

                    double ldImpueto = 0.00, ldTotal = 0.00;
                    ldImpueto = nuevoTotal * .15;
                    ldTotal = nuevoTotal + ldImpueto;
                    string totImpueto = "", totCarrito = "";
                    totImpueto = ldImpueto.ToString("0.##");
                    totCarrito = ldTotal.ToString("0.##");

                    jsonObject.Carrito.Add(new CarritoEncabezadoModel()
                    {

                        ID_Cliente = correo,
                        Fecha = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss"),
                        SubTotal = nuevoTotal.ToString("#.00"),
                        ISV = totImpueto,
                        Total = totCarrito,
                        DetalleCarrito = listDetalle

                    });


                    Uri RequestUri = new Uri("https://webfacturacesar.000webhostapp.com/Margarita/methods/carrito/carritoapp.php");

                    var client = new HttpClient();
                    var json = JsonConvert.SerializeObject(jsonObject);

                    var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(RequestUri, contentJson);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {

                        String jsonx = response.Content.ReadAsStringAsync().Result;
                        JObject jsons = JObject.Parse(jsonx);
                        String Mensaje = jsons["msg"].ToString();
                        await DisplayAlert("Success", "Cantidad del item: " + item.desc_prod.ToString() + ", actualizada", "Ok");
                        GetCarritoList();

                    }
                    else
                    {
                        await DisplayAlert("Error", "Estamos en mantenimiento", "Ok");
                    }

                    //await DisplayAlert("Aviso", json, "Ok");                
                }
                else
                {
                    await DisplayAlert("Alerta", "La cantidad debe ser mayor a cero", "Ok");
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "Ok");
            }

        }

        private async void btnquitarproductocarrito_Clicked(object sender, EventArgs e)
        {
            try
            {
                var item = (sender as Button).BindingContext as CarritoListModel;               

                EliminarItemCarritoModel save = new EliminarItemCarritoModel
                {
                    ID_Cliente = correo,
                    ID_Producto = item.ID_Producto
                };

                Uri RequestUri = new Uri("https://webfacturacesar.000webhostapp.com/Margarita/methods/carrito/del.php");

                var client = new HttpClient();
                var json = JsonConvert.SerializeObject(save);
                var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(RequestUri, contentJson);

                if (response.StatusCode == HttpStatusCode.OK)
                {

                    String jsonx = response.Content.ReadAsStringAsync().Result;
                    JObject jsons = JObject.Parse(jsonx);
                    String Mensaje = jsons["msg"].ToString();

                    await DisplayAlert("Success", "Item: " + item.desc_prod.ToString() + ", eliminado", "Ok");
                    GetCarritoList();
                }

                else
                {
                    await DisplayAlert("Error", "Estamos en mantenimiento", "Ok");
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "Ok");
            }
        }

        private void btnagregarubicacioncarrito_Clicked(object sender, EventArgs e)
        {

        }

        private void btnfechadeentrega_Clicked(object sender, EventArgs e)
        {

        }

        private void btnaudioreferencia_Clicked(object sender, EventArgs e)
        {

        }

        private async void btnprocesarorden_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PagoOrdenPage());
        }

        private async void GetCarritoList()
        {
            var AccesoInternet = Connectivity.NetworkAccess;

            if (AccesoInternet == NetworkAccess.Internet)
            {
                sl.IsVisible = true;
                spinner.IsRunning = true;

                List<CarritoListModel> listacarrito = new List<CarritoListModel>();
                listacarrito = await ProductsApiController.ControllerObtenerListaCarrito(correo);

                if (listacarrito.Count > 0)
                {
                    listview_carritoproductos.ItemsSource = null;
                    listview_carritoproductos.ItemsSource = listacarrito;                    

                    double subtotal = 0, impuesto = 0, total = 0;

                   foreach(var v in listacarrito)
                    {
                        subtotal = subtotal + Convert.ToDouble(v.Cantidad.ToString()) * Convert.ToDouble(v.Precio.ToString());
                    }
                    impuesto = subtotal * .15;
                    total = subtotal + impuesto;

                    lblsubtotal.Text = "L. " + subtotal.ToString("#,#.00");
                    lblisv.Text = "L. " + impuesto.ToString("#,#.00");
                    lbltotalapagar.Text = "L. " + total.ToString("#,#.00");
                }
                else
                {
                    await DisplayAlert("Notificación", $"Lista vacía, ingrese datos", "Ok");
                }

                List<UbicacionesListModel> listaubicaciones = new List<UbicacionesListModel>();
                listaubicaciones = await ProductsApiController.ControllerObtenerListaUbicaciones(correo);

                if (listaubicaciones.Count > 0)
                {

                    selectubicacion.ItemsSource = null;
                    //selectubicacion.ItemsSource = listacarrito;                    

                    var pickerList = new List<String>();

                    foreach (var v in listaubicaciones)
                    {
                        pickerList.Add(v.ID_Ubicacion.ToString() + "-" + v.Direccion.ToString());                                                 
                    }

                    selectubicacion.ItemsSource = pickerList;
                    selectubicacion.SelectedIndex = 0;
                }
                else
                {
                    await DisplayAlert("Notificación", $"Lista vacía, ingrese datos", "Ok");
                }

                sl.IsVisible = false;
                spinner.IsRunning = false;
            }
        }

        private void selectubicacion_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}