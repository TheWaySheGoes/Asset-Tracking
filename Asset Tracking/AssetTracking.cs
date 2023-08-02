using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Asset_Tracking
{
    internal class AssetTracking
    {
        //List of tracking Items in the Database
        List<Item> items = new List<Item>{new Computer("HP", "Elitebook", "Spain", DateTime.Now, decimal.Parse("123,678"), "EUR"), new Phone("HP", "Elitebook", "Spain", DateTime.Now, decimal.Parse("123,678"), "EUR") };




    }
}
