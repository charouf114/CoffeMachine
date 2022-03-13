# CoffeMachine

The DB should be like this

CREATE TABLE Drink.dbo.Drink (
  BudgetId varchar(256) NOT NULL,
  DrinkType int NOT NULL,
  SucreCount int NULL,
  HasMug bit NULL,
  CreationDate date NULL,
  CONSTRAINT PK_BudgetId PRIMARY KEY CLUSTERED (BudgetId)
)
ON [PRIMARY]
GO
