using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MargaritasAppClase.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using Newtonsoft.Json.Linq;
using System.IO;

namespace MargaritasAppClase.Views.TabbedMenu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PerfilPage : ContentPage
    {
        
        public PerfilPage()
        {
            InitializeComponent();            
        }

        private async void btnactualizarperfilpage_Clicked(object sender, EventArgs e)
        {

            GetPerfilModel getPerfil = new GetPerfilModel
            {
                authmail = Application.Current.Properties["correo"].ToString(),
            };

            Uri RequestUri = new Uri("https://webfacturacesar.000webhostapp.com/Margarita/methods/cliente/");

            var client = new HttpClient();
            var json = JsonConvert.SerializeObject(getPerfil);
            var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(RequestUri, contentJson);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                String jsonx = response.Content.ReadAsStringAsync().Result;
                JObject jsons = JObject.Parse(jsonx);
                //String Mensaje = jsons["msg"].ToString();

                string contenido = response.Content.ReadAsStringAsync().Result.ToString();

                dynamic dyn = JsonConvert.DeserializeObject(contenido);
                byte[] newBytes = null;

                var item = dyn.items;
                
                //string img64 = item.Foto.ToString();
                /*
                newBytes = Convert.FromBase64String(img64);
                var stream = new MemoryStream(newBytes);
                */

            }
            else
            {
                await DisplayAlert("Error", "Estamos en mantenimiento", "Ok");
            }



            await Navigation.PushAsync(new ActualizarPerfilPage());
        }

        private async void btncerrarsesion_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties.Remove("correo");
            await Navigation.PushAsync(new LoginPage());
            Navigation.RemovePage(Navigation.NavigationStack[0]);
        }
    }
}