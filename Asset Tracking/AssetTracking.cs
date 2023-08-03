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
        List<Item> items = new List<Item> { new Computer("HP", "Elitebook", "USA", new DateTime(2020, 10, 2), decimal.Parse("123,678")),
                                            new Phone("HP", "Elitebook", "Spain", new DateTime(2020, 6, 1), decimal.Parse("123,678")),
                                            new Phone("iPhone", "8", "Spain", new DateTime(2018, 12, 29), decimal.Parse("970")),
                                            new Computer("HP", "Elitebook", "Spain", new DateTime(2019, 6, 1), decimal.Parse("1423")),
                                            new Phone("iPhone", "11", "Spain", new DateTime(2022, 6, 25), decimal.Parse("990")),
                                            new Phone("iPhone", "X", "Sweden", new DateTime(2018, 7, 15), decimal.Parse("1245")),
                                            new Phone("Motorola", "Razr", "Sweden", new DateTime(2022, 12, 16), decimal.Parse("970")),
                                            new Computer("HP", "Elitebook", "Sweden", new DateTime(2023, 10, 2), decimal.Parse("588")),
                                            new Computer("Asus", "W234", "USA", new DateTime(2017, 4, 21), decimal.Parse("1200")),
                                            new Computer("Lenovo", "Yoga 730", "USA", new DateTime(2018, 5, 28), decimal.Parse("835")),
                                            new Computer("Lenovo", "Yoga 530", "USA", new DateTime(2019, 5, 21), decimal.Parse("1030"))
                                            };
        static int paddingSize = 12;
        String exitOrTypeMsg = "Enter Type\n - 1 for Computer\n - 2 for Phone\n - Q for Exit";
        String exitOrProductOrSearchMsg = "To enter a new product enter 'P' | To quit - enter: 'Q' ";
        String categoryMsg = "Enter Category: ";
        String brandMsg = "Enter Brand | To quit - enter 'Q': ";
        String dateMsg = "Enter purchase date in format dd-MM-yyyy | To quit - enter: 'Q' ";
        String officeMsg = "Enter office\n - 1 for Spain\n - 2 for Sweden\n - 3 for USA\n - Q for Exit ";
        String modelMsg = "Enter Model | To quit - enter 'Q': ";
        String priceMsg = "Enter Price ex. 1234,5 | To quit - enter 'Q': ";
        String wrongChoiceMsg = "Wrong choice, try again:";
        String tableHeader = "-------------------------------------------------------------------------------------------------";
        String tableCategories = "Type".PadRight(paddingSize) + "Brand".PadRight(paddingSize) + "Model".PadRight(paddingSize) + "Office".PadRight(paddingSize) + "Purchased".PadRight(paddingSize) +
            "Price USD".PadRight(paddingSize) + "Currency".PadRight(paddingSize) + "Local Price".PadRight(paddingSize);
        String tableFooter = "-------------------------------------------------------------------------------------------------";
        bool exitProductLoop = false;
        bool exitMainLoop = false;
        LoopType loopType = LoopType.SHOW_PRODUCT_TABLE;
        String showProductsKeyWord = "P";
        String quitKeyWord = "Q";


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
         */
        private void showProductTable()
        {
            printMsg(tableHeader);
            printMsg(tableCategories, Color.GREEN);
            List<Item> tempList = items.OrderBy(item => item.Office).ThenBy(item => item.Purchased.Date).ToList();
            items = tempList;
            //background color according to timestamp vals
            foreach (Item item in items)
            {
                DateTime today = DateTime.Now.Date;
                DateTime threeYears = item.Purchased.Date.AddYears(3);
                TimeSpan timeSpan = today - threeYears;
                int difference = Convert.ToInt32(Math.Floor(timeSpan.TotalDays));
                if (difference <= 90 && difference > 0)
                {

                    printMsg(item.toString(), Color.YELLOW);
                }
                else if (difference > 90)
                {
                    printMsg(item.toString(), Color.RED);
                }

                else
                {
                    printMsg(item.toString());
                }
            }

            printMsg(tableFooter);
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
            else { }
        }

        /*
         * Adds a product to the list, exits and shows products table otherwise
         */
        private void addProductLoop()
        {
            while (!exitProductLoop)
            {
                String type = null;
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
                    printMsg(exitOrTypeMsg, Color.CYAN);
                    printMsg(categoryMsg, Color.WHITE);
                    input = Console.ReadLine().Trim();
                    if (isExitProductsLoop(input, quitKeyWord))
                    {
                        exitProductLoop = true;
                        wrongInput = false;
                    }
                    else if (!exitProductLoop)
                    {

                        switch (input)
                        {
                            case "1":
                                type = "Computer";
                                wrongInput = false;
                                break;
                            case "2":
                                type = "Phone";
                                wrongInput = false;
                                break;
                            default:
                                printMsg(wrongChoiceMsg, Color.RED);
                                break;
                        }
                    }
                }
                //add brand name or exit
                if (!exitProductLoop)
                {
                    printMsg(brandMsg, Color.CYAN, false);
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
                    printMsg(modelMsg, Color.CYAN, false);
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

                if (!exitProductLoop)
                {
                    wrongInput = true;
                    while (wrongInput)
                    {
                        printMsg(officeMsg, Color.CYAN);
                        printMsg(categoryMsg, Color.WHITE);
                        input = Console.ReadLine().Trim();
                        if (isExitProductsLoop(input, quitKeyWord))
                        {
                            exitProductLoop = true;
                            wrongInput = false;
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
                                default:
                                    printMsg(wrongChoiceMsg, Color.RED);
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    wrongInput = false;
                }


                //add purchase date or exit
                if (!exitProductLoop)
                {
                    wrongInput = true;
                    while (wrongInput)
                    {
                        printMsg(dateMsg, Color.CYAN, false);
                        input = Console.ReadLine().Trim();
                        if (isExitProductsLoop(input, quitKeyWord))
                        {
                            exitProductLoop = true;
                            wrongInput = false;
                        }
                        else if (!exitProductLoop)
                        {

                            try
                            {
                                purchased = DateTime.Parse(input);
                                wrongInput = false;
                            }
                            catch (ArgumentNullException)
                            {
                                printMsg("Date cant be empty", Color.RED);
                            }
                            catch (FormatException)
                            {
                                printMsg("Date has wrong format", Color.RED);
                            }
                        }
                    }
                }

                //add price or exit
                if (!exitProductLoop)
                {
                    wrongInput = true;
                    while (wrongInput)
                    {
                        printMsg(priceMsg, Color.CYAN, false);
                        input = Console.ReadLine().Trim();
                        if (isExitProductsLoop(input, quitKeyWord))
                        {
                            exitProductLoop = true;
                        }
                        else if (!exitProductLoop)
                        {

                            try
                            {
                                priceUSD = decimal.Parse(input);
                                wrongInput = false;
                            }
                            catch (ArgumentNullException)
                            {
                                printMsg("Price cant be empty", Color.RED);
                            }
                            catch (FormatException)
                            {
                                printMsg("Price has wrong format", Color.RED);
                            }
                            catch (OverflowException)
                            {
                                printMsg("Price is to big", Color.RED);
                            }
                        }
                    }
                }

                if (!exitProductLoop)
                {
                    if (type == "Computer")
                    {
                        items.Add(new Computer(brand, model, office, purchased, priceUSD));
                    }
                    else if (type == "Phone")
                    {
                        items.Add(new Phone(brand, model, office, purchased, priceUSD));
                    }
                    exitProductLoop = true;
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
        private void printMsg(String msg, Color color = Color.WHITE, bool newLine = true)
        {
            switch (color)
            {
                case Color.WHITE:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case Color.GREEN:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case Color.BLUE:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case Color.YELLOW:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case Color.CYAN:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case Color.RED:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
            }

            if (newLine)
            {
                Console.WriteLine(msg);
            }
            else
            {
                Console.Write(msg);
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

