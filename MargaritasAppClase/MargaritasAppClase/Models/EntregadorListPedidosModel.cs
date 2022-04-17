using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MargaritasAppClase.Models
{
    public class EntregadorListPedidosModel
    {
        public EntregadorListPedidosModel(string id_pedido, string id_cliente, string fh_entrega, string id_ubicacion, string estado, string id_entregador, string latitudped, string longitudped)
        {
            this.id_pedido = id_pedido;
            this.id_cliente = id_cliente;
            this.fh_entrega = fh_entrega;
            this.id_ubicacion = id_ubicacion;
            this.estado = estado;
            this.id_entregador = id_entregador;
            this.latitudped = latitudped;
            this.longitudped = longitudped;
        }

        public string id_pedido { get; set; }
        public string id_cliente { get; set; }
        public string fh_entrega { get; set; }
        public string id_ubicacion { get; set; }
        public string estado { get; set; }
        public string id_entregador { get; set; }
        public string latitudped { get; set; }
        public string longitudped { get; set; }
    }
}
