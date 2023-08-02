using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asset_Tracking
{
    abstract class Item
    {

        internal String Type { get; set; }
        internal String Brand { get; set; }
        internal String Model { get; set; }
        internal String Office { get; set; }
        internal DateTime Purchased { get; set; }
        internal Decimal PriceUSD { get; set; }
        internal String Currency { get; set; }
        internal Decimal LocalPrice { get; set; }

        protected Item(string brand, string model, string office, DateTime purchased, Decimal priceUSD, string currency)
        {
            Brand = brand;
            Model = model;
            Office = office;
            Purchased = purchased;
            PriceUSD = priceUSD;
            Currency = currency;
            LocalPrice = convertToLocalPrice(priceUSD, currency);
        }

        private Decimal convertToLocalPrice(decimal priceUSD, String currency)
        {
            return decimal.Parse("12,34");
        }


    }
}
