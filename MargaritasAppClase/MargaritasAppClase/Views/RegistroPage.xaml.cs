using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace MargaritasAppClase.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistroPage : ContentPage
    {
        public string AudioPath, fileName;
        public RegistroPage()
        {
            InitializeComponent();
            TapGestureRecognizer_Tapped();
        }

        byte[] imageToSave, audioToSave;

        private async void btntomarphoto_Clicked(object sender, EventArgs e)
        {
            try
            {
                var takepic = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "PhotoApp",
                    Name = DateTime.Now.ToString() + "_Pic.jpg",
                    SaveToAlbum = true,
                    CompressionQuality = 40
                });

                await DisplayAlert("Ubicacion de la foto: ", takepic.Path, "Ok");

                if (takepic != null)
                {
                    imageToSave = null;
                    MemoryStream memoryStream = new MemoryStream();

                    takepic.GetStream().CopyTo(memoryStream);
                    imageToSave = memoryStream.ToArray();

                    registroimg.Source = ImageSource.FromStream(() => { return takepic.GetStream(); });
                }
                
                //descripcion_entry.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async void btnregistrarusuario_Clicked(object sender, EventArgs e)
        {

            try
            {
                if (String.IsNullOrEmpty(nombreregistro_input.Text) && String.IsNullOrEmpty(apellidoregistro_input.Text) && String.IsNullOrEmpty(correoregistro_input.Text) && String.IsNullOrEmpty(telefonoregistro_input.Text) && String.IsNullOrEmpty(password_input.Text) && String.IsNullOrEmpty(confirmarpassword_input.Text))
                {
                    await DisplayAlert("Campo Vacio", "Por favor, Complete los campos requeridos ", "Ok");
                }
                else
                {
                    //convertir la imagen a base64
                    string pathBase64Imagen = Convert.ToBase64String(imageToSave);

                    //extraer el path del audio
                    //string audio = AudioPath;
                    //convertir a arreglo de bytes
                    //byte[] fileByte = System.IO.File.ReadAllBytes(audio);
                    //convertir el audio a base64
                    //string pathBase64Audio = Convert.ToBase64String(fileByte);

                    ClientesModel save = new ClientesModel
                    {
                        ID_Cliente = "0",
                        Nombre = nombreregistro_input.Text,
                        Apellido = apellidoregistro_input.Text,
                        Correo = correoregistro_input.Text,
                        Contrasena = password_input.Text,
                        FechaNac = "0",
                        FechaCrea = "0",
                        Telefono = telefonoregistro_input.Text,
                        Foto = pathBase64Imagen,
                        Estado = "1",
                        TipoUsuario = "1",
                    };

                    Uri RequestUri = new Uri("https://webfacturacesar.000webhostapp.com/Margarita/methods/cliente/add.php");

                    var client = new HttpClient();
                    var json = JsonConvert.SerializeObject(save);
                    var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(RequestUri, contentJson);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        String jsonx = response.Content.ReadAsStringAsync().Result;

                        JObject jsons = JObject.Parse(jsonx);

                        String Mensaje = jsons["msg"].ToString();

                        await DisplayAlert("Success", "Datos guardados correctamente", "Ok");

                        nombreregistro_input.Text = "";
                        apellidoregistro_input.Text = "";
                        correoregistro_input.Text = "";
                        telefonoregistro_input.Text = "";
                        password_input.Text = "";
                        confirmarpassword_input.Text = "";
                        imageToSave = null;
                        registroimg.Source = null;
                        nombreregistro_input.Focus();

                    }
                    else
                    {
                        await DisplayAlert("Error", "Estamos en mantenimiento", "Ok");
                    }

                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
            }


            //await Navigation.PushAsync(new Views.ConfirmarUsuarioPage());
        }

        private void TapGestureRecognizer_Tapped()
        {
            lbl_loginpage.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    Navigation.PushAsync(new Views.LoginPage());
                })
            });
        }
    }
}