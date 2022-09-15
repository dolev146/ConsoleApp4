using System;
using System.Data;
using System.Diagnostics;//used for Stopwatch class

using MySql.Data;
using MySql.Data.MySqlClient;

using MySqlAccess;
using BusinessLogic;
using System.Collections;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("The current time is " + DateTime.Now);

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
    }

} while (userInput != -1);

Console.WriteLine("Thank you for your time");
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

