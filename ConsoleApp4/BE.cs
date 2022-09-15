using System.Collections;

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


    class Topping
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

    class Receptacle
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






}