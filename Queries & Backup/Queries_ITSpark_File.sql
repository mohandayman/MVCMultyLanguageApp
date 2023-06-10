Create Database SchoolSystem ;
use SchoolSystem

CREATE TABLE Student (
   ID int primary key  IDENTITY(1,1),
[Name] varchar(200) not null , 
Gender varchar (10) not null, 
City varchar (30) not null, 
BirthDate Date not null ,

);







