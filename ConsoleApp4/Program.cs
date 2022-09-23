using System;
using System.Data;
using System.Diagnostics;//used for Stopwatch class

using MySql.Data;
using MySql.Data.MySqlClient;

using MySqlAccess;
using BusinessLogic;
using System.Collections;
using BusinessEntities;

using MongoAccess;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("The current time is " + DateTime.Now);

// let user input choose between mongo and sql
Console.WriteLine("Please choose between mongo and sql "
+"(write mongo for mongo sql for mysql");
string userChoice = Console.ReadLine();

// if user chose mongo
if (userChoice == "mongo")
{
    int userInput = 0;
    do
    {
        Console.WriteLine("_____________________");
        Console.WriteLine("Please chose a task:");
        Console.WriteLine("1. fill mongo with data");
        Console.WriteLine("2. Get all the sales");
        Console.WriteLine("3. Get all the details of a specific Sale");
        // make a sale
        Console.WriteLine("4. Make a sale");
        Console.WriteLine("5. Get all the sales of a specific date and the report of the day");
        // show not completed sales
        Console.WriteLine("6. Show not completed sales");
        // edit sale
        Console.WriteLine("7. Edit sale");
        // delete sale
        Console.WriteLine("8. Delete sale");
        // read sale
        Console.WriteLine("9. Read sale");
        // most common ingredient
        Console.WriteLine("10. Get the most common ingredient");
        // exit
        Console.WriteLine("(-1)  Exit");

        userInput = Convert.ToInt32(Console.ReadLine());

        switch (userInput)
        {
            case 1:
                // before filling check if the collection is empty
                if (MongoAccess.MongoAccess.checkIfCollectionIsEmpty())
                {
                    // fill the collection with data
                    Console.WriteLine("fill mongo with data");
                    Logic.fill_Receptacles_to_mongo();
                    Logic.fill_Toppings_to_mongo();
                    Logic.fill_Tastes_to_mongo();
                }
                break;
            case 2:
                // get all the sales
                Console.WriteLine("_____________________");
                Console.WriteLine("Get all the sales");
                Console.WriteLine("_____________________");
                // get all the sales from mongo
                MongoAccess.MongoAccess.getAllSales();

                break;
            case 3:
                // get all the details of a specific sale
                Console.WriteLine("_____________________");
                Console.WriteLine("Get all the details of a specific sale");
                Console.WriteLine("_____________________");
                // get the id of the sale
                Console.WriteLine("Please enter the id of the sale");
                string id = Console.ReadLine();
                // get the sale from mongo
                MongoAccess.MongoAccess.getSale(id);
                break;
            case 4:
                // make a sale
                Console.WriteLine("_____________________");
                Console.WriteLine("Make a sale");
                Console.WriteLine("_____________________");
                Logic.MongoMakeOrder();
                break;
            case 5:
                // get all the sales of a specific date and the report of the day
                Console.WriteLine("_____________________");
                Console.WriteLine("Get all the sales of a specific date and the report of the day");
                Console.WriteLine("_____________________");
                // get the date
                Console.WriteLine("Please enter the date");
                string date = Console.ReadLine();
                // get the sales from mongo
                MongoAccess.MongoAccess.getSalesOfSpecificDate(date);
                break;
            case 6:
                // show not completed sales
                Console.WriteLine("_____________________");
                Console.WriteLine("Show not completed sales");
                Console.WriteLine("_____________________");
                // get the sales from mongo
                MongoAccess.MongoAccess.getNotCompletedSales();
                break;
            case 7:
                // edit sale
                Console.WriteLine("_____________________");
                Console.WriteLine("Edit sale");
                Console.WriteLine("_____________________");
                // get the id of the sale
                Console.WriteLine("Please enter the id of the sale");
                string idEdit = Console.ReadLine();
                // get the sale from mongo
                MongoAccess.MongoAccess.getSale(idEdit);
                // edit the sale
                Logic.MongoEditOrder(idEdit);
                break;
            case 8:
                // delete sale
                Console.WriteLine("_____________________");
                Console.WriteLine("Delete sale");
                Console.WriteLine("_____________________");
                // get the id of the sale
                Console.WriteLine("Please enter the id of the sale");
                string idDelete = Console.ReadLine();
                // get the sale from mongo
                MongoAccess.MongoAccess.getSale(idDelete);
                // delete the sale
                MongoAccess.MongoAccess.deleteSale(idDelete);
                break;
            case 9:
                // read sale
                Console.WriteLine("_____________________");
                Console.WriteLine("Read sale");
                Console.WriteLine("_____________________");
                // get the id of the sale
                Console.WriteLine("Please enter the id of the sale");
                string idRead = Console.ReadLine();
                // get the sale from mongo
                MongoAccess.MongoAccess.getSale(idRead);
                break;
            case 10:
                // most common ingredient
                Console.WriteLine("_____________________");
                Console.WriteLine("Get the most common ingredient");
                Console.WriteLine("_____________________");
                // get the sales from mongo
                MongoAccess.MongoAccess.getMostCommonIngredient();
                break;


        }



    } while (userInput != -1);
}// if user chose sql 
else if (userChoice == "sql")
{
    int userInput = 0;
    do
    {
        Console.WriteLine("_____________________");
        Console.WriteLine("Please chose a task:");
        Console.WriteLine("1 - create empty tables");
        Console.WriteLine("2 - fill tables with data");
        Console.WriteLine("3 - print values of a table");
        Console.WriteLine("4 - Make an order");
        Console.WriteLine("5 - Delete an order");
        Console.WriteLine("6 - Edit an order");
        Console.WriteLine("7 - Read an order");
        Console.WriteLine("8 - get day report for specific day ");
        Console.WriteLine("9 - show not completed sales ");
        Console.WriteLine("10 - most common ingredient");
        Console.WriteLine("(-1) - for exit");

        userInput = Int32.Parse(Console.ReadLine());

        switch (userInput)
        {
            case 1:
                BusinessLogic.Logic.createTables();
                break;
            case 2:
                Console.WriteLine("fill should be called");
                BusinessLogic.Logic.fillTables(100);
                break;
            case 3:
                Console.WriteLine("Enter table name (Tastes/Toppings/Receptacles/Sales/Tastes_Sales/Toppings_Sales)");
                string tableName = Console.ReadLine();
                ArrayList results = BusinessLogic.Logic.getTableData(tableName);
                foreach (Object obj in results)
                    Console.WriteLine("   {0}", obj);
                Console.WriteLine();
                break;
            case 4:
                BusinessLogic.Logic.makeOrder();
                break;
            case 5:
                Console.WriteLine("Enter order id");
                int id = Int32.Parse(Console.ReadLine());
                BusinessLogic.Logic.deleteOrder(id);
                break;
            case 6:
                Console.WriteLine("Enter order id");
                int id1 = Int32.Parse(Console.ReadLine());
                BusinessLogic.Logic.EditOrder(id1);
                break;
            case 7:
                Console.WriteLine("Enter order id");
                int id2 = Int32.Parse(Console.ReadLine());
                BusinessLogic.Logic.ReadOrder(id2);
                break;
            case 8:
                BusinessLogic.Logic.getDayReport();
                break;
            case 9:
                BusinessLogic.Logic.getNotCompletedSales();
                break;
            case 10:
                BusinessLogic.Logic.getMostCommonIngredient();
                break;
        }

    } while (userInput != -1);

    Console.WriteLine("Thank you for your time");
}


// Console.ReadKey();

/*
string connStr = "server=localhost;user=root;database=world;port=3306;password=1234";
MySqlConnection conn = new MySqlConnection(connStr);

try
{
    Console.WriteLine("Connecting to MySQL...");
    conn.Open();

    string sql = "SELECT * FROM countries WHERE name='Israel'";
    MySqlCommand cmd = new MySqlCommand(sql, conn);
    MySqlDataReader rdr = cmd.ExecuteReader();

    while (rdr.Read())
    {
        Console.WriteLine(rdr[0] + " -- " + rdr[1] + " -- " + rdr[2]);
    }
    rdr.Close();
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}

conn.Close();
Console.WriteLine("Done.");
*/

