https://github.com/dolev146/ConsoleApp4
# Ice Cream Store DB
- [@ Dolev Dublon](https://github.com/dolev146) 207867342
- [@ Yakov Khodorkovski](https://github.com/yakov103) 207045063

## how to run 
1. Download the Visual Studio and .NET 6 library.
2. clone the repository through Visual Studio 
3. Install NUGET packeges of MySQL.data and MongoDB.driver. 
4. Run with at the CLI.

## how to use 
Navigate the menu with numbers : 
// **** photo of the menu **** // 

Choose the wanted DB platform . 


## about code 
### data structure 
The program is divided to layers as we learned in the class 

DAL: Database Layer - Includes all the integration between the server and database. 

BE: Business Entity - Includes all the entities and classes for data manipulation. 

BL: Business Logic - Includes all the management logic of the business. 


### DB schema


// structur of the SQL 
we followed this approache 
https://stackoverflow.com/questions/66461298/add-multiple-products-in-one-order-with-the-same-client-id

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


more pictures

![image](https://user-images.githubusercontent.com/62290677/191848821-497044b8-8020-4f6a-8086-7fdf06ce9c72.png)
![image](https://user-images.githubusercontent.com/62290677/191848896-43f7e586-e0e9-4c27-b926-8426d3d9730f.png)
![image](https://user-images.githubusercontent.com/62290677/191848950-02d81855-7236-45b8-8cbb-01a013ce65d0.png)
![image](https://user-images.githubusercontent.com/62290677/191849012-2f29a2d2-7ef8-4e12-9b55-ca81e30703f4.png)


