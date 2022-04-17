using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using Plugin.Geolocator;
using System.Diagnostics;
using static Xamarin.Essentials.Permissions;
using Xamarin.Essentials;

namespace MargaritasAppClase.Views.EntregadorMenu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapaEntregadorPage : ContentPage
    {
        public MapaEntregadorPage()
        {            
            InitializeComponent();            
          
        }

        protected async override void OnAppearing() 
        {
            base.OnAppearing();
            double Latitud = Convert.ToDouble("15.88555");
            double Longitud = Convert.ToDouble("-88.025441");

            Pin pin = new Pin 
            {
                Label = "Ubicacion del pedido",
                Type = PinType.Place,
                Position = new Position(Latitud, Longitud)
            };

            MapaEntregador.Pins.Add(pin);

            var location = await Geolocation.GetLocationAsync();

            if (location == null)
            {
                location = await Geolocation.GetLastKnownLocationAsync();
            }

            MapaEntregador.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Latitud, Longitud), Distance.FromMeters(200)));

            var localizacion = CrossGeolocator.Current;

            if (localizacion != null)
            {
                localizacion.PositionChanged += Localizacion_PositionChanged;

                if (!localizacion.IsListening)
                {
                    Debug.WriteLine("StartListeningAsync");
                    await localizacion.StartListeningAsync(TimeSpan.FromMinutes(5), 100);
                }

                var posicion = await localizacion.GetPositionAsync();
                var mapac = new Position(Latitud, Longitud);
                MapaEntregador.MoveToRegion(MapSpan.FromCenterAndRadius(mapac, Distance.FromMeters(200)));

            }
            else 
            {
                await localizacion.GetLastKnownLocationAsync();
                var posicion = await localizacion.GetPositionAsync();
                var mapac = new Position(Latitud, Longitud);
                MapaEntregador.MoveToRegion(new MapSpan(mapac,2,2));
            }           
        }

        private void Localizacion_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {

            double Latituda = Convert.ToDouble("15.88555");
            double Longituda = Convert.ToDouble("-88.025441");
            var mapac = new Position(Latituda, Longituda);
            MapaEntregador.MoveToRegion(new MapSpan(mapac, 2, 2));

        }




    }
}