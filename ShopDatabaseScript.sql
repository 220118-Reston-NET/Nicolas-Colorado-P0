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
)

create table StoreFront 
(
	storeID int identity(1,1) primary key,
	Name varchar(50),
	Address varchar(50),
	Phone varchar(50)
)


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



--(Many-to-Many Relationships)
-- Orders + Products = Lineitems
create table LineItems --This table is now known as LineItems
(
	orderID int foreign key references Orders(orderID),
	productID int foreign key references Product(productID),
	Quantity int
)

select o.orderID, p.productID from Orders o 
inner join LineItems li  on o.orderID = li.orderID
inner join Product p on p.productID = li.productID


--Storefront + Products = Inventory
create table Inventory 
(
	storeID int foreign key references Storefront(storeID),
	productID int foreign key references Product(productID),
	Quantity int
)

select sf.storeID, p.productID from StoreFront sf 
inner join Inventory i on sf.storeID = i.storeID
inner join Product p on p.productID = i.productID




