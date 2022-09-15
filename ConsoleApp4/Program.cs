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
    Console.WriteLine("4 - Make an order")
    Console.WriteLine("");
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

