using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asset_Tracking
{
    internal class Phone : Item
    {
        public Phone(String brand, String model, String office, DateTime purchased, Decimal priceUSD, String currency) : base(brand, model, office, purchased, priceUSD, currency)
        {
            Type = this.GetType().ToString();
        }
    }
}
