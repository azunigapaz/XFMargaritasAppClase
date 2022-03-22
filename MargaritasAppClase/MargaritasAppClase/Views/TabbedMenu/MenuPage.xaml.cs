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
    public partial class MenuPage : ContentPage
    {

        public MenuPage()
        {
            InitializeComponent();
            GetProductsList();
        }

        private void btnagregarcarrito_Clicked(object sender, EventArgs e)
        {

        }

        private async void GetProductsList()
        {
            var AccesoInternet = Connectivity.NetworkAccess;

            if (AccesoInternet == NetworkAccess.Internet)
            {
                sl.IsVisible = true;
                spinner.IsRunning = true;

                List<ProductsListModel> listaproducros = new List<ProductsListModel>();
                listaproducros = await ProductsApiController.ControllerObtenerListaProductos();

                if (listaproducros.Count > 0)
                {
                    listview_mainproductos.ItemsSource = null;
                    listview_mainproductos.ItemsSource = listaproducros;
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