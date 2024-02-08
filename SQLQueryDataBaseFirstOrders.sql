DROP TABLE IF EXISTS OrderDetails;
DROP TABLE IF EXISTS Orders;

CREATE TABLE OrderDetails
(
    Id int not null identity primary key,
    ProductId nvarchar(450) not null,
    Quantity int not null,
    UnitPrice decimal(10, 2) not null
);

CREATE TABLE Orders
(
    Id int not null identity primary key,
    CustomerId int not null,
    OrderDetailsId int not null references OrderDetails(Id),
    TotalPrice decimal(10, 2) not null, 
    OrderDate datetime not null
);

