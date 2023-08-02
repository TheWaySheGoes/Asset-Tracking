using Microsoft.VisualBasic;
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
        List<Item> items = new List<Item>{new Computer("HP", "Elitebook", "USA", DateTime.Now.Date, decimal.Parse("123,678")), new Phone("HP", "Elitebook", "Spain", DateTime.Now.Date, decimal.Parse("123,678")) };
        static int paddingSize = 12;
        String exitOrProductMsg = "To enter a new product - follow the steps | To quit - enter: 'Q'";
        String exitOrProductOrSearchMsg = "To enter a new product enter 'P' | To search for product enter 'S' | To quit - enter: 'Q'";
        String categoryMsg = "Enter Category: ";
        String productMsg = "Enter Product: ";
        String priceMsg = "Enter Price: ";
        String priceWholeNumberMsg = "Price must be a number ex. 1234,5";
        String tableHeader = "------------------------------------------------------------";
        String tableCategories = "Type".PadRight(paddingSize) + "Brand".PadRight(paddingSize) + "Model".PadRight(paddingSize) + "Office".PadRight(paddingSize) + "Purchased".PadRight(paddingSize) + 
            "Price USD".PadRight(paddingSize) + "Currency".PadRight(paddingSize) + "Local Price".PadRight(paddingSize);
        String tableFooter = "------------------------------------------------------------";
        bool exitProductLoop = false;
        bool exitMainLoop = false;
        bool exitSearchLoop = false;
        LoopType loopType = LoopType.SHOW_PRODUCT_TABLE;
        String showProductsKeyWord = "P";
        String quitKeyWord = "Q";
        String searchKeyWord = "S";


        /*
         * Main logic, throwing the execution to specific loop
         */
        public void mainLoop()
        {
            while (!exitMainLoop)
            {
                switch (loopType)
                {
                    case LoopType.SEARCH:
                        {
                            searchLoop();
                            break;
                        }
                    case LoopType.PRODUCTS:
                        {
                            addProductLoop();
                            break;
                        }
                    case LoopType.SHOW_PRODUCT_TABLE:
                        {
                            showProductTable();
                            break;
                        }
                }
            }
        }

        /*
   * Generates a table with all of the products in the product list, sorted by price, with sum 
   * 
   */
        private void showProductTable()
        {
            printMsg(tableHeader);
            printMsg(tableCategories, Color.GREEN);
            items.OrderBy(item => item.Office).ToList().OrderBy(item => item.Purchased).ToList().ForEach(item => printMsg(item.toString()));   //TODO color code for dates
            //products.OrderBy(product => product.Price).ToList().ForEach(product => printMsg(product.toString()));
            printMsg(tableFooter);
            //Console.WriteLine(products.Sum(product => product.Price).ToString().PadLeft(60));
            printMsg(exitOrProductOrSearchMsg, Color.BLUE);
            String input = Console.ReadLine();

            if (input == showProductsKeyWord)
            {
                loopType = LoopType.PRODUCTS;
            }
            else if (input == searchKeyWord)
            {
                loopType = LoopType.SEARCH;
            }
            else if (input == quitKeyWord)
            {
                exitMainLoop = true;
            }
            //TODO exit
            else { }
        }

        /*
   * Adds a product to the list, exits and shows products table otherwise
   */
        private void addProductLoop()
        {
            while (!exitProductLoop)
            {
                String category = null;
                String product = null;
                decimal price = 0;

                //add category or exit
                printMsg(exitOrProductMsg, Color.YELLOW);
                Console.Write(categoryMsg);
                String input = Console.ReadLine().Trim();
                if (isExitProductsLoop(input, quitKeyWord))
                {
                    exitProductLoop = true;
                }
                else if (!exitProductLoop)
                {
                    category = input;
                }

                //add product name or exit
                if (!exitProductLoop)
                {
                    Console.Write(productMsg);
                    input = Console.ReadLine().Trim();
                    if (isExitProductsLoop(input, quitKeyWord))
                    {
                        exitProductLoop = true;
                    }
                    else if (!exitProductLoop)
                    {
                        product = input;

                    }
                }

                //add price or exit
                if (!exitProductLoop)
                {
                    Console.Write(priceMsg);
                    input = Console.ReadLine().Trim();
                    if (isExitProductsLoop(input, quitKeyWord))
                    {
                        exitProductLoop = true;
                    }
                    else if (!exitProductLoop)
                    {
                        while (!decimal.TryParse(input, out decimal number))
                        {
                            printMsg(priceWholeNumberMsg, Color.RED);
                            input = Console.ReadLine().Trim();
                            if (isExitProductsLoop(input, quitKeyWord))
                            {
                                exitProductLoop = true;
                                break;
                            }
                        }
                        if (!exitProductLoop)
                        {
                            price = decimal.Parse(input);
                        }
                    }
                }
                /*
                if (!exitProductLoop)
                {
                    Product tempProduct = new Product();
                    tempProduct.Category = category;
                    tempProduct.ProductName = product;
                    tempProduct.Price = price;
                    products.Add(tempProduct);
                }*/

            }
            exitProductLoop = false;
            loopType = LoopType.SHOW_PRODUCT_TABLE;
        }



        /*
         * Checks if exitCommand is typed in console input
         */
        private bool isExitProductsLoop(String msg, String exitCommand)
        {
            if (msg == exitCommand)
            {
                return true;
            }
            return false;
        }

        /*
         * Changes console output foreground color to one of pre defined Enums. 
         * After that goes back to default white.
         * 
         */
        private void printMsg(String msg, Color color = Color.WHITE)
        {
            switch (color)
            {
                case Color.WHITE:
                    Console.WriteLine(msg);
                    break;
                case Color.GREEN:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(msg);
                    break;
                case Color.BLUE:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(msg);
                    break;
                case Color.YELLOW:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(msg);
                    break;
                case Color.CYAN:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(msg);
                    break;
                case Color.RED:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(msg);
                    break;
            }

            if (Console.ForegroundColor != ConsoleColor.White)
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
        }


        /*
         * shows the product table highlighting the searched product
         */
        private void searchLoop()
        {
            while (!exitSearchLoop)
            {
                Console.Write(productMsg);
                String input = Console.ReadLine();

                printMsg(tableHeader);
                printMsg(tableCategories, Color.GREEN);
                /*products.OrderBy(product => product.Price).ToList().ForEach(product =>
                {
                    if (product.ProductName == input)
                    {
                        printMsg(product.toString(), Color.CYAN);
                    }
                    else
                    {
                        printMsg(product.toString());
                    }
                });*/
                printMsg(tableFooter);
                printMsg(exitOrProductOrSearchMsg, Color.BLUE);
                input = Console.ReadLine();

                if (input == showProductsKeyWord)
                {
                    loopType = LoopType.PRODUCTS;
                    exitSearchLoop = true;
                }
                else if (input == searchKeyWord)
                {
                    loopType = LoopType.SEARCH;
                    exitSearchLoop = false;
                }
                else if (input == quitKeyWord)
                {
                    exitSearchLoop = true;
                    exitMainLoop = true;
                }
                //TODO exit
                else { }
            }
            exitSearchLoop = false;
        }
    }
}

enum Color
{
    WHITE,
    BLUE,
    GREEN,
    YELLOW,
    CYAN,
    RED
}

enum LoopType
{
    SEARCH,
    PRODUCTS,
    SHOW_PRODUCT_TABLE
}

