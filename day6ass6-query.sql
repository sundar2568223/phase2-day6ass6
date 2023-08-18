CREATE DATABASE ProductInventoryDB;
USE ProductInventoryDB;

create table Products(
Pid int primary key,
Pname nvarchar(20),
Price decimal,
Quantity int,MFDate date,
ExpDate date)

drop table Products
SELECT *FROM Products