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

namespace MargaritasAppClase.Views.TabbedMenu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarritoPage : ContentPage
    {
        string correo = Application.Current.Properties["correo"].ToString();
        public CarritoPage()
        {
            InitializeComponent();
        }

        private void btnagregarproductocarrito_Clicked(object sender, EventArgs e)
        {

        }

        private void btnrestarproductocarrito_Clicked(object sender, EventArgs e)
        {

        }

        private void btnquitarproductocarrito_Clicked(object sender, EventArgs e)
        {

        }

        private void btnseleccionarubicacion_Clicked(object sender, EventArgs e)
        {

        }

        private void btnfechadeentrega_Clicked(object sender, EventArgs e)
        {

        }

        private void btnaudioreferencia_Clicked(object sender, EventArgs e)
        {

        }

        private void btnrealizarpedido_Clicked(object sender, EventArgs e)
        {

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
                }
                else
                {
                    await DisplayAlert("Notificación", $"Lista vacía, ingrese datos", "Ok");
                }

                sl.IsVisible = false;
                spinner.IsRunning = false;
            }
        }
    }
}