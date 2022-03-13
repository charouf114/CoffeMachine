# CoffeMachine
The Project is composed on

Client Side : CoffeApp Created on WPF

Server Side : CoffeServices Creaded on ASP.Net Core 3.1

The DB Schema should be like this

CREATE TABLE Drink.dbo.Drink (
  BudgetId varchar(256) NOT NULL,
  DrinkType int NOT NULL,
  SucreCount int NULL,
  HasMug bit NULL,
  CreationDate date NULL,
  CONSTRAINT PK_BudgetId PRIMARY KEY CLUSTERED (BudgetId)
)

