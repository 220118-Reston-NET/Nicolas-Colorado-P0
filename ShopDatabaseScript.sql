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
	StoreFrontLocation varchar(50),
	TotalPrice float
)

create table Product
(
	productID int identity(1,1) primary key,
	Name varchar(50),
	Price float,
	Category varchar(50),
	Quantity int
)

insert Product (Name, Price, Category, Quantity)
values ('Men''s Goldlink Chain', 290.00, 'Jewelery', 10),
	   ('Elsa PerettiDiamond Bracelet', 575.00, 'Jewelry', 3),
	   ('Sterling Silver Mood Ring', 32.95, 'Jewelry', 40),
	   ('Rogue RA-090 Dreadnought Acoustic Guitar', 89.99, 'Electronics and Musical Instruments', 4),
	   ('Mitchell MS450 Single-Cutaway Electric Guitar', 399.99, 'Electronics and Musical Instruments', 4),
	   ('Insignia CD Boombox', 39.99, 'Electronics and Musical Instruments', 12),
	   ('Samsung USB-C Data Charging Cable', 6.99, 'Electronics and Musical Instruments', 30),
	   ('Pokemon: Trading Card Game Rayquaza Battle Deck', 14.99,  'Toys and Games', 20),
	   ('Sony PlayStation 3 500 GB System', 217.99, 'Toys and Games', 5),
	   ('Nerf Rival Hypnos XIX-1200', 52.99, 'Toys and Games', 6),
	   ('Belgian Chocolate Cake', 24.99, 'Produce and Baked Goods', 5),
	   ('Golden Apple', 1.50, 'Produce and Baked Goods', 50),
	   ('Chiquita Bananas 2-lb', 4.39, 'Produce and Baked Goods', 25),
	   ('Krispy Kreme Original Glazed Dozen Box', 7.99, 'Produce and Baked Goods', 9),
	   ('Women''s Floral Dress', 39.99, 'Clothing', 10),
	   ('Men''s Cotton Sweatpants', 27.96, 'Clothing', 8),
	   ('Children''s One-Piece Pajama Set', 18.50, 'Clothing', 11),
	   ('Everlast MMA Gloves', 34.99, 'Sports Equipment', 2),
	   ('Adidas MLS Club Soccer Ball', 19.99, 'Sports Equipment', 7),
	   ('CAP Barbell Neoprene 16-lb Dumbbell', 16.99, 'Sports Equipment', 14)
	   
	   
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
            
alter table 


--Begin the inner join process to create relationships (One-to-many relationship)
-- Customer + Orders
alter table Orders 
add customerID int foreign key references Customer(customerID)

select * from Customer c 
inner join Orders o on c.customerID = o.customerID

-- Orders + StoreFront
alter table Orders
add storeID int foreign key references StoreFront(storeID)

select * from StoreFront sf 
inner join Orders o on sf.storeID = o.storeID


---Error fixing after adding Products
drop table LineItems 

drop table Inventory 

drop table Product 






--(Many-to-Many Relationships)
-- Orders + Products = Lineitems
create table LineItems --This table is now known as LineItems
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
	storeID int foreign key references Storefront(storeID),
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

select o.orderID, o.storeID, o.StoreFrontLocation, o.TotalPrice from Customer c 
inner join ViewOrder vo on c.customerID = vo.customerID 
inner join Orders o on o.orderID = vo.orderID 



--Show orders made under a store
--StoreFront + orders = ViewStoreOrder

create table ViewStoreOrder
(
	storeID int foreign key references StoreFront(storeID),
	orderID int foreign key references Orders(orderID)
)

select o.orderID, o.customerID, o.StoreFrontLocation, o.TotalPrice from StoreFront sf 
inner join ViewStoreOrder vso on sf.storeID = vso.storeID 
inner join Orders o on o.orderID = vso.orderID 

------select max(e.empId) from Employee e



