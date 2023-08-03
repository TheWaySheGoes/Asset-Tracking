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
        List<Item> items = new List<Item> { new Computer("HP", "Elitebook", "USA", DateTime.Now.Date, decimal.Parse("123,678")), new Phone("HP", "Elitebook", "Spain", DateTime.Now.Date, decimal.Parse("123,678")) };
        static int paddingSize = 12;
        String exitOrTypeMsg = "Enter Type\n - 1 for Computer\n - 2 for Phone\n - Q for Exit";
        String exitOrProductOrSearchMsg = "To enter a new product enter 'P' | To quit - enter: 'Q'";
        String categoryMsg = "Enter Category: ";
        String brandMsg = "Enter Brand: ";
        String dateMsg = "Enter purchase date in format dd-MM-yyyy: ";
        String officeMsg = "Enter office\n - 1 for Spain\n - 2 for Sweden\n - 3 for USA";
        String modelMsg = "Enter Model: ";
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
                Item type = null;
                String brand = null;
                String model = null;
                String office = null;
                DateTime purchased = new DateTime();
                decimal priceUSD = 0;
                String input = null;

                //add type or exit
                bool wrongInput = true;
                while (wrongInput)
                {
                    printMsg(exitOrTypeMsg, Color.YELLOW);
                    Console.Write(categoryMsg);
                    input = Console.ReadLine().Trim();
                    if (isExitProductsLoop(input, quitKeyWord))
                    {
                        exitProductLoop = true;
                    }
                    else if (!exitProductLoop)
                    {

                        switch (input)
                        {
                            case "1":
                                type = new Computer();
                                wrongInput = false;
                                break;
                            case "2":
                                type = new Phone();
                                wrongInput = false;
                                break;
                        }
                    }
                }
                //add brand name or exit
                if (!exitProductLoop)
                {
                    Console.Write(brandMsg);
                    input = Console.ReadLine().Trim();
                    if (isExitProductsLoop(input, quitKeyWord))
                    {
                        exitProductLoop = true;
                    }
                    else if (!exitProductLoop)
                    {
                        brand = input;

                    }
                }

                //add model name or exit
                if (!exitProductLoop)
                {
                    Console.Write(modelMsg);
                    input = Console.ReadLine().Trim();
                    if (isExitProductsLoop(input, quitKeyWord))
                    {
                        exitProductLoop = true;
                    }
                    else if (!exitProductLoop)
                    {
                        model = input;

                    }
                }

                //add office name or exit
                wrongInput = true;
                while (wrongInput)
                {
                    if (!exitProductLoop)
                    {
                        Console.Write(officeMsg);
                        input = Console.ReadLine().Trim();
                        if (isExitProductsLoop(input, quitKeyWord))
                        {
                            exitProductLoop = true;
                        }
                        else if (!exitProductLoop)
                        {
                            switch (input)
                            {
                                case "1":
                                    office = "Spain";
                                    wrongInput = false;
                                    break;
                                case "2":
                                    office = "Sweden";
                                    wrongInput = false;
                                    break;
                                case "3":
                                    office = "USA";
                                    wrongInput = false;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        wrongInput = false;
                    }
                }

                //add purchase date or exit
                if (!exitProductLoop)
                {
                    Console.Write(dateMsg);
                    input = Console.ReadLine().Trim();
                    if (isExitProductsLoop(input, quitKeyWord))
                    {
                        exitProductLoop = true;
                    }
                    else if (!exitProductLoop)
                    {
                        bool repeat = true;
                        while (repeat)
                        {
                            try
                            {
                                purchased = DateTime.Parse(input);
                            }
                            catch (ArgumentNullException e)
                            {
                                printMsg("Date cant be empty", Color.RED);
                            }
                            catch (FormatException e)
                            {
                                printMsg("Date has wrong format", Color.RED);
                            }
                            finally
                            {
                                repeat = false;
                            }
                        }
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
                        bool repeat = true;
                        while (repeat)
                        {
                            try
                            {
                                priceUSD = decimal.Parse(input);
                            }
                            catch (ArgumentNullException e)
                            {
                                printMsg("Price cant be empty", Color.RED);
                            }
                            catch (FormatException e)
                            {
                                printMsg("Price has wrong format", Color.RED);
                            }
                            catch (OverflowException e)
                            {
                                printMsg("Price is to big", Color.RED);
                            }
                            finally
                            {
                                repeat = false;
                            }
                        }
                    }
                }

                if (!exitProductLoop)
                {
                    type.Brand = brand;
                    type.Model = model;
                    type.Office = office;
                    type.Purchased = purchased;
                    type.PriceUSD = priceUSD;
                }

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
    PRODUCTS,
    SHOW_PRODUCT_TABLE
}

