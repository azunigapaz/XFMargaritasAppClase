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
    public partial class ListaUbicacionesPage : ContentPage
    {
        public ListaUbicacionesPage()
        {
            InitializeComponent();
        }

        private async void tbiagregarubicacionespage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AgregarUbicacionesPage());
        }

        private void ls_ubicaciones_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}