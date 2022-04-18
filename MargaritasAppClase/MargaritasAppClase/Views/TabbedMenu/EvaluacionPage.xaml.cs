using MargaritasAppClase.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MargaritasAppClase.Views.TabbedMenu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EvaluacionPage : ContentPage
    {
        string correo = Application.Current.Properties["correo"].ToString();

        string correl;
        int nota1 = 0, nota2 = 0, nota3 = 0, nota4 = 0;

        private void muybuenocalentregadorpdos_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            nota2 = 4;
        }

        public EvaluacionPage(string correl)
        {
            InitializeComponent();
            this.correl = correl;
        }

        private async void btnenviarevaluacion_Clicked(object sender, EventArgs e)
        {
            if (nota1 > 0 && nota2 > 0 && nota3 > 0 && nota4 > 0)
            {
                SaveEvaluacionModel save = new SaveEvaluacionModel
                {
                    ID_Cliente = correo,
                    Correl = correl,
                    Calif1 = nota1.ToString(),
                    Calif2 = nota2.ToString(),
                    Calif3 = nota3.ToString(),
                    Calif4 = nota4.ToString()
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
                    await DisplayAlert("Success", "Formulario Enviado", "Ok");
                }
                else
                {
                    await DisplayAlert("Error", "Estamos en mantenimiento", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Margarita App", "Debe responder todas las preguntas", "Ok");
            }
        }

    }
}