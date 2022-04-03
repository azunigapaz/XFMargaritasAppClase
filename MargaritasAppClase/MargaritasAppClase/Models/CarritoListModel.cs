﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MargaritasAppClase.Models
{
    public class CarritoListModel
    {
        public CarritoListModel(string ID_Carrito, string ID_Cliente, string fecha, string SubTotal, string ISV, string Total, string ID_CarritoDet, string FK_ID_Carrito, string desc_prod, string foto, ImageSource fotografia)
        {
            this.ID_Carrito = ID_Carrito;
            this.ID_Cliente = ID_Cliente;
            this.fecha = fecha;
            this.SubTotal = SubTotal;
            this.ISV = ISV;
            this.Total = Total;
            this.ID_CarritoDet = ID_CarritoDet;
            this.FK_ID_Carrito = FK_ID_Carrito;
            this.desc_prod = desc_prod;
            this.foto = foto;
            this.fotografia = fotografia;
        }

        public string ID_Carrito { get; set; }
        public string ID_Cliente { get; set; }
        public string fecha { get; set; }
        public string SubTotal { get; set; }
        public string ISV { get; set; }
        public string Total { get; set; }
        public string ID_CarritoDet { get; set; }
        public string FK_ID_Carrito { get; set; }
        public string desc_prod { get; set; }
        public string foto { get; set; }
        public ImageSource fotografia { get; set; }
    }
}
