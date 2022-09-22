
using MongoDB.Driver;
using MongoDB.Bson;

using BusinessEntities;
using BusinessLogic;
using System.Collections;

namespace MongoAccess
{
    public class MongoAccess
    {
        public static void getNotCompletedSales()
        {
            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://arielUni:arielUni123@cluster0.ayzwu1i.mongodb.net/?retryWrites=true&w=majority");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("ice_cream_store_mongo");
            IMongoCollection<MongoSale> collection = database.GetCollection<MongoSale>("sales");
            var filter = Builders<MongoSale>.Filter.Eq("completed", false);
            var result = collection.Find(filter).ToList();
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }


        }
        public static void getSalesOfSpecificDate(string date)
        {
            // connect to mongo
            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://arielUni:arielUni123@cluster0.ayzwu1i.mongodb.net/?retryWrites=true&w=majority");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("ice_cream_store_mongo");
            IMongoCollection<MongoSale> collection = database.GetCollection<MongoSale>("sales");

            // get all sales of specific date
            var filter = Builders<MongoSale>.Filter.Eq("dateTime", date);
            var sales = collection.Find(filter).ToList();
            foreach (var doc in sales)
            {
                Console.WriteLine(doc);
            }
        }



        public static void getSale(string id)
        {
            // connect to mongo
            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://arielUni:arielUni123@cluster0.ayzwu1i.mongodb.net/?retryWrites=true&w=majority");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("ice_cream_store_mongo");
            IMongoCollection<MongoSale> collection = database.GetCollection<MongoSale>("sales");
            // search for the sale with the specified _id
            var filter = Builders<MongoSale>.Filter.Eq("_id", id);
            // print the sale
            try
            {
                var sale = collection.Find(filter).FirstOrDefault();
                Console.WriteLine(sale);
            }
            catch (System.Exception e)
            {
                Console.WriteLine("Sale not found");
            }
        }

        public static bool checkIfCollectionIsEmpty()
        {
            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://arielUni:arielUni123@cluster0.ayzwu1i.mongodb.net/?retryWrites=true&w=majority");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("ice_cream_store_mongo");

            // check also for the toppings collection
            IMongoCollection<Topping> collection2 = database.GetCollection<Topping>("toppings");
            var filter2 = Builders<Topping>.Filter.Empty;
            var count2 = collection2.CountDocuments(filter2);
            if (count2 == 0)
            {
                return true;
            }
            // check also for the tastes collection
            IMongoCollection<Taste> collection3 = database.GetCollection<Taste>("tastes");
            var filter3 = Builders<Taste>.Filter.Empty;
            var count3 = collection3.CountDocuments(filter3);
            if (count3 == 0)
            {
                return true;
            }
            // check also for the receptacles collection

            IMongoCollection<Receptacle> collection4 = database.GetCollection<Receptacle>("receptacles");
            var filter4 = Builders<Receptacle>.Filter.Empty;
            var count4 = collection4.CountDocuments(filter4);
            if (count4 == 0)
            {
                return true;
            }


            return false;


        }

        public static bool checkIfSalesEmpty()
        {
            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://arielUni:arielUni123@cluster0.ayzwu1i.mongodb.net/?retryWrites=true&w=majority");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("ice_cream_store_mongo");
            IMongoCollection<MongoSale> collection = database.GetCollection<MongoSale>("sales");
            var filter = Builders<MongoSale>.Filter.Empty;
            try
            {
                var sales = collection.Find(filter).ToList();
                if (sales.Count == 0)
                {
                    return true;
                }
                return false;
            }
            catch (System.Exception)
            {
                return false;
            }
        }


        // getAllSales
        public static void getAllSales()
        {
            // connect to mongo
            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://arielUni:arielUni123@cluster0.ayzwu1i.mongodb.net/?retryWrites=true&w=majority");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("ice_cream_store_mongo");
            IMongoCollection<MongoSale> collection = database.GetCollection<MongoSale>("sales");
            // convert the collection to a list of MongoSale
            try
            {
                List<MongoSale> sales = collection.Find(new BsonDocument()).ToList();
                // print all the sales
                foreach (MongoSale sale in sales)
                {
                    Console.WriteLine(sale);
                }
            }
            catch (System.Exception)
            {
                Console.WriteLine("No sales found");
            }


        }

        public static void test()
        {

            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://arielUni:arielUni123@cluster0.ayzwu1i.mongodb.net/?retryWrites=true&w=majority");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("ice_cream_store_mongo");

            var dbList = database.ListCollectionNames().ToList();

            Console.WriteLine("The list of databases on this server is: ");
            foreach (var db in dbList)
            {
                Console.WriteLine(db);
            }
        }

        public static void fillOneSale(MongoSale sale)
        {
            // connect to mongo
            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://arielUni:arielUni123@cluster0.ayzwu1i.mongodb.net/?retryWrites=true&w=majority");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("ice_cream_store_mongo");
            IMongoCollection<MongoSale> collection = database.GetCollection<MongoSale>("sales");
            // insert the sale
            collection.InsertOne(sale);
        }

        public static void fillSales(List<MongoSale> sales) //to make it by interface, rename to fillData
        {

            List<BsonDocument> documents = new List<BsonDocument>();

            //build list of all documents
            // insert all the sales to the documents list
            foreach (MongoSale sale in sales)
            {
                BsonDocument document = new BsonDocument
                {
                    { "id", sale.getId() },
                    { "rid", sale.getrid() },
                    { "dateTime", sale.getDateTime() },
                    { "completed", sale.getCompleted() },
                    { "paid", sale.getPaid() },
                    { "total_price", sale.getTotalPrice() },
                    // add the list of tastesQuantity
                    { "tastesQuantity", new BsonArray(sale.getTastesQuantityArray()) },
                    // add the list of toppings
                    { "toppings", new BsonArray(sale.getToppings()) }
                };
                documents.Add(document);
            }

            Console.WriteLine("list is ok");
            //add them all to mongo

            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://arielUni:arielUni123@cluster0.ayzwu1i.mongodb.net/?retryWrites=true&w=majority");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("ice_cream_store_mongo");

            var collection = database.GetCollection<BsonDocument>("sales");


            collection.InsertMany(documents);
            //await collection.InsertOneAsync (document);
        }
    }
}