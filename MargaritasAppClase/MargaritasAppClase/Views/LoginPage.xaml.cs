﻿using System;
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
using MargaritasAppClase.Views;
using System.IO;
using Plugin.LocalNotification;

namespace MargaritasAppClase.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {

        string pdCorreo = "", pdPass = "";
        public LoginPage()
        {
            InitializeComponent();
            TapRegistroPage_Tapped();
            TapForgotPassPage_Tapped();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (Application.Current.Properties.ContainsKey("correo"))
            {
                await Navigation.PushAsync(new Views.TabbedMenu.MainTabbedPage());
            }

        }

        private async void btniniciarsesion_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(correo_input.Text) && String.IsNullOrEmpty(password_input.Text))
            {
                await DisplayAlert("Campo Vacio", "Por favor, Ingrese un correo y una contraseña ", "Ok");
            }
            else
            {

                LoginModel Login = new LoginModel
                {
                    authmail = correo_input.Text,
                    authpass = password_input.Text,
                };

                Uri RequestUri = new Uri("https://webfacturacesar.000webhostapp.com/Margarita/methods/login/");

                var client = new HttpClient();
                var json = JsonConvert.SerializeObject(Login);
                var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(RequestUri, contentJson);


                if (response.StatusCode == HttpStatusCode.OK)
                {
                    String jsonx = response.Content.ReadAsStringAsync().Result;
                    JObject jsons = JObject.Parse(jsonx);
                    String Mensaje = jsons["success"].ToString();

                    //await DisplayAlert("Success", "Datos guardados correctamente", "Ok");
                    

                    if (Mensaje == "true")
                    {

                        pdCorreo = correo_input.Text;
                        pdPass = password_input.Text;

                        Application.Current.Properties["correo"] = pdCorreo;
                        Application.Current.Properties["pass"] = pdPass;
                        await Application.Current.SavePropertiesAsync();
                        
                        await Navigation.PushAsync(new Views.TabbedMenu.MainTabbedPage());
                        var notificacion = new NotificationRequest
                        {
                            BadgeNumber = 1,
                            Description = "Tenemos nuevas promociones para ti",
                            Title = "Te extrañamos",
                            ReturningData = "Dummy Data",
                            NotificationId = 1337,
                            
                        };
                        
                        await NotificationCenter.Current.Show(notificacion );
                        
                    }
                    else
                    {
                        await DisplayAlert("Error", "El usuario no existe", "Ok");
                        correo_input.Text = "";
                        password_input.Text = "";
                        correo_input.Focus();
                    }

                    //await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", "Estamos en mantenimiento", "Ok");
                }
            }
        }

        private void TapRegistroPage_Tapped() => lbl_registropage.GestureRecognizers.Add(new TapGestureRecognizer()
        {
            Command = new Command(() =>
            {
                Navigation.PushAsync(new Views.RegistroPage());
            })
        });

        private void TapForgotPassPage_Tapped() => lbl_forgotpasspage.GestureRecognizers.Add(new TapGestureRecognizer()
        {
            Command = new Command(() =>
            {
                Navigation.PushAsync(new Views.ForgotPassPage());
            })
        });

    }

}

