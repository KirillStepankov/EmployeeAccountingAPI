/*1. Необходим скрипт создания таблицы клиентов Clients в которой будет храниться следующая информация: наименование клиента, его адрес. 
	Адрес клиента может быть не известен.*/
CREATE DATABASE ClientsDB
USE ClientsDB
GO

CREATE TABLE Clients (
    Id int NOT NULL,
    [Name] varchar(255) NOT NULL UNIQUE,
    Adres varchar(255),
    PRIMARY KEY (Id)
);

/*2. Необходим скрипт создания таблицы счетов клиентов Accounts в которой будет храниться следующая информация: 
	номер счета, валюта счета, дата открытия, дата закрытия, текущий остаток. Текущий остаток и дата закрытия известны не сразу.*/
CREATE TABLE Accounts (
    Id int NOT NULL,
    AccountNumber nvarchar(20) NOT NULL UNIQUE,
    CurrencyAccount varchar(3) NOT NULL,
	OpeningDate datetime NOT NULL,
	ClosingDate datetime,
	CurrentBalance decimal,
	ClientId int NOT NULL,
    PRIMARY KEY (Id),
	FOREIGN KEY (ClientId) REFERENCES Clients(Id)
);

/*!!!3. По изменениям в данных таблицах должна вестись история (триггеры).*/


/*4. Необходимо написать запрос, который возвращает список клиентов, у которых есть счета и количество этих счетов.*/
SELECT Clients.Id, Clients.[Name], COUNT(Accounts.Id) FROM Clients 
INNER JOIN Accounts 
ON Clients.Id = Accounts.ClientId
GROUP BY Clients.Id, Clients.[Name]

/*5. Необходим написать запрос, который возвращает список клиентов, у которых все счета закрыты.*/
SELECT Clients.Id, Clients.[Name], Clients.Adres  FROM Clients 
INNER JOIN Accounts 
ON Clients.Id = Accounts.ClientId
WHERE Accounts.ClosingDate IS NOT NULL
/*6. Необходимо написать запрос, который возвращает список счетов клиентов, у которых в наименование есть ОАО.*/
SELECT * FROM Accounts 
WHERE ClientId IN (SELECT Id FROM Clients WHERE Clients.[Name] LIKE 'ОАО%')
/*7. Необходимо написать запрос, который для клиентов с наименованиями, определяемыми в условии, проставляет текущий остаток счета равным 0, если он был неизвестен.*/
declare @nameClient varchar(255)
SET @nameClient = 'Клиент3' 
UPDATE Accounts
SET CurrentBalance = 0 
WHERE CurrentBalance IS NULL  
	AND ClientID IN (
		SELECT Id
		FROM Clients
		WHERE [Name] LIKE @nameClient
		) 
/*8. Необходимо написать запрос, который возвращает список незакрытых счетов вместе с наименованием клиента, кому принадлежит этот счет.*/
SELECT	Accounts.AccountNumber as [Номер счета],
		Clients.[Name] as [Клиент]
FROM Accounts LEFT JOIN Clients
	ON Accounts.ClientId = Clients.Id
WHERE Accounts.ClosingDate IS NULL

/*9. Имеется две таблицы Т1 и Т2. Каждая имеет по одному столбцу F1.
	В таблице Т1 хранится 10 строк. В таблице Т2 хранится 7 строк.
	Необходимо написать запрос, который вернет только те значения столбца F1, которые есть в обеих таблицах.*/
CREATE DATABASE TFDB

USE TFDB
GO

CREATE TABLE T1 (
F1 INT NOT NULL
)

INSERT T1 (F1) VALUES(1)
INSERT T1 (F1) VALUES(2)
INSERT T1 (F1) VALUES(3)
INSERT T1 (F1) VALUES(4)
INSERT T1 (F1) VALUES(5)
INSERT T1 (F1) VALUES(6)
INSERT T1 (F1) VALUES(7)
INSERT T1 (F1) VALUES(8)
INSERT T1 (F1) VALUES(9)
INSERT T1 (F1) VALUES(10)

CREATE TABLE T2 (
F1 INT NOT NULL
)

INSERT T2 (F1) VALUES(1)
INSERT T2 (F1) VALUES(3)
INSERT T2 (F1) VALUES(6)
INSERT T2 (F1) VALUES(7)
INSERT T2 (F1) VALUES(8)
INSERT T2 (F1) VALUES(11)
INSERT T2 (F1) VALUES(15)

SELECT * FROM T1 
INNER JOIN T2 
ON T1.F1 = T2.F1
