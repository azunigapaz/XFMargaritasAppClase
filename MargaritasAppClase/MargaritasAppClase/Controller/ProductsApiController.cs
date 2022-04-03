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
        //Object objSitioGlobal = null;
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

        public async static Task<ClientesModel> ControllerGetUser(string correo)
        {
            var clientesModel = new ClientesModel();

            using (HttpClient cliente = new HttpClient())
            {
                var respuesta = await cliente.GetAsync("https://webfacturacesar.000webhostapp.com/Margarita/methods/cliente/" + correo);

                if (respuesta.IsSuccessStatusCode)
                {
                    string contenido = respuesta.Content.ReadAsStringAsync().Result;
                    clientesModel = JsonConvert.DeserializeObject<ClientesModel>(contenido);
                }
            }

            return await Task.FromResult(clientesModel);
        }


        public async static Task<List<CarritoListModel>> ControllerObtenerListaCarrito(string correo)
        {
            List<CarritoListModel> listacarrito = new List<CarritoListModel>();

            using (HttpClient cliente = new HttpClient())
            {
                var respuesta = await cliente.GetAsync("https://webfacturacesar.000webhostapp.com/Margarita/methods/carrito/getItemsCarrito.php?" + correo);

                if (respuesta.IsSuccessStatusCode)
                {
                    string contenido = respuesta.Content.ReadAsStringAsync().Result.ToString();

                    dynamic dyn = JsonConvert.DeserializeObject(contenido);
                    byte[] newBytes = null;


                    if (contenido.Length > 28)
                    {

                        foreach (var item in dyn.Carrito)
                        {
                            string idCarrito = item.ID_Carrito.ToString(), idCliente = item.ID_Cliente.ToString(), fecha = item.fecha.ToString(), subTotal = item.SubTotal.ToString(), isv = item.ISV.ToString(), total = item.Total.ToString();

                            foreach (var itemdetalle in item.DetalleCarrito)
                            {
                                string idCarritoDetalle = itemdetalle.ID_CarritoDet.ToString(), fkIdCarrito = itemdetalle.FK_ID_Carrito.ToString(), descProducto = itemdetalle.desc_prod.ToString(), img64 = itemdetalle.foto.ToString();

                                newBytes = Convert.FromBase64String(img64);
                                var stream = new MemoryStream(newBytes);


                                listacarrito.Add(new CarritoListModel(
                                                idCarrito,
                                                idCliente,
                                                fecha,
                                                subTotal,
                                                isv,
                                                total,
                                                idCarritoDetalle,
                                                fkIdCarrito,
                                                descProducto, 
                                                img64,
                                                ImageSource.FromStream(() => stream)
                                                ));

                            }

                        }
                    }
                }
            }
            return listacarrito;
        }

    }
}
