using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asset_Tracking
{
    internal class Phone : Item
    {
        public Phone() { }
        public Phone(String brand, String model, String office, DateTime purchased, Decimal priceUSD) : base(brand, model, office, purchased, priceUSD)
        {
            Type = "Phone";
        }
        public override string toString()
        {
            return Type.PadRight(paddingSize) + Brand.PadRight(paddingSize) + Model.PadRight(paddingSize) + Office.PadRight(paddingSize) + Purchased.Date.ToString("dd-MM-yyyy").PadRight(paddingSize) +
                PriceUSD.ToString().PadRight(paddingSize) + Currency.PadRight(paddingSize) + LocalPrice.ToString().PadRight(paddingSize);
        }
    }
}
