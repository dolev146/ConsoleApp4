using System.Collections;
//added for mongo semi-generated class
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BusinessEntities
{

    // data holder classes (theoreticaly may be a struct ?)
    class Taste
    {
        int id;
        string name;

        public Taste(string name)
        {
            this.name = name;
        }

        public void setID(int id) { this.id = id; }
        public string getName() { return name; }
        public int getID() { return id; }

        public override string ToString()
        {
            return base.ToString() + "name : " + name + " , id:" + id;
        }
    }

    class Sale
    {
        int id;
        int rid;
        DateTime dateTime;
        bool completed;
        bool paid;
        int total_price;
        public Sale(int rid, DateTime dateTime, bool completed, bool paid, int total_price)
        {
            this.rid = rid;
            this.dateTime = dateTime;
            this.completed = completed;
            this.paid = paid;
            this.total_price = total_price;
        }
        public void setID(int id) { this.id = id; }
        public int getrid() { return rid; }
        public int getId() { return id; }
        public int getTotalPrice() { return total_price; }
        public string getDateTime()
        {
            string formatForMySql = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
            return formatForMySql;
        }
        public bool getCompleted() { return completed; }
        public bool getPaid() { return paid; }


        public override string ToString()
        {
            return base.ToString() + "rid: " + rid + " ,total_price: " + total_price +
                " ,dateTime: " + dateTime +
                " , completed: " + completed +
                " , paid: " + paid;
        }
    }


    public class Topping
    {
        string name;
        int id;

        public Topping(string name)
        {
            this.name = name;
        }


        public string getName() { return name; }
        public void setID(int id) { this.id = id; }

        public override string ToString()
        {
            return base.ToString() + "name: " + name + " ,id: " + id;
        }
    }

    public class Receptacle
    {
        int id;
        string name;
        int price;

        public Receptacle(string name, int price)
        {
            this.name = name;
            this.price = price;
        }

        public void setID(int id) { this.id = id; }
        public string getName() { return name; }
        public int getID() { return id; }

        public int getPrice() { return price; }

        public override string ToString()
        {
            return base.ToString() + "name : " + name + " , id:" + id + ", price: " + price;
        }
    }
    class Taste_Sale
    {
        int sid;
        int tid;
        int quantity;

        public Taste_Sale(int sid, int tid, int quantity)
        {
            this.sid = sid;
            this.tid = tid;
            this.quantity = quantity;
        }


        public int getSaleId() { return sid; }
        public int getTasteId() { return tid; }
        public int getQuantity() { return quantity; }

        public override string ToString()
        {
            string r = "quantity: " + quantity + " , tid: " + tid + " , sid: " + sid;
            return r;
        }
    }


    class Topping_Sale
    {
        int sid;
        int topid;

        public Topping_Sale(int sid, int topid)
        {
            this.sid = sid;
            this.topid = topid;
        }


        public int getSaleId() { return sid; }
        public int getToppingId() { return topid; }

        public override string ToString()
        {
            string r = "tid: " + topid + " , sid: " + sid;
            return r;
        }
    }


    public class TasteQuantity
    {
        // this class hold json of tasteName quantity objects {"tasteName":quantity}
        public string tasteName { get; set; }
        public int quantity { get; set; }

        // get tasteName
        public string getTasteName() { return tasteName; }
        // get quantity
        public int getQuantity() { return quantity; }

        public override string ToString()
        {
            return base.ToString() + "tasteName: " + tasteName + " , quantity: " + quantity;
        }

        // constructor
        public TasteQuantity(string tasteName, int quantity)
        {
            this.tasteName = tasteName;
            this.quantity = quantity;
        }

    }




    // create new class for mongo sale db
    // id need to be a string
    public class MongoSale
    {
        string id;
        int rid;
        string recepcleName;
        // hold a Receptcle type
        public Receptacle receptacle;
        // hold a list of objects that are "tasteName" : "quantity" Taste's and quantity of the taste
        public List<TasteQuantity> tastesQuantityArray;
        // hold a list of toppings names
        public ArrayList toppings;

        public DateTime dateTime;
        bool completed;
        bool paid;
        int total_price;


        public MongoSale(int rid, string recepcleName, DateTime dateTime, bool completed, bool paid, int total_price, List<TasteQuantity> tastesQuantityArray, ArrayList toppings)
        {
            this.toppings = toppings;
            this.tastesQuantityArray = tastesQuantityArray;
            this.rid = rid;
            this.receptacle = new Receptacle(recepcleName, rid);
            this.dateTime = dateTime;
            this.completed = completed;
            this.paid = paid;
            this.total_price = total_price;
        }

        // default constructor
        public MongoSale()
        {

        }

        // set taste list
        public void settastesQuantityArray(List<TasteQuantity> tastesQuantityArray) { this.tastesQuantityArray = tastesQuantityArray; }
        // set topping list
        public void setToppings(ArrayList toppings) { this.toppings = toppings; }
        // set receptacle
        public void SetReceptacle(Receptacle receptacle) { this.receptacle = receptacle; }
        // get receptacle
        public Receptacle GetReceptacle() { return receptacle; }
        // get taste list
        public List<TasteQuantity> getTastesQuantityArray() { return tastesQuantityArray; }
        // get topping list
        public ArrayList getToppings() { return toppings; }

        public void setID(string id) { this.id = id; }
        public int getrid() { return rid; }
        public string getId() { return id; }
        public int getTotalPrice() { return total_price; }
        public string getDateTime()
        {
            string formatForMySql = dateTime.ToString("yyyy-MM-dd HH:mm:ss");
            return formatForMySql;
        }

        public void setCompleted(bool completed)
        {
            this.completed = completed;
        }

        public void setPaid(bool paid)
        {
            this.paid = paid;
        }

        public void setDate(DateTime dt)
        {
            this.dateTime = dt;
        }

        public void addTasteQuantity(TasteQuantity tq)
        {
            // add to tastesQuantityArray
            tastesQuantityArray.Add(tq);
        }

        public void addTopping(Topping t)
        {
            // add to tastesQuantityArray
            toppings.Add(t);
        }

        public bool getCompleted() { return completed; }
        public bool getPaid() { return paid; }

        public DateTime getDate(){
            return dateTime;
        }

        public override string ToString()
        {
            string r = "id: " + id + " , rid: " + rid + " , recepcleName: " + recepcleName + " , dateTime: " + dateTime + " , completed: " + completed + " , paid: " + paid + " , total_price: " + total_price + " , tastesQuantityArray: " + tastesQuantityArray + " , toppings: " + toppings;
            return r;
        }

        public void calculateTotalPrice(){
            // if regualar cone then add is 0
            // if special cone then add is 2
            // if box then add is 5
            int add = 0;
            if (receptacle.getName().Equals("regular cone"))
            {
                add = 0;
            }
            else if (receptacle.getName().Equals("special cone"))
            {
                add = 2;
            }
            else if (receptacle.getName().Equals("box"))
            {
                add = 5;
            }

            // one ball of ice cream cost 7
            // two balls of ice cream cost 12
            // 3 balls of ice cream cost 18
            // and for every ball after that add more 6
            int iceCreamPrice = 0;
            int iceCreamQuantity = 0;
            foreach (TasteQuantity tq in tastesQuantityArray)
            {
                iceCreamQuantity += tq.quantity;
            }
            if (iceCreamQuantity == 1)
            {
                iceCreamPrice = 7;
            }
            else if (iceCreamQuantity == 2)
            {
                iceCreamPrice = 12;
            }
            else if (iceCreamQuantity == 3)
            {
                iceCreamPrice = 18;
            }
            else if (iceCreamQuantity > 3)
            {
                iceCreamPrice = 18 + ((iceCreamQuantity - 3) * 6);
            }

            this.total_price = iceCreamPrice + add;

        }

    }
}