using MySql.Data;
using MySql.Data.MySqlClient;

using BusinessEntities;
using BusinessLogic;
using System.Collections;

namespace MySqlAccess
{
    class MySqlAccess
    {


        static string connStr = "server=localhost;user=root;port=3306;password=";

        /*
        this call will represent CRUD operation
        CRUD stands for Create,Read,Update,Delete
        */
        public static void createTables()
        {

            try
            {
                MySqlConnection conn = new MySqlConnection(connStr);
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();


                string sql = "DROP DATABASE IF EXISTS ice_cream_store;";

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                // create ice_cream_store
                sql = "CREATE DATABASE IF NOT EXISTS ice_cream_store;";
                cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                // create Tastes
                sql = "CREATE TABLE `ice_cream_store`.`Tastes` ( " +
                      "`tid` INTEGER NOT NULL AUTO_INCREMENT primary key," +
                      "`name`  VARCHAR(20) NOT NULL," +
                      "UNIQUE (name));";


                cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                   // create Toppings
                sql = "CREATE TABLE `ice_cream_store`.`Toppings` ( " +
                      "`topid` INTEGER NOT NULL AUTO_INCREMENT primary key," +
                      "`name`  VARCHAR(20) NOT NULL," +
                      "UNIQUE (name));";


                cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();


                // create Receptacles
                sql = "CREATE TABLE  `ice_cream_store`.`Receptacles` (" +
                    "	`rid` INTEGER NOT NULL AUTO_INCREMENT primary key," +
                    "	`name`  VARCHAR(20) NOT NULL," +
                    "	`price` int NOT NULL," +
                    "    UNIQUE(name)" +
                    ");";


                cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                // create `Sales`
                sql = "CREATE TABLE `ice_cream_store`.`Sales` (" +
                    "	`sid` INTEGER NOT NULL AUTO_INCREMENT primary key, " +
                    "   `rid` integer not null," +
                    "    foreign key(`rid`) references Receptacles(`rid`)," +
                    "	`datetime` datetime not null," +
                    "     `completed` bool not null," +
                    "    `paid` bool not null," +
                    "`total_price` INTEGER NOT NULL" +
                    ");";

                cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                // create connection Sales - Tastes
                sql = "CREATE TABLE `ice_cream_store`.`Tastes_Sales` ( " +
                    "	`sid` integer not null,	" +
                    "foreign key(`sid`) references Sales(`sid`),	" +
                    "`tid` integer not null," +
                    "	foreign key(`tid`) references Tastes(`tid`)," +
                    "    PRIMARY KEY(`sid` , `tid`),  " +
                    "  `quantity` integer not null" +
                    "); ";

                cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                // create connection Toppings_Sales  
                sql = "CREATE TABLE `ice_cream_store`.`Toppings_Sales` ( " +
                    "	`sid` integer not null,	" +
                    "foreign key(`sid`) references Sales(`sid`),	" +
                    "`topid` integer not null," +
                    "	foreign key(`topid`) references Toppings(`topid`)," +
                    "    PRIMARY KEY(`sid` , `topid`)  " +
                    "); ";

                cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                conn.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static void insertObject(Object obj)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(connStr);
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                string sql = null;

                if (obj is Taste)
                {
                    Taste taste = (Taste)obj;
                    sql = "INSERT INTO `ice_cream_store`.`Tastes` (`name`) " +
                    "VALUES ('" + taste.getName() + "');";
                }

                if (obj is Topping)
                {
                    Topping topping = (Topping)obj;
                    sql = "INSERT INTO `ice_cream_store`.`Toppings` (`name`) " +
                    "VALUES ('" + topping.getName() + "');";
                }

                if (obj is Receptacle)
                {
                    Receptacle receptacle = (Receptacle)obj;
                    sql = "INSERT INTO `ice_cream_store`.`Receptacles` (`Name`, `price`) " +
                    "VALUES ('" + receptacle.getName() + "', '" + receptacle.getPrice() + "');";
                }

                if (obj is Taste_Sale)
                {
                    Taste_Sale taste_sale = (Taste_Sale)obj;
                    sql = "INSERT INTO `ice_cream_store`.`Tastes_Sales` (`sid`, `tid`, `quantity`) " +
                    "VALUES ('" + taste_sale.getSaleId() + "', '" + taste_sale.getTasteId() + "', '" + taste_sale.getQuantity() + "');";
                }

                if (obj is Topping_Sale)
                {
                    Topping_Sale topping_sale = (Topping_Sale)obj;
                    sql = "INSERT INTO `ice_cream_store`.`Toppings_Sales` (`sid`, `topid`) " +
                    "VALUES ('" + topping_sale.getSaleId() + "', '" + topping_sale.getToppingId() + "');";
                }

                if (obj is Sale)
                {
                    Sale sale = (Sale)obj;
                    sql = "INSERT INTO `ice_cream_store`.`Sales` (`rid`, `datetime`, `completed`, `paid`, `total_price`) " +
                    "VALUES ('" + sale.getrid()  + "', '" +
                     sale.getDateTime().ToString() + "', " + sale.getCompleted() + ", " + sale.getPaid() + ", '" + sale.getTotalPrice() +"');";
                }

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public static ArrayList readAll(string tableName)
        {
            ArrayList all = new ArrayList();

            try
            {
                MySqlConnection conn = new MySqlConnection(connStr);
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                string sql = "SELECT * FROM `ice_cream_store`." + tableName;
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    //Console.WriteLine(rdr[0] + " -- " + rdr[1]);
                    Object[] numb = new Object[rdr.FieldCount];
                    rdr.GetValues(numb);
                    //Array.ForEach(numb, Console.WriteLine);
                    all.Add(numb);
                }
                rdr.Close();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return all;
        }
    }

}