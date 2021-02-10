using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class Messages //norhwind ile ilgili mesajları verdiğimiz class. Her projede ürün yoktur onun için bu klasörü core a oluşturmadık. Evransel değil northwind e özgü.
    {
        public static string ProductAdded = "Ürün eklendi"; //Public ler büyük harfle başlar (pascal case)
        public static string ProductNameInvalid = "Ürün ismi geçersiz";
        public static string MaintenanceTime = "Sistem bakımda";
        public static string ProductsListed = "Ürünler listelendi";
    }
}
