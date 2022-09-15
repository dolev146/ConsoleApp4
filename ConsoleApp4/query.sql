DROP DATABASE IF EXISTS ice_cream_store;
CREATE DATABASE IF NOT EXISTS ice_cream_store;
USE ice_cream_store; 

CREATE TABLE `Tastes` (
    `tid` INTEGER NOT NULL AUTO_INCREMENT primary key,
    `name`  VARCHAR(20) NOT NULL,
	UNIQUE (name)
);
CREATE TABLE `Toppings` ( 
	`topid` INTEGER NOT NULL AUTO_INCREMENT primary key,
	`name`  VARCHAR(20) NOT NULL,
    UNIQUE (name)
);
CREATE TABLE `Receptacles` ( 
	`rid` INTEGER NOT NULL AUTO_INCREMENT primary key,
	`name`  VARCHAR(20) NOT NULL,
	`price` int NOT NULL,
    UNIQUE (name)
);

CREATE TABLE `Sales` ( 
	`sid` INTEGER NOT NULL AUTO_INCREMENT primary key,
    `rid` integer not null,
    foreign key (`rid`) references Receptacles(`rid`),
	`datetime` datetime not null, 
    `completed` bool not null,
    `paid` bool not null,
`total_price` INTEGER NOT NULL
);

CREATE TABLE `Tastes_Sales` ( 
	`sid` integer not null,
	foreign key (`sid`) references Sales(`sid`),
	`tid` integer not null,
	foreign key (`tid`) references Tastes(`tid`),
    PRIMARY KEY (`sid` , `tid`),
    `quantity` integer not null
);

CREATE TABLE `Toppings_Sales` ( 
	`sid` integer not null,
	foreign key (`sid`) references Sales(`sid`),
	`topid` integer not null,
	foreign key (`topid`) references Toppings(`topid`),
    PRIMARY KEY (`sid` , `topid`)
);




select * from tastes;
select * from Receptacles;
select * from Toppings;
select * from sales;
select * from Toppings_Sales;
select * from Tastes_Sales;


