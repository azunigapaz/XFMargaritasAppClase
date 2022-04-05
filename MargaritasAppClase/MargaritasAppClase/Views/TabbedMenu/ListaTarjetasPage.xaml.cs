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
    public partial class ListaTarjetasPage : ContentPage
    {
        public ListaTarjetasPage()
        {
            InitializeComponent();
        }

        private async void tbiagregartarjetaspage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AgregarTarjetaPage());
        }

        private void ls_tarjetas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }
    }
}