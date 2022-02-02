---The core tables created for Project Zero.

create table Customer
(
	customerID int primary key,
	Name varchar(50),
	Address varchar(50),
	Email varchar(50),
	Phone varchar(50)
)

create table Orders
(
	orderID int primary key,
	StoreFrontLocation varchar(50),
	TotalPrice float
)

create table Product 
(
	productID int primary key,
	Name varchar(50),
	Price float,
	Category varchar(50),
)

create table StoreFront 
(
	storeID int primary key,
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

--Storefront + Products = Inventory
alter table Product 
add storeID int foreign key references Storefront(storeID)

select * from StoreFront sf 
inner join Product p on sf.

alter table Product 
drop storeID int foreign key references Storefront(storeID)

--(Many-to-Many Relationships)
-- Orders + Products = Lineitems
create table orders_products
(
	orderID int foreign key references Orders(orderID),
	productID int foreign key references Product(productID)
)

alter table orders_products 
add Quantity int

select o.orderID, p.Name from Orders o 
inner join orders_products op on o.orderID = op.orderID
inner join Product p on p.productID = op.productID

 



