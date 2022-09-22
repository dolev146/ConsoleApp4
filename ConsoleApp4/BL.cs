using System.Collections;
using System.Globalization;
using BusinessEntities;
using MongoDB.Bson;
using MongoDB.Driver;
using MySql.Data.MySqlClient;

namespace BusinessLogic
{
    public class RandomDateTime
    {
        DateTime start;
        Random gen;
        int range;

        public RandomDateTime()
        {
            start = new DateTime(1995, 1, 1);
            gen = new Random();
            range = (DateTime.Today - start).Days;
        }

        public DateTime Next()
        {
            return start.AddDays(gen.Next(range)).AddHours(gen.Next(0, 24)).AddMinutes(gen.Next(0, 60)).AddSeconds(gen.Next(0, 60));
        }
    }




    public class Logic
    {

        public static string[] colors = { "Yellow", "White", "Black", "Green", "Transparent" };
        public static string[] tasks = { "Service 10K", "Wheels", "BodyWork" };
        public static string[] desc = { "Replace filets of oil,gasoline,air contidioner", "Change 4 tires", "fix scratchs" };
        public static string[] cities = { "Jerusalem", "Tel Aviv", "Beeh Sheva", "Ariel" };
        // tastes = names
        public static string[] tastes = { "Vanil", "Chocolate", "Mekupelet", "Pistachio", "Cherry", "Halva", "Mango", "Mint", "Rum", "Strawberry", "Teaberry", "Tutti-Frutti", "Twist", "Watermelon", "Banana" };
        // receptacles = cars
        public static string[] receptacles_name = { "Regular_cone", "Special_cone", "Box" };
        public static int[] receptacles_price = { 0, 2, 5 };
        // toppings 
        public static string[] toppings = { "maple_syrup", "chocolate_syrup", "color_sparkles", "black_sparkls", "tchina", "cherry_syrup" };
        public static string[] toppings_for_vanil = { "chocolate_syrup", "color_sparkles", "black_sparkls", "tchina", "cherry_syrup" };
        public static string[] toppings_for_chocolate_mekupelet = { "maple_syrup", "color_sparkles", "black_sparkls", "tchina", "cherry_syrup" };
        public static string[] toppings_for_chocolate_mekupelet_vanila = { "color_sparkles", "black_sparkls", "tchina", "cherry_syrup" };

        

        public static void fill_Receptacles_to_mongo()
        {
            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://arielUni:arielUni123@cluster0.ayzwu1i.mongodb.net/?retryWrites=true&w=majority");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("ice_cream_store_mongo");
            var collection = database.GetCollection<BsonDocument>("receptacles");
            for (int i = 0; i < receptacles_name.Length; i++)
            {
                BsonDocument document = new BsonDocument
                {
                    { "name", receptacles_name[i] },
                    { "price", receptacles_price[i] }
                };
                collection.InsertOne(document);
            }

        }

        public static void fill_Toppings_to_mongo()
        {
            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://arielUni:arielUni123@cluster0.ayzwu1i.mongodb.net/?retryWrites=true&w=majority");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("ice_cream_store_mongo");
            var collection = database.GetCollection<BsonDocument>("toppings");
            for (int i = 0; i < toppings.Length; i++)
            {
                BsonDocument document = new BsonDocument
                {
                    { "name", toppings[i] }
                };
                collection.InsertOne(document);
            }
        }

        public static void fill_Tastes_to_mongo()
        {

            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://arielUni:arielUni123@cluster0.ayzwu1i.mongodb.net/?retryWrites=true&w=majority");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("ice_cream_store_mongo");
            var collection = database.GetCollection<BsonDocument>("tastes");
            for (int i = 0; i < tastes.Length; i++)
            {
                BsonDocument document = new BsonDocument
                {
                    { "name", tastes[i] }
                };
                collection.InsertOne(document);
            }

        }

        public static void MongoMakeOrder()
        {
            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://arielUni:arielUni123@cluster0.ayzwu1i.mongodb.net/?retryWrites=true&w=majority");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("ice_cream_store_mongo");
            var collection = database.GetCollection<BsonDocument>("sales");

            // ask what recepacle to use
            Console.WriteLine("Choose a recepacle: ");
            for (int i = 0; i < receptacles_name.Length; i++)
            {
                Console.WriteLine(i + 1 + ". " + receptacles_name[i]);
            }
            int recepacle = Convert.ToInt32(Console.ReadLine()) - 1;

            // make for loop to choose tastes and quantity Objects of each taste untill say done
            List<BsonDocument> tastes_list = new List<BsonDocument>();
            string answer = "yes";
            while (answer == "yes")
            {
                Console.WriteLine("Choose a taste: ");
                for (int i = 0; i < tastes.Length; i++)
                {
                    Console.WriteLine(i + 1 + ". " + tastes[i]);
                }
                int taste = Convert.ToInt32(Console.ReadLine()) - 1;
                Console.WriteLine("Choose quantity: ");
                int quantity = Convert.ToInt32(Console.ReadLine());
                BsonDocument taste_document = new BsonDocument
                {
                    { "name", tastes[taste] },
                    { "quantity", quantity }
                };
                tastes_list.Add(taste_document);
                Console.WriteLine("Do you want to add another taste? (yes/no)");
                answer = Console.ReadLine();
            }

            // ask what toppings to use
            List<BsonDocument> toppings_list = new List<BsonDocument>();
            answer = "yes";
            
            
            
            




        }


        public static void createTables()
        {
            MySqlAccess.MySqlAccess.createTables();
        }

        public static void getNotCompletedSales()
        {
            string connStr = "server=localhost;user=root;port=3306;password=";
            MySqlConnection conn = new MySqlConnection(connStr);
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();
            string sql = "SELECT COUNT(*) FROM `ice_cream_store`.`sales` WHERE completed = 0;";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Console.WriteLine("There are " + rdr[0] + " not completed sales");
            }
            rdr.Close();

            // sql = "SELECT * FROM `ice_cream_store`.`sales` WHERE completed = 0;";

            // MySqlCommand cmd2 = new MySqlCommand(sql, conn);
            // MySqlDataReader rdr2 = cmd2.ExecuteReader();
            // while (rdr2.Read())
            // {
            //     Console.WriteLine("sid: " + rdr2[0] + " rid: " + rdr2[1] + " dateTime : " + rdr2[2] + " completed :" + rdr2[3] + " paid: " + rdr2[4] + " total price : " + rdr[5]);
            // }
            // rdr2.Close();

            // Console.WriteLine all the sales that are not completed

            MySqlCommand cmd3 = new MySqlCommand("SELECT * FROM `ice_cream_store`.`sales` WHERE completed = 0;", conn);
            MySqlDataReader rdr3 = cmd3.ExecuteReader();
            while (rdr3.Read())
            {
                Console.WriteLine("sid: " + rdr3[0] + " rid: " + rdr3[1] + " dateTime : " + rdr3[2] + " completed :" + rdr3[3] + " paid: " + rdr3[4] + " total price : " + rdr3[5]);
            }
            rdr3.Close();



            conn.Close();




        }


        public static void getMostCommonIngredient()
        {
            long maximum = 0;
            string mostCommon = "";
            string connStr = "server=localhost;user=root;port=3306;password=";
            MySqlConnection conn = new MySqlConnection(connStr);
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();
            // check the most common topping with 
            //SELECT toppings.name,Toppings_Sales.topid, count(*) as magnitude from sales left join Toppings_Sales on sales.sid = Toppings_Sales.sid left join toppings on toppings.topid = toppings_sales.topid
            // group by Toppings_Sales.topid 
            // order by magnitude desc
            // limit 1;
            string sql = "SELECT toppings.name,Toppings_Sales.topid, count(*) as magnitude from `ice_cream_store`.`sales` left join `ice_cream_store`.`Toppings_Sales` on sales.sid = Toppings_Sales.sid left join `ice_cream_store`.`toppings` on toppings.topid = toppings_sales.topid group by Toppings_Sales.topid order by magnitude desc limit 1;";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                maximum = (long)rdr[2];
                mostCommon = (string)rdr[0];
                Console.WriteLine("The most common topping is " + rdr[0] + " with " + rdr[2] + " sales");
            }
            rdr.Close();
            // get the most common recepcle
            //SELECT  Receptacles.name,sales.rid, count(*) as magnitude from sales left join Receptacles on Receptacles.rid = sales.rid
            // group by sales.rid 
            // order by magnitude desc
            // limit 1;
            sql = "SELECT Receptacles.name,sales.rid, count(*) as magnitude from `ice_cream_store`.`sales` left join `ice_cream_store`.`Receptacles` on Receptacles.rid = sales.rid group by sales.rid order by magnitude desc limit 1;";
            cmd = new MySqlCommand(sql, conn);
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                if ((long)rdr[2] > maximum)
                {
                    maximum = (long)rdr[2];
                    mostCommon = (string)rdr[0];
                }
                Console.WriteLine("The most common receptacle is " + rdr[0] + " with " + rdr[2] + " sales");
            }
            rdr.Close();
            // get the most common taste
            //SELECT  tastes.name,sales.tid, count(*) as magnitude from sales left join tastes on tastes.tid = sales.tid
            // group by sales.tid
            // order by magnitude desc
            // limit 1;
            sql = "SELECT `ice_cream_store`.tastes.name,`ice_cream_store`.Tastes_sales.tid, count(*) as magnitude " +
                "from `ice_cream_store`.sales left join `ice_cream_store`.Tastes_Sales " +
                "on sales.sid = Tastes_Sales.sid " +
                "left join `ice_cream_store`.tastes on tastes.tid = Tastes_Sales.tid " +
                "group by Tastes_Sales.tid " +
                "order by magnitude desc " +
                "limit 1; "


                ;
            cmd = new MySqlCommand(sql, conn);
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                if ((long)rdr[2] > maximum)
                {
                    maximum = (long)rdr[2];
                    mostCommon = (string)rdr[0];
                }
                Console.WriteLine("The most common taste is " + rdr[0] + " with " + rdr[2] + " sales");
            }
            rdr.Close();
            conn.Close();

            // so the most common ingredient is 
            Console.WriteLine("");
            Console.WriteLine("finally ...");
            Console.WriteLine("The most common ingredient is " + mostCommon + " with " + maximum + " sales");

        }

        public static void getDayReport()
        {
            Console.WriteLine("Please enter the date you want to get the stats from (yyyy-mm-dd):");
            string date = Console.ReadLine();
            while (!DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date1))
            {
                Console.WriteLine("Please enter the date you want to get the stats from (yyyy-mm-dd):");
                date = Console.ReadLine();
            }
            string connStr = "server=localhost;user=root;port=3306;password=";
            MySqlConnection conn = new MySqlConnection(connStr);
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();
            string sql = "SELECT AVG(total_price) FROM `ice_cream_store`.`sales` WHERE  DATE(datetime)= '" + date + "'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Console.WriteLine("The average price of the sales on " + date + " is: " + rdr[0]);
            }
            rdr.Close();
            sql = "SELECT SUM(total_price) FROM `ice_cream_store`.`sales` WHERE DATE(datetime) = '" + date + "'";
            cmd = new MySqlCommand(sql, conn);
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Console.WriteLine("The total price of the sales on " + date + " is: " + rdr[0]);
            }
            rdr.Close();
            sql = "SELECT COUNT(*) FROM `ice_cream_store`.`sales` WHERE DATE(datetime)= '" + date + "'";
            cmd = new MySqlCommand(sql, conn);
            rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Console.WriteLine("The number of sales on " + date + " is: " + rdr[0]);
            }
            rdr.Close();

            conn.Close();




        }



        public static void ReadOrder(int order_num)
        {
            string connStr = "server=localhost;user=root;port=3306;password=";
            MySqlConnection conn = new MySqlConnection(connStr);
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();
            ArrayList all = new ArrayList();
            ArrayList all2 = new ArrayList();
            string sql = "select * from `ice_cream_store`.`sales` left join `ice_cream_store`.`Tastes_Sales` on Tastes_Sales.sid = sales.sid left join `ice_cream_store`.`Tastes` on Tastes_Sales.tid = tastes.tid where `ice_cream_store`.sales.sid = " + order_num + ";";
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


            int saleId = 0;
            int recepticleId = 0;
            DateTime dateTime = new DateTime();
            bool completed = false;
            bool paid = false;
            int total_price = 0;
            int toppingId = 0;
            string tastesNames = "";

            Console.WriteLine("order id = " + order_num);
            Console.WriteLine("Tastes Names: ");
            foreach (Object[] row in all)
            {
                saleId = (int)row[0];
                recepticleId = (int)row[1];
                dateTime = (DateTime)row[2];
                completed = (bool)row[3];
                paid = (bool)row[4];
                total_price = (int)row[5];
                toppingId = (int)row[7];
                tastesNames = (string)row[10];
                Console.Write(" " + tastesNames + " ");
            }
            Console.WriteLine("");
            Console.Write("Recepticle: " + receptacles_name[recepticleId] + "\n");
            Console.Write("Date: " + dateTime + "\n");
            Console.Write("Completed: " + completed + "\n");
            Console.Write("Paid: " + paid + "\n");
            Console.Write("Total price: " + total_price + "\n");
            rdr.Close();
            conn.Close();

            MySqlConnection conn2 = new MySqlConnection(connStr);
            Console.WriteLine("Connecting to MySQL...");
            conn2.Open();
            // read the toppings 
            string sql2 = "select * from `ice_cream_store`.`sales` left join `ice_cream_store`.`Toppings_Sales` on Toppings_Sales.sid = sales.sid left join `ice_cream_store`.`Toppings` on Toppings_Sales.topid = Toppings.topid where `ice_cream_store`.sales.sid = " + order_num + ";";
            MySqlCommand cmd2 = new MySqlCommand(sql2, conn2);
            MySqlDataReader rdr2 = cmd2.ExecuteReader();
            Console.WriteLine("Toppings: ");
            while (rdr2.Read())
            {
                //Console.WriteLine(rdr[0] + " -- " + rdr[1]);
                Object[] numb = new Object[rdr2.FieldCount];
                rdr2.GetValues(numb);
                //Array.ForEach(numb, Console.WriteLine);
                all2.Add(numb);
                Console.Write(" " + numb[4] + " ");
            }
            Console.WriteLine("");
            rdr2.Close();
            conn2.Close();

            string toppingNames = "";
            Console.WriteLine("toppings are : ");
            foreach (Object[] row in all2)
            {

                toppingNames = (string)row[9];
                Console.Write(" " + toppingNames + " ");

            }


        }



        public static void deleteOrder(int order_num)
        {
            string connStr = "server=localhost;user=root;port=3306;password=";
            MySqlConnection conn = new MySqlConnection(connStr);
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();

            string sql = "DELETE FROM `ice_cream_store`.`Tastes_Sales` WHERE sid = " + order_num + ";";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            sql = "DELETE FROM `ice_cream_store`.`Toppings_Sales` WHERE sid = " + order_num + ";";

            cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            sql = "DELETE FROM `ice_cream_store`.`Sales` WHERE sid = " + order_num + ";";
            cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public static void makeOrder()
        {
            int intindex = 0;
            Console.WriteLine("please choose a Receptacle ");

            for (int i = 0; i < receptacles_name.Length; i++)
            {
                Console.WriteLine(i + " " + receptacles_name[i]);
            }
            int receptacle = int.Parse(Console.ReadLine());
            while (receptacle < 0 || receptacle > receptacles_name.Length)
            {
                Console.WriteLine("please choose a Receptacle between 0 to " + receptacles_name.Length);
                receptacle = int.Parse(Console.ReadLine());
            }



            List<string> chosen_tastes = new List<string>();
            HashSet<string> chosen_toppings = new HashSet<string>();

            Console.WriteLine("How many balls of ice cream  ");
            int balls = 0;
            if (receptacle == 0)
            {
                for (int i = 1; i <= 3; i++)
                {
                    Console.WriteLine(i + " " + "balls");
                }
                balls = int.Parse(Console.ReadLine());
                while (balls > 3 || balls < 1)
                {
                    Console.WriteLine("please choose a number between 1-3");
                    balls = int.Parse(Console.ReadLine());
                }
            }
            else if (receptacle == 1)
            {
                for (int i = 1; i <= 4; i++)
                {
                    Console.WriteLine(i + " " + "balls");
                }
                balls = int.Parse(Console.ReadLine());
                while (balls > 4 || balls < 1)
                {
                    Console.WriteLine("please choose a number between 1-4");
                    balls = int.Parse(Console.ReadLine());
                }
            }
            else if (receptacle == 2)
            {
                for (int i = 1; i <= 7; i++)
                {
                    Console.WriteLine(i + " " + "balls");
                }
                balls = int.Parse(Console.ReadLine());
                while (balls > 7 || balls < 1)
                {
                    Console.WriteLine("please choose a number between 1-7");
                    balls = int.Parse(Console.ReadLine());
                }
            }
            else
            {
                Console.WriteLine("you choose wrong number");
            }

            Console.WriteLine("please choose a taste ");
            for (int i = 0; i < tastes.Length; i++)
            {
                Console.WriteLine(i + " " + tastes[i]);
            }
            for (int i = 0; i < balls; i++)
            {
                int taste = int.Parse(Console.ReadLine());
                while (taste > 14 || taste < 0)
                {
                    Console.WriteLine("please choose a number between 0-14");
                    taste = int.Parse(Console.ReadLine());
                }
                chosen_tastes.Add(tastes[taste]);
            }

            Console.WriteLine("please choose a topping ");

            if (balls > 1)
            {
                if (receptacle == 0)
                {
                    if ((chosen_tastes.Contains("Chocolate") || chosen_tastes.Contains("Mekupelet") && chosen_tastes.Contains("Vanil")))
                    {
                        for (int i = 0; i < toppings_for_chocolate_mekupelet_vanila.Length; i++)
                        {
                            Console.WriteLine(i + " " + toppings_for_chocolate_mekupelet_vanila[i]);
                        }
                        int topping = int.Parse(Console.ReadLine());
                        while (topping > 4 || topping < 0)
                        {
                            Console.WriteLine("please choose a number between 0-4");
                            topping = int.Parse(Console.ReadLine());
                        }
                        chosen_toppings.Add(toppings_for_chocolate_mekupelet_vanila[topping]);
                    }
                    else if (chosen_tastes.Contains("Chocolate") || chosen_tastes.Contains("Mekupelet"))
                    {
                        for (int i = 0; i < toppings_for_chocolate_mekupelet.Length; i++)
                        {
                            Console.WriteLine(i + " " + toppings_for_chocolate_mekupelet[i]);
                        }
                        int topping = int.Parse(Console.ReadLine());
                        while (topping > 5 || topping < 0)
                        {
                            Console.WriteLine("please choose a number between 0-5");
                            topping = int.Parse(Console.ReadLine());
                        }
                        chosen_toppings.Add(toppings_for_chocolate_mekupelet[topping]);

                    }
                    else if (chosen_tastes.Contains("Vanil"))
                    {
                        for (int i = 0; i < toppings_for_vanil.Length; i++)
                        {
                            Console.WriteLine(i + " " + toppings_for_vanil[i]);
                        }
                        int topping = int.Parse(Console.ReadLine());
                        while (topping > 5 || topping < 0)
                        {
                            Console.WriteLine("please choose a number between 0-5");
                            topping = int.Parse(Console.ReadLine());
                        }
                        chosen_toppings.Add(toppings_for_vanil[topping]);
                    }
                    else
                    {
                        for (int i = 0; i < toppings.Length; i++)
                        {
                            Console.WriteLine(i + " " + toppings[i]);
                        }
                        int topping = int.Parse(Console.ReadLine());
                        while (topping > 6 || topping < 0)
                        {
                            Console.WriteLine("please choose a number between 0-6");
                            topping = int.Parse(Console.ReadLine());
                        }
                        chosen_toppings.Add(toppings[topping]);
                    }
                }
                else if (receptacle == 1 || receptacle == 2)
                {
                    int toppings11 = 1;
                    if (receptacle == 1)
                    {
                        Console.WriteLine("Wow you took a Spacial Receptacle, how many toppings do you want? ");
                        toppings11 = int.Parse(Console.ReadLine());
                        while (toppings11 > 6 || toppings11 < 0)
                        {
                            Console.WriteLine("please choose a number between 0-6");
                            toppings11 = int.Parse(Console.ReadLine());
                        }
                    }
                    else
                    {
                        Console.WriteLine("Wow you took a BOX, how many toppings do you want? ( max 3 ) ");
                        toppings11 = int.Parse(Console.ReadLine());
                        while (toppings11 > 3 || toppings11 < 0)
                        {
                            Console.WriteLine("please choose a number between 0-3");
                            toppings11 = int.Parse(Console.ReadLine());
                        }

                    }
                    for (int j = 0; j < toppings11; j++)
                    {
                        Console.WriteLine("please choose the " + (j + 1) + " topping ");
                        if ((chosen_tastes.Contains("Chocolate") || chosen_tastes.Contains("Mekupelet") && chosen_tastes.Contains("Vanil")))
                        {
                            for (int i = 0; i < toppings_for_chocolate_mekupelet_vanila.Length; i++)
                            {
                                Console.WriteLine(i + " " + toppings_for_chocolate_mekupelet_vanila[i]);
                            }
                            int topping = int.Parse(Console.ReadLine());
                            while (topping > 4 || topping < 0)
                            {
                                Console.WriteLine("please choose a number between 0-4");
                                topping = int.Parse(Console.ReadLine());
                            }
                            chosen_toppings.Add(toppings_for_chocolate_mekupelet_vanila[topping]);
                        }
                        else if (chosen_tastes.Contains("Chocolate") || chosen_tastes.Contains("Mekupelet"))
                        {
                            for (int i = 0; i < toppings_for_chocolate_mekupelet.Length; i++)
                            {
                                Console.WriteLine(i + " " + toppings_for_chocolate_mekupelet[i]);
                            }
                            int topping = int.Parse(Console.ReadLine());
                            while (topping > 5 || topping < 0)
                            {
                                Console.WriteLine("please choose a number between 0-5");
                                topping = int.Parse(Console.ReadLine());
                            }
                            chosen_toppings.Add(toppings_for_chocolate_mekupelet[topping]);

                        }
                        else if (chosen_tastes.Contains("Vanil"))
                        {
                            for (int i = 0; i < toppings_for_vanil.Length; i++)
                            {
                                Console.WriteLine(i + " " + toppings_for_vanil[i]);
                            }
                            int topping = int.Parse(Console.ReadLine());
                            while (topping > 5 || topping < 0)
                            {
                                Console.WriteLine("please choose a number between 0-5");
                                topping = int.Parse(Console.ReadLine());
                            }
                            chosen_toppings.Add(toppings_for_vanil[topping]);
                        }
                        else
                        {
                            for (int i = 0; i < toppings.Length; i++)
                            {
                                Console.WriteLine(i + " " + toppings[i]);
                            }
                            int topping = int.Parse(Console.ReadLine());
                            while (topping > 6 || topping < 0)
                            {
                                Console.WriteLine("please choose a number between 0-6");
                                topping = int.Parse(Console.ReadLine());
                            }
                            chosen_toppings.Add(toppings[topping]);
                        }



                    }
                }



            }
            int ball_price = 0;
            if (balls == 1)
            {
                ball_price = 7;
            }
            else if (balls == 2)
            {
                ball_price = 12;
            }
            else if (balls > 3)
            {
                ball_price = balls * 6;
            }
            int total_price = ball_price + receptacles_price[receptacle];
            Console.WriteLine("Your total price is " + total_price + " NIS");
            Console.WriteLine("Your chosen tastes are: ");
            foreach (string taste in chosen_tastes)
            {
                Console.WriteLine(taste);
            }
            Console.WriteLine("Your chosen toppings are: ");
            foreach (string topping in chosen_toppings)
            {
                Console.WriteLine(topping);
            }
            Console.WriteLine("Your chosen balls are: " + balls);
            Console.WriteLine("Your chosen receptacle price is: " + receptacles_price[receptacle]);

            Sale s = new Sale(receptacle + 1, DateTime.Now, true, true, total_price);

            MySqlAccess.MySqlAccess.insertObject(s);


            // https://stackoverflow.com/questions/15862191/counting-the-number-of-times-a-value-appears-in-an-array
            // insert taste_sales
            // make hashmap for tastes quantity
            Dictionary<string, int> taste_quantity = new Dictionary<string, int>();
            foreach (string taste in chosen_tastes)
            {
                if (taste_quantity.ContainsKey(taste))
                {
                    taste_quantity[taste]++;
                }
                else
                {
                    taste_quantity.Add(taste, 1);
                }
            }
            // https://stackoverflow.com/questions/3133711/select-last-id-without-insert
            ArrayList all = new ArrayList();
            string connStr = "server=localhost;user=root;port=3306;password=";
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();
            string sql = "SELECT max(`sid`) FROM `ice_cream_store`.`Sales`";
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

            foreach (Object[] row in all)
            {
                intindex = (int)row[0];

            }


            // now insert to taste_sales
            foreach (KeyValuePair<string, int> entry in taste_quantity)
            {
                // get the index of the taste
                int taste_index = Array.IndexOf(tastes, entry.Key);
                Taste_Sale ts = new Taste_Sale(intindex, taste_index + 1, entry.Value);
                MySqlAccess.MySqlAccess.insertObject(ts);
            }

            // insert topping_sales
            // now insert to topping_sales
            foreach (string topping in chosen_toppings)
            {
                // get the index of the topping
                int topping_index = Array.IndexOf(toppings, topping);
                Topping_Sale ts = new Topping_Sale(intindex, topping_index + 1);
                MySqlAccess.MySqlAccess.insertObject(ts);
            }
            Console.WriteLine("Order Done!");
        }

        public static void EditOrder(int order_number)
        {
            int intindex = order_number;

            // UPDATE table_name
            //SET column1 = value1, column2 = value2, ...
            //WHERE condition;
            string connStr = "server=localhost;user=root;port=3306;password=";
            MySqlConnection conn = new MySqlConnection(connStr);
            Console.WriteLine("Connecting to MySQL...");
            conn.Open();

            string sql = "DELETE FROM `ice_cream_store`.`Tastes_Sales` WHERE sid = " + intindex + ";";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            sql = "DELETE FROM `ice_cream_store`.`Toppings_Sales` WHERE sid = " + intindex + ";";

            cmd = new MySqlCommand(sql, conn);

            cmd.ExecuteNonQuery();

            Console.WriteLine("please choose a Receptacle ");

            for (int i = 0; i < receptacles_name.Length; i++)
            {
                Console.WriteLine(i + " " + receptacles_name[i]);
            }
            int receptacle = int.Parse(Console.ReadLine());
            while (receptacle < 0 || receptacle > receptacles_name.Length)
            {
                Console.WriteLine("please choose a Receptacle between 0 to " + receptacles_name.Length);
                receptacle = int.Parse(Console.ReadLine());
            }

            sql = "UPDATE `ice_cream_store`.`Sales` SET `rid` = " + receptacle + " WHERE sid = " + intindex + ";";
            cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            sql = "UPDATE `ice_cream_store`.`Sales` SET `datetime` = " + "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'" + " WHERE sid = " + intindex + ";";
            cmd = new MySqlCommand(sql, conn);
            // "UPDATE `ice_cream_store`.`Sales` SET `datetime` = 2022-09-15 16:38:49 WHERE sid = 110;"
            cmd.ExecuteNonQuery();







            List<string> chosen_tastes = new List<string>();
            HashSet<string> chosen_toppings = new HashSet<string>();

            Console.WriteLine("How many balls of ice cream  ");
            int balls = 0;
            if (receptacle == 0)
            {
                for (int i = 1; i <= 3; i++)
                {
                    Console.WriteLine(i + " " + "balls");
                }
                balls = int.Parse(Console.ReadLine());
                while (balls > 3 || balls < 1)
                {
                    Console.WriteLine("please choose a number between 1-3");
                    balls = int.Parse(Console.ReadLine());
                }
            }
            else if (receptacle == 1)
            {
                for (int i = 1; i <= 4; i++)
                {
                    Console.WriteLine(i + " " + "balls");
                }
                balls = int.Parse(Console.ReadLine());
                while (balls > 4 || balls < 1)
                {
                    Console.WriteLine("please choose a number between 1-4");
                    balls = int.Parse(Console.ReadLine());
                }
            }
            else if (receptacle == 2)
            {
                for (int i = 1; i <= 7; i++)
                {
                    Console.WriteLine(i + " " + "balls");
                }
                balls = int.Parse(Console.ReadLine());
                while (balls > 7 || balls < 1)
                {
                    Console.WriteLine("please choose a number between 1-7");
                    balls = int.Parse(Console.ReadLine());
                }
            }
            else
            {
                Console.WriteLine("you choose wrong number");
            }

            Console.WriteLine("please choose a taste ");
            for (int i = 0; i < tastes.Length; i++)
            {
                Console.WriteLine(i + " " + tastes[i]);
            }
            for (int i = 0; i < balls; i++)
            {
                int taste = int.Parse(Console.ReadLine());
                while (taste > 14 || taste < 0)
                {
                    Console.WriteLine("please choose a number between 0-14");
                    taste = int.Parse(Console.ReadLine());
                }
                chosen_tastes.Add(tastes[taste]);
            }

            Console.WriteLine("please choose a topping ");

            if (balls > 1)
            {
                if (receptacle == 0)
                {
                    if ((chosen_tastes.Contains("Chocolate") || chosen_tastes.Contains("Mekupelet") && chosen_tastes.Contains("Vanil")))
                    {
                        for (int i = 0; i < toppings_for_chocolate_mekupelet_vanila.Length; i++)
                        {
                            Console.WriteLine(i + " " + toppings_for_chocolate_mekupelet_vanila[i]);
                        }
                        int topping = int.Parse(Console.ReadLine());
                        while (topping > 4 || topping < 0)
                        {
                            Console.WriteLine("please choose a number between 0-4");
                            topping = int.Parse(Console.ReadLine());
                        }
                        chosen_toppings.Add(toppings_for_chocolate_mekupelet_vanila[topping]);
                    }
                    else if (chosen_tastes.Contains("Chocolate") || chosen_tastes.Contains("Mekupelet"))
                    {
                        for (int i = 0; i < toppings_for_chocolate_mekupelet.Length; i++)
                        {
                            Console.WriteLine(i + " " + toppings_for_chocolate_mekupelet[i]);
                        }
                        int topping = int.Parse(Console.ReadLine());
                        while (topping > 5 || topping < 0)
                        {
                            Console.WriteLine("please choose a number between 0-5");
                            topping = int.Parse(Console.ReadLine());
                        }
                        chosen_toppings.Add(toppings_for_chocolate_mekupelet[topping]);

                    }
                    else if (chosen_tastes.Contains("Vanil"))
                    {
                        for (int i = 0; i < toppings_for_vanil.Length; i++)
                        {
                            Console.WriteLine(i + " " + toppings_for_vanil[i]);
                        }
                        int topping = int.Parse(Console.ReadLine());
                        while (topping > 5 || topping < 0)
                        {
                            Console.WriteLine("please choose a number between 0-5");
                            topping = int.Parse(Console.ReadLine());
                        }
                        chosen_toppings.Add(toppings_for_vanil[topping]);
                    }
                    else
                    {
                        for (int i = 0; i < toppings.Length; i++)
                        {
                            Console.WriteLine(i + " " + toppings[i]);
                        }
                        int topping = int.Parse(Console.ReadLine());
                        while (topping > 6 || topping < 0)
                        {
                            Console.WriteLine("please choose a number between 0-6");
                            topping = int.Parse(Console.ReadLine());
                        }
                        chosen_toppings.Add(toppings[topping]);
                    }
                }
                else if (receptacle == 1 || receptacle == 2)
                {
                    int toppings11 = 1;
                    if (receptacle == 1)
                    {
                        Console.WriteLine("Wow you took a Spacial Receptacle, how many toppings do you want? ");
                        toppings11 = int.Parse(Console.ReadLine());
                        while (toppings11 > 6 || toppings11 < 0)
                        {
                            Console.WriteLine("please choose a number between 0-6");
                            toppings11 = int.Parse(Console.ReadLine());
                        }
                    }
                    else
                    {
                        Console.WriteLine("Wow you took a BOX, how many toppings do you want? ( max 3 ) ");
                        toppings11 = int.Parse(Console.ReadLine());
                        while (toppings11 > 3 || toppings11 < 0)
                        {
                            Console.WriteLine("please choose a number between 0-3");
                            toppings11 = int.Parse(Console.ReadLine());
                        }

                    }
                    for (int j = 0; j < toppings11; j++)
                    {
                        Console.WriteLine("please choose the " + (j + 1) + " topping ");
                        if ((chosen_tastes.Contains("Chocolate") || chosen_tastes.Contains("Mekupelet") && chosen_tastes.Contains("Vanil")))
                        {
                            for (int i = 0; i < toppings_for_chocolate_mekupelet_vanila.Length; i++)
                            {
                                Console.WriteLine(i + " " + toppings_for_chocolate_mekupelet_vanila[i]);
                            }
                            int topping = int.Parse(Console.ReadLine());
                            while (topping > 4 || topping < 0)
                            {
                                Console.WriteLine("please choose a number between 0-4");
                                topping = int.Parse(Console.ReadLine());
                            }
                            chosen_toppings.Add(toppings_for_chocolate_mekupelet_vanila[topping]);
                        }
                        else if (chosen_tastes.Contains("Chocolate") || chosen_tastes.Contains("Mekupelet"))
                        {
                            for (int i = 0; i < toppings_for_chocolate_mekupelet.Length; i++)
                            {
                                Console.WriteLine(i + " " + toppings_for_chocolate_mekupelet[i]);
                            }
                            int topping = int.Parse(Console.ReadLine());
                            while (topping > 5 || topping < 0)
                            {
                                Console.WriteLine("please choose a number between 0-5");
                                topping = int.Parse(Console.ReadLine());
                            }
                            chosen_toppings.Add(toppings_for_chocolate_mekupelet[topping]);

                        }
                        else if (chosen_tastes.Contains("Vanil"))
                        {
                            for (int i = 0; i < toppings_for_vanil.Length; i++)
                            {
                                Console.WriteLine(i + " " + toppings_for_vanil[i]);
                            }
                            int topping = int.Parse(Console.ReadLine());
                            while (topping > 5 || topping < 0)
                            {
                                Console.WriteLine("please choose a number between 0-5");
                                topping = int.Parse(Console.ReadLine());
                            }
                            chosen_toppings.Add(toppings_for_vanil[topping]);
                        }
                        else
                        {
                            for (int i = 0; i < toppings.Length; i++)
                            {
                                Console.WriteLine(i + " " + toppings[i]);
                            }
                            int topping = int.Parse(Console.ReadLine());
                            while (topping > 6 || topping < 0)
                            {
                                Console.WriteLine("please choose a number between 0-6");
                                topping = int.Parse(Console.ReadLine());
                            }
                            chosen_toppings.Add(toppings[topping]);
                        }



                    }
                }



            }



            int ball_price = 0;
            if (balls == 1)
            {
                ball_price = 7;
            }
            else if (balls == 2)
            {
                ball_price = 12;
            }
            else if (balls > 3)
            {
                ball_price = balls * 6;
            }
            int total_price = ball_price + receptacles_price[receptacle];
            Console.WriteLine("Your total price is " + total_price + " NIS");
            Console.WriteLine("Your chosen tastes are: ");
            foreach (string taste in chosen_tastes)
            {
                Console.WriteLine(taste);
            }
            Console.WriteLine("Your chosen toppings are: ");
            foreach (string topping in chosen_toppings)
            {
                Console.WriteLine(topping);
            }
            Console.WriteLine("Your chosen balls are: " + balls);
            Console.WriteLine("Your chosen receptacle price is: " + receptacles_price[receptacle]);




            // https://stackoverflow.com/questions/15862191/counting-the-number-of-times-a-value-appears-in-an-array
            // insert taste_sales
            // make hashmap for tastes quantity
            Dictionary<string, int> taste_quantity = new Dictionary<string, int>();
            foreach (string taste in chosen_tastes)
            {
                if (taste_quantity.ContainsKey(taste))
                {
                    taste_quantity[taste]++;
                }
                else
                {
                    taste_quantity.Add(taste, 1);
                }
            }


            // now insert to taste_sales
            foreach (KeyValuePair<string, int> entry in taste_quantity)
            {
                // get the index of the taste
                int taste_index = Array.IndexOf(tastes, entry.Key);
                Taste_Sale ts = new Taste_Sale(intindex, taste_index + 1, entry.Value);
                MySqlAccess.MySqlAccess.insertObject(ts);
            }

            // insert topping_sales
            // now insert to topping_sales
            foreach (string topping in chosen_toppings)
            {
                // get the index of the topping
                int topping_index = Array.IndexOf(toppings, topping);
                Topping_Sale ts = new Topping_Sale(intindex, topping_index + 1);
                MySqlAccess.MySqlAccess.insertObject(ts);
            }

            Console.WriteLine("Do you want to complete the order? (Y/N)");
            string answer = Console.ReadLine();
            if (answer == "Y")
            {
                sql = "UPDATE `ice_cream_store`.`Sales` SET `completed` = '1' WHERE sid = " + intindex + ";";
                cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Your order has been completed");
            }
            else
            {
                sql = "UPDATE `ice_cream_store`.`Sales` SET `completed` = '0' WHERE sid = " + intindex + ";";
                cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Your order has been canceled");
            }

            Console.WriteLine("Do you want to pay now? (Y/N)");
            answer = Console.ReadLine();
            if (answer == "Y")
            {
                sql = "UPDATE `ice_cream_store`.`Sales` SET `paid` = '1' WHERE sid = " + intindex + ";";
                cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Your order has been paid");
            }
            else
            {
                sql = "UPDATE `ice_cream_store`.`Sales` SET `paid` = '0' WHERE sid = " + intindex + ";";
                cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                Console.WriteLine("No money , no order mister ");
            }

            sql = "UPDATE `ice_cream_store`.`Sales` SET `total_price` = " + total_price + " WHERE sid = " + intindex + ";";
            cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();

            Console.WriteLine("Order Done!");



        }









        public static void fillTables(int num)
        {
            Random r = new Random();

            //generate values for Tastes
            for (int i = 0; i < tastes.Length; i++)
            {
                Taste o = new Taste(tastes[i]);
                MySqlAccess.MySqlAccess.insertObject(o);
            }

            //generate values for receptacles
            for (int i = 0; i < receptacles_name.Length; i++)
            {
                Receptacle o = new Receptacle(receptacles_name[i], receptacles_price[i]);
                MySqlAccess.MySqlAccess.insertObject(o);
            }

            //generate values for Toppings
            for (int i = 0; i < toppings.Length; i++)
            {
                Topping o = new Topping(toppings[i]);
                MySqlAccess.MySqlAccess.insertObject(o);
            }


            List<string> chosen_tastes = new List<string>();
            HashSet<string> chosen_toppings = new HashSet<string>();
            RandomDateTime date = new RandomDateTime();
            date.Next();
            Random gen = new Random();
            //generate random values for Tastes_Sales Topping_Sales Sales
            for (int i = 0; i < num; i++)
            { // 86 85

                int rReceptacle = r.Next(0, receptacles_name.Length);
                date.Next();
                bool completed = true;
                bool paid = true;

                if (gen.NextDouble() <= 0.2)
                {
                    completed = false;
                }
                if (gen.NextDouble() <= 0.2)
                {
                    paid = false;
                }

                int ball_price = 0;
                int balls_amount = 0;

                if (rReceptacle == 0)
                {
                    // if its a regular cone then max balls will be 3
                    balls_amount = r.Next(1, 3);
                }
                else if (rReceptacle == 1)
                {
                    // if its a special cone then max balls will be 4 
                    balls_amount = r.Next(1, 4);
                }
                else if (rReceptacle == 2)
                {
                    // if its a Box then max balls will be 7 
                    balls_amount = r.Next(1, 7);
                }

                // now choose tastes
                for (int x = 0; x < balls_amount; x++)
                {
                    int rTaste = r.Next(0, tastes.Length);
                    chosen_tastes.Add(tastes[rTaste]);
                }

                // now choose toppings if its Box or special cone or regular cone with 2 or more balls
                if (rReceptacle == 1 || rReceptacle == 2 || rReceptacle == 0 && balls_amount > 1)
                {
                    // only 1 topping on a cone or it will break :)
                    if (rReceptacle == 1 || rReceptacle == 0)
                    {
                        if ((chosen_tastes.Contains("Chocolate") || chosen_tastes.Contains("Mekupelet")) && chosen_tastes.Contains("Vanil"))
                        {
                            int rTopping = r.Next(0, toppings_for_chocolate_mekupelet_vanila.Length);
                            chosen_toppings.Add(toppings_for_chocolate_mekupelet_vanila[rTopping]);
                        }
                        else if (chosen_tastes.Contains("Chocolate") || chosen_tastes.Contains("Mekupelet"))
                        {
                            int rTopping = r.Next(0, toppings_for_chocolate_mekupelet.Length);
                            chosen_toppings.Add(toppings_for_chocolate_mekupelet[rTopping]);
                        }
                        else if (chosen_tastes.Contains("Vanil"))
                        {
                            int rTopping = r.Next(0, toppings_for_vanil.Length);
                            chosen_toppings.Add(toppings_for_vanil[rTopping]);
                        }
                        else
                        {
                            int rTopping = r.Next(0, toppings.Length);
                            chosen_toppings.Add(toppings[rTopping]);
                        }
                    }
                    else
                    {
                        // choose max 3 toppings for Box

                        if ((chosen_tastes.Contains("Chocolate") || chosen_tastes.Contains("Mekupelet")) && chosen_tastes.Contains("Vanil"))
                        {
                            for (int three_tastes = 0; three_tastes < 3; three_tastes++)
                            {
                                int rTopping = r.Next(0, toppings_for_chocolate_mekupelet_vanila.Length);
                                chosen_toppings.Add(toppings[rTopping]);
                            }
                        }
                        else if (chosen_tastes.Contains("Chocolate") || chosen_tastes.Contains("Mekupelet"))
                        {
                            for (int three_tastes = 0; three_tastes < 3; three_tastes++)
                            {
                                int rTopping = r.Next(0, toppings_for_chocolate_mekupelet.Length);
                                chosen_toppings.Add(toppings[rTopping]);
                            }
                        }
                        else if (chosen_tastes.Contains("Vanil"))
                        {
                            for (int three_tastes = 0; three_tastes < 3; three_tastes++)
                            {
                                int rTopping = r.Next(0, toppings_for_vanil.Length);
                                chosen_toppings.Add(toppings[rTopping]);
                            }
                        }
                        else
                        {
                            for (int three_tastes = 0; three_tastes < 3; three_tastes++)
                            {
                                int rTopping = r.Next(0, toppings.Length);
                                chosen_toppings.Add(toppings[rTopping]);
                            }
                        }
                    }
                }

                // now handle the ball priceing 
                if (balls_amount == 1)
                {
                    ball_price = 7;
                }
                else if (balls_amount == 2)
                {
                    ball_price = 12;
                }
                else if (balls_amount > 3)
                {
                    ball_price = balls_amount * 6;
                }
                int total_price = ball_price + receptacles_price[rReceptacle];
                Sale s = new Sale(rReceptacle + 1, date.Next(), completed, paid, total_price);
                MySqlAccess.MySqlAccess.insertObject(s);
                // https://stackoverflow.com/questions/15862191/counting-the-number-of-times-a-value-appears-in-an-array
                // insert taste_sales
                // make hashmap for tastes quantity
                Dictionary<string, int> taste_quantity = new Dictionary<string, int>();
                foreach (string taste in chosen_tastes)
                {
                    if (taste_quantity.ContainsKey(taste))
                    {
                        taste_quantity[taste]++;
                    }
                    else
                    {
                        taste_quantity.Add(taste, 1);
                    }
                }
                // now insert to taste_sales
                foreach (KeyValuePair<string, int> entry in taste_quantity)
                {
                    // get the index of the taste
                    int taste_index = Array.IndexOf(tastes, entry.Key);
                    Taste_Sale ts = new Taste_Sale(i + 1, taste_index + 1, entry.Value);
                    MySqlAccess.MySqlAccess.insertObject(ts);
                }
                // insert topping_sales
                // now insert to topping_sales
                foreach (string topping in chosen_toppings)
                {
                    // get the index of the topping
                    int topping_index = Array.IndexOf(toppings, topping);
                    Topping_Sale ts = new Topping_Sale(i + 1, topping_index + 1);
                    MySqlAccess.MySqlAccess.insertObject(ts);
                }

            } // 244
        }

        public static ArrayList getTableData(string tableName)
        {
            ArrayList all = MySqlAccess.MySqlAccess.readAll(tableName);
            ArrayList results = new ArrayList();

            if (tableName == "Tastes")
            {
                foreach (Object[] row in all)
                {
                    Taste o = new Taste((string)row[1]);
                    o.setID((int)row[0]);
                    results.Add(o);
                }
            }

            if (tableName == "Toppings")
            {
                foreach (Object[] row in all)
                {
                    Topping o = new Topping((string)row[1]);
                    o.setID((int)row[0]);
                    results.Add(o);
                }
            }

            if (tableName == "Receptacles")
            {
                foreach (Object[] row in all)
                {
                    Receptacle o = new Receptacle((string)row[1], (int)row[2]);
                    o.setID((int)row[0]);
                    results.Add(o);
                }
            }

            if (tableName == "Sales")
            {
                foreach (Object[] row in all)
                {
                    // format the DateTime to be in the right format
                    // convert the row[2][0] to string
                    // string date = row[2][0].ToString();
                    // string sDate = (string)row[2]);
                    //DateTime date = DateTime.ParseExact("sDate", "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

                    Sale o = new Sale((int)row[1], (DateTime)row[2], (bool)row[3], (bool)row[4], (int)row[5]);
                    o.setID((int)row[0]);
                    results.Add(o);
                }
            }

            if (tableName == "Tastes_Sales")
            {
                foreach (Object[] row in all)
                {
                    Taste_Sale o = new Taste_Sale((int)row[0], (int)row[1], (int)row[2]);
                    results.Add(o);
                }
            }

            if (tableName == "Toppings_Sales")
            {
                foreach (Object[] row in all)
                {
                    Topping_Sale o = new Topping_Sale((int)row[0], (int)row[1]);
                    results.Add(o);
                }
            }

            return results;
        }

    }
}