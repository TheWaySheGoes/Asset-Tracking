using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Asset_Tracking
{
    abstract class Item
    {
        //EUR, SEK
        Decimal[] currencyConverters = new Decimal[] { 10.67m, 0.91m };
        internal int paddingSize = 12;
        internal String Type { get; set; }
        internal String Brand { get; set; }
        internal String Model { get; set; }
        internal String Office { get; set; }
        internal DateTime Purchased { get; set; }
        internal Decimal PriceUSD { get; set; }
        internal String Currency { get; set; }
        internal Decimal LocalPrice { get; set; }

        protected Item() { }
        protected Item(string brand, string model, string office, DateTime purchased, Decimal priceUSD)
        {
            Brand = brand;
            Model = model;
            Office = office;
            Purchased = purchased;
            PriceUSD = priceUSD;
            Currency = setCurrency(office);
            LocalPrice = convertToLocalPrice(priceUSD, Currency);
        }

        /*
         *Sets Currency based on the office the Item is in.
         */
        private String setCurrency(String office)
        {
            String outCurrency = "";

            switch (office)
            {
                case "Spain":
                    outCurrency = "EUR";
                    break;
                case "Sweden":
                    outCurrency = "SEK";
                    break;
                case "USA":
                    outCurrency = "USD";
                    break;
            }
            return outCurrency;
        }

        /*
         * Set the value of the price based on the currency
         */
        private Decimal convertToLocalPrice(decimal priceUSD, String currency)
        {
            Decimal outPrice = 0.0m;

            switch (Currency)
            {
                case "EUR":
                    outPrice = priceUSD * currencyConverters[0];
                    break;
                case "SEK":
                    outPrice = priceUSD * currencyConverters[1];
                    break;
                case "USD":
                    outPrice = PriceUSD;
                    break;
            }

            return outPrice;
        }

        /*
         * shows informations about the specific object. Overided by the object
         */
        abstract public String toString();


    }
}
