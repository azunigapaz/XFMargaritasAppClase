using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MargaritasAppClase.Views.EntregadorMenu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PerfilEntregadorPage : ContentPage
    {
        public PerfilEntregadorPage()
        {
            InitializeComponent();
        }

        private async void btnactualizarperfilrepartidorpage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ActualizarPerfilEntregadorPage());
        }

        private void btncerrarsesionrepartidor_Clicked(object sender, EventArgs e)
        {

        }
    }
}