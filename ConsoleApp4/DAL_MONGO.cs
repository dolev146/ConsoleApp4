
using MongoDB.Driver;
using MongoDB.Bson;

using BusinessEntities;
using BusinessLogic;
using System.Collections;

namespace MongoAccess
{
    public class MongoAccess
    {
        // getAllSales
        public static List<MongoSale> getAllSales()
        {
            // connect to mongo
            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://arielUni:arielUni123@cluster0.ayzwu1i.mongodb.net/?retryWrites=true&w=majority");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("ice_cream_store_mongo");
            IMongoCollection<MongoSale> collection = database.GetCollection<MongoSale>("sales");
            // get all the sales
            List<MongoSale> sales = collection.Find(new BsonDocument()).ToList();
            // return the sales
            return sales;
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