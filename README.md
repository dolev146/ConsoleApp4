https://github.com/dolev146/ConsoleApp4
# Ice Cream Store DB
- [@ Dolev Dublon](https://github.com/dolev146) 207867342
- [@ Yakov Khodorkovski](https://github.com/yakov103) 207045063

erd = entity relationship diagram

![מצגת1_page-0001](https://user-images.githubusercontent.com/62290677/191915019-0e55ae31-68bd-451a-be86-db7c4b46254e.jpg)





## how to run 
1. Download the Visual Studio and .NET 6 library.
2. clone the repository through Visual Studio 
3. Install NUGET packeges of MySQL.data and MongoDB.driver. 
4. Run with at the CLI.

## how to use 
Navigate the menu with numbers : 
// **** photo of the menu **** // 

Choose the wanted DB platform . 
![image](https://user-images.githubusercontent.com/62290677/191916109-cf5499c2-d4cd-44f2-a6ea-8fa9240354f3.png)

then choose the number of request you would like to make


1
will create empty tables
![image](https://user-images.githubusercontent.com/62290677/191919059-56bf9175-1367-4474-b622-df34bc1aec58.png)

![image](https://user-images.githubusercontent.com/62290677/191919012-24bbaadc-1436-408a-b021-3b434f3d3d3e.png)
watch the video for full explenation




### data structure of the database
The program is divided to layers as we learned in the class 

DAL: Database Layer - Includes all the integration between the server and database. 

In this layer we have the following classes:
- MongoAccess - includes all the methods that connect to mongoDB and do the CRUD operations
- SQLAccess - includes all the methods that connect to SQL server and do the CRUD operations
- DataAccess - includes all the methods that connect to the database and do the CRUD operations

BEL: Business Entity Layer - Includes all the entities and classes for data manipulation.
In this layer we have the following classes:
- Tastes - includes all the properties of the taste entity
- Toppings - includes all the properties of the toppings entity
- Receptacles - includes all the properties of the receptacles entity
- Sales - includes all the properties of the sales entity
- Tastes_Sales - includes all the properties of the tastes_sales entity
- Toppings_Sales - includes all the properties of the toppings_sales entity
- MongoAccess - includes all the methods that connect to mongoDB and do the CRUD operations
- SQLAccess - includes all the methods that connect to SQL server and do the CRUD operations
- DataAccess - includes all the methods that connect to the database and do the CRUD operations

BLL: Business Logic Layer - Includes all the management logic of the business. 
In this layer we have the following classes:
- MongoMakeOrder - includes all the methods that make a sale in mongoDB
- MongoEditOrder - includes all the methods that edit a sale in mongoDB
- Logic - includes all the methods that make a sale in SQL server
- Logic - includes all the methods that edit a sale in SQL server
- Logic - includes all the methods that delete a sale in SQL server
- Logic - includes all the methods that read a sale in SQL server
- Logic - includes all the methods that get the day report in SQL server
- Logic - includes all the methods that get the not completed sales in SQL server
- Logic - includes all the methods that get the most common ingredient in SQL server


### DB schema


// structur of the SQL 
we followed this approache 
https://stackoverflow.com/questions/66461298/add-multiple-products-in-one-order-with-the-same-client-id

https://dbdiagram.io/d/6320648a0911f91ba59b015f

![Untitled](https://user-images.githubusercontent.com/62290677/191808189-850e36c4-0b54-4eb1-bf31-ce0a089613e5.png)
```
CREATE TABLE `Tastes` (
  `tid` INTEGER PRIMARY KEY NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(20) UNIQUE NOT NULL
);

CREATE TABLE `Toppings` (
  `topid` INTEGER PRIMARY KEY NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(20) UNIQUE NOT NULL
);

CREATE TABLE `Receptacles` (
  `rid` INTEGER PRIMARY KEY NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(20) UNIQUE NOT NULL,
  `price` int NOT NULL
);

CREATE TABLE `Sales` (
  `sid` INTEGER PRIMARY KEY NOT NULL AUTO_INCREMENT,
  `rid` integer NOT NULL,
  `datetime` datetime NOT NULL,
  `completed` bool NOT NULL,
  `paid` bool NOT NULL,
  `total_price` int NOT NULL
);

CREATE TABLE `Tastes_Sales` (
  `sid` integer NOT NULL,
  `tid` integer NOT NULL,
  `quantity` integer NOT NULL,
  PRIMARY KEY (`sid`, `tid`)
);

CREATE TABLE `Toppings_Sales` (
  `sid` integer NOT NULL,
  `topid` integer NOT NULL,
  PRIMARY KEY (`sid`, `topid`)
);

ALTER TABLE `Sales` ADD FOREIGN KEY (`rid`) REFERENCES `Receptacles` (`rid`);

ALTER TABLE `Tastes_Sales` ADD FOREIGN KEY (`tid`) REFERENCES `Tastes` (`tid`);

CREATE TABLE `Tastes_Sales_Sales` (
  `Tastes_Sales_sid` integer NOT NULL,
  `Sales_sid` INTEGER NOT NULL,
  PRIMARY KEY (`Tastes_Sales_sid`, `Sales_sid`)
);

ALTER TABLE `Tastes_Sales_Sales` ADD FOREIGN KEY (`Tastes_Sales_sid`) REFERENCES `Tastes_Sales` (`sid`);

ALTER TABLE `Tastes_Sales_Sales` ADD FOREIGN KEY (`Sales_sid`) REFERENCES `Sales` (`sid`);


ALTER TABLE `Toppings_Sales` ADD FOREIGN KEY (`sid`) REFERENCES `Sales` (`sid`);

ALTER TABLE `Toppings_Sales` ADD FOREIGN KEY (`topid`) REFERENCES `Toppings` (`topid`);


```


// stucute of the MongoDB 

![jsoncrack com](https://user-images.githubusercontent.com/62290677/191808387-f38d18e9-9da5-4679-84bf-32a2915aeaa8.png)

```
{
  "Sales": [
    {
      "_id": "string",
      "completed": "bool",
      "paid": "bool",
      "total_price": "int",
      "Recepticle": "string",
      "Taste_quantity": [
        {
          "name": "string",
          "quantity": "int"
        },
        {
          "name": "string",
          "quantity": "int"
        }
      ],
      "Toppings": [
        {
          "name": "toppingName",
          "price": "int"
        },
        {
          "name": "toppingName",
          "price": "int"
        }
      ],
      "dateTime": "dateTime"
    }
  ]
}
```
https://jsoncrack.com/editor?json=%5B%5B%22Sales%22%2C%22a%7C0%22%2C%22_id%22%2C%22completed%22%2C%22paid%22%2C%22total_price%22%2C%22Recepticle%22%2C%22Taste_quantity%22%2C%22Toppings%22%2C%22dateTime%22%2C%22a%7C2%7C3%7C4%7C5%7C6%7C7%7C8%7C9%22%2C%22string%22%2C%22bool%22%2C%22int%22%2C%22name%22%2C%22quantity%22%2C%22a%7CE%7CF%22%2C%22o%7CG%7CB%7CD%22%2C%22a%7CH%7CH%22%2C%22price%22%2C%22a%7CE%7CJ%22%2C%22toppingName%22%2C%22o%7CK%7CL%7CD%22%2C%22a%7CM%7CM%22%2C%22o%7CA%7CB%7CC%7CC%7CD%7CB%7CI%7CN%7C9%22%2C%22a%7CO%22%2C%22o%7C1%7CP%22%5D%2C%22Q%22%5D


more pictures

![image](https://user-images.githubusercontent.com/62290677/191848821-497044b8-8020-4f6a-8086-7fdf06ce9c72.png)
![image](https://user-images.githubusercontent.com/62290677/191848896-43f7e586-e0e9-4c27-b926-8426d3d9730f.png)
![image](https://user-images.githubusercontent.com/62290677/191848950-02d81855-7236-45b8-8cbb-01a013ce65d0.png)
![image](https://user-images.githubusercontent.com/62290677/191849012-2f29a2d2-7ef8-4e12-9b55-ca81e30703f4.png)


