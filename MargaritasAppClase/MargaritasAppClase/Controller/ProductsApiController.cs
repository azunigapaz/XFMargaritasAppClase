using System;
using System.Collections.Generic;
using System.Text;

using MargaritasAppClase.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;



namespace MargaritasAppClase.Controller
{
    public class ProductsApiController
    {

        public async static Task<List<ProductsListModel>> ControllerObtenerListaProductos()
        {
            List<ProductsListModel> listaproductos = new List<ProductsListModel>();

            using (HttpClient cliente = new HttpClient())
            {
                var respuesta = await cliente.GetAsync("https://webfacturacesar.000webhostapp.com/Margarita/methods/products/index.php");

                if (respuesta.IsSuccessStatusCode)
                {
                    string contenido = respuesta.Content.ReadAsStringAsync().Result.ToString();

                    dynamic dyn = JsonConvert.DeserializeObject(contenido);
                    byte[] newBytes = null;


                    if (contenido.Length > 28)
                    {

                        foreach (var item in dyn.items)
                        {
                            string img64 = item.Foto.ToString();
                            newBytes = Convert.FromBase64String(img64);
                            var stream = new MemoryStream(newBytes);

                            listaproductos.Add(new ProductsListModel(
                                            item.Id.ToString(), item.Descripcion.ToString(),
                                            item.Precio.ToString(),
                                            ImageSource.FromStream(() => stream),
                                            img64, item.Cantidad.ToString()
                                            ));
                        }
                    }
                }
            }
            return listaproductos;
        }

    }
}
