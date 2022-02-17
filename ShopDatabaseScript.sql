---The core tables created for Project Zero.

create table Customer
(
	customerID int identity(1,1) primary key,
	Name varchar(50),
	Address varchar(50),
	Email varchar(50),
	Phone varchar(50)
)

create table Orders
(
	orderID int identity(1,1) primary key,
	TotalPrice float
)

drop table Orders 

create table Product
(
	productID int identity(1,1) primary key,
	Name varchar(50),
	Price float,
	Category varchar(50)
)

insert Product (Name, Price, Category)
values ('Men''s Goldlink Chain', 290.00, 'Jewelery'),
	   ('Elsa PerettiDiamond Bracelet', 575.00, 'Jewelry'),
	   ('Sterling Silver Mood Ring', 32.95, 'Jewelry'),
	   ('Rogue RA-090 Dreadnought Acoustic Guitar', 89.99, 'Electronics and Musical Instruments'),
	   ('Mitchell MS450 Single-Cutaway Electric Guitar', 399.99, 'Electronics and Musical Instruments'),
	   ('Insignia CD Boombox', 39.99, 'Electronics and Musical Instruments'),
	   ('Samsung USB-C Data Charging Cable', 6.99, 'Electronics and Musical Instruments'),
	   ('Pokemon: Trading Card Game Rayquaza Battle Deck', 14.99,  'Toys and Games'),
	   ('Sony PlayStation 3 500 GB System', 217.99, 'Toys and Games'),
	   ('Nerf Rival Hypnos XIX-1200', 52.99, 'Toys and Games'),
	   ('Belgian Chocolate Cake', 24.99, 'Produce and Baked Goods'),
	   ('Golden Apple', 1.50, 'Produce and Baked Goods'),
	   ('Chiquita Bananas 2-lb', 4.39, 'Produce and Baked Goods'),
	   ('Krispy Kreme Original Glazed Dozen Box', 7.99, 'Produce and Baked Goods'),
	   ('Women''s Floral Dress', 39.99, 'Clothing'),
	   ('Men''s Cotton Sweatpants', 27.96, 'Clothing'),
	   ('Children''s One-Piece Pajama Set', 18.50, 'Clothing'),
	   ('Everlast MMA Gloves', 34.99, 'Sports Equipment'),
	   ('Adidas MLS Club Soccer Ball', 19.99, 'Sports Equipment'),
	   ('CAP Barbell Neoprene 16-lb Dumbbell', 16.99, 'Sports Equipment')
	   
insert into Inventory  
values (1,1,10),
	   (1,2,3),
	   (1,3,40),
	   (1,4,4),
	   (1,5,4),
	   (1,6,12),
	   (1,7,30),
	   (1,8,20),
	   (1,9,5),
	   (1,10,6),
	   (1,11,5),
	   (1,12,50),
	   (1,13,25),
	   (1,14,9),
	   (1,15,10),
	   (1,16,8),
	   (1,17,11),
	   (1,18,2),
	   (1,19,7),
	   (1,20,14)
	   
	   
create table StoreFront 
(
	storeID int identity(1,1) primary key,
	Name varchar(50),
	Address varchar(50),
	Phone varchar(50)
)

--Tables with identity keys don't need to include the ID when inserting values.
insert StoreFront (Name, Address, Phone)
values ('Colorado''s Market', '421 Cone Spiral Boulevard, Orlando, FL', '407-123-4567')     
            


--Begin the inner join process to create relationships (One-to-many relationship)
-- Customer + Orders
alter table Orders 
add customerID int foreign key references Customer(customerID)

-- Orders + StoreFront
alter table Orders
add storeID int foreign key references StoreFront(storeID)

--Undo StoreID Foreign key restraint in Order table 
alter table Orders 
drop constraint FK__Orders__storeID__57DD0BE4;

ALTER TABLE Orders DROP CONSTRAINT fk_city_ref_users;

Unhandled exception. System.Data.SqlClient.SqlException (0x80131904): The INSERT statement conflicted with the FOREIGN KEY constraint "FK__Orders__storeID__40058253". The conflict occurred in database "ShopDB", table "dbo.StoreFront", column 'storeID'.


---Error fixing after adding Products
drop table LineItem 
drop table ViewOrder 
drop table ViewStoreOrder 

--drop table Inventory 

--drop table Product 






--(Many-to-Many Relationships)
-- Orders + Products = Lineitems
create table LineItem --This table is now known as LineItems
(
	orderID int foreign key references Orders(orderID),
	productID int foreign key references Product(productID),
	Quantity int
)

select p.productID, p.Name, li.Quantity from Orders o 
inner join LineItems li  on o.orderID = li.orderID
inner join Product p on p.productID = li.productID


--Storefront + Products = Inventory
create table Inventory 
(
	storeID int foreign key references StoreFront(storeID),
	productID int foreign key references Product(productID),
	Quantity int
)

--alter table Inventory 
--add Quantity int

select p.productID, p.Name, p.Price, p.Category, i.Quantity from StoreFront sf 
inner join Inventory i on sf.storeID = i.storeID
inner join Product p on p.productID = i.productID

select p.productID, p.Name, p.Price, p.Category, i.Quantity from Product p
inner join Inventory i on i.productID = p.productID
inner join StoreFront on sf.storeID = i.productID


--Now, it's time to create tables and SQL statements to enable the View Order and View Inventory functionalities.

--Show orders made by a customer
--Customer + Orders = ViewOrder

create table ViewOrder 
(
	customerID int foreign key references Customer(customerID),
	orderID int foreign key references Orders(orderID)
)


select o.orderID, o.customerID, o.TotalPrice from Customer c 
inner join ViewOrder vo on c.customerID = vo.customerID 
inner join Orders o on o.orderID = vo.orderID 

select c.customerId, o.orderID, o.TotalPrice from Orders o  
inner join Customer c on o.customerID  = c.customerID 

--Show orders made under a store
--StoreFront + orders = ViewStoreOrder

create table ViewStoreOrder
(
	storeID int foreign key references StoreFront(storeID),
	orderID int foreign key references Orders(orderID)
)

select o.orderID, o.storeID, o.customerID, o.TotalPrice from StoreFront sf 
inner join ViewStoreOrder vso on sf.storeID = vso.storeID 
inner join Orders o on o.orderID = vso.orderID 

select o.orderID, o.storeID, o.customerID, o.TotalPrice from Orders o 
inner join ViewStoreOrder vso on o.orderID = vso.orderID 
inner join StoreFront sf on sf.storeID = vso.storeID
where sf.storeID = storeID

------select max(e.empId) from Employee e



