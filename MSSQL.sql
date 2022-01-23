/*1. ��������� ������ �������� ������� �������� Clients � ������� ����� ��������� ��������� ����������: ������������ �������, ��� �����. 
	����� ������� ����� ���� �� ��������.*/
CREATE DATABASE ClientsDB
USE ClientsDB
GO

CREATE TABLE Clients (
    Id int NOT NULL,
    [Name] varchar(255) NOT NULL UNIQUE,
    Adres varchar(255),
    PRIMARY KEY (Id)
);

/*2. ��������� ������ �������� ������� ������ �������� Accounts � ������� ����� ��������� ��������� ����������: 
	����� �����, ������ �����, ���� ��������, ���� ��������, ������� �������. ������� ������� � ���� �������� �������� �� �����.*/
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

/*!!!3. �� ���������� � ������ �������� ������ ������� ������� (��������).*/


/*4. ���������� �������� ������, ������� ���������� ������ ��������, � ������� ���� ����� � ���������� ���� ������.*/
SELECT Clients.Id, Clients.[Name], COUNT(Accounts.Id) FROM Clients 
INNER JOIN Accounts 
ON Clients.Id = Accounts.ClientId
GROUP BY Clients.Id, Clients.[Name]

/*5. ��������� �������� ������, ������� ���������� ������ ��������, � ������� ��� ����� �������.*/
SELECT Clients.Id, Clients.[Name], Clients.Adres  FROM Clients 
INNER JOIN Accounts 
ON Clients.Id = Accounts.ClientId
WHERE Accounts.ClosingDate IS NOT NULL
/*6. ���������� �������� ������, ������� ���������� ������ ������ ��������, � ������� � ������������ ���� ���.*/
SELECT * FROM Accounts 
WHERE ClientId IN (SELECT Id FROM Clients WHERE Clients.[Name] LIKE '���%')
/*7. ���������� �������� ������, ������� ��� �������� � ��������������, ������������� � �������, ����������� ������� ������� ����� ������ 0, ���� �� ��� ����������.*/
declare @nameClient varchar(255)
SET @nameClient = '������3' 
UPDATE Accounts
SET CurrentBalance = 0 
WHERE CurrentBalance IS NULL  
	AND ClientID IN (
		SELECT Id
		FROM Clients
		WHERE [Name] LIKE @nameClient
		) 
/*8. ���������� �������� ������, ������� ���������� ������ ���������� ������ ������ � ������������� �������, ���� ����������� ���� ����.*/
SELECT	Accounts.AccountNumber as [����� �����],
		Clients.[Name] as [������]
FROM Accounts LEFT JOIN Clients
	ON Accounts.ClientId = Clients.Id
WHERE Accounts.ClosingDate IS NULL

/*9. ������� ��� ������� �1 � �2. ������ ����� �� ������ ������� F1.
	� ������� �1 �������� 10 �����. � ������� �2 �������� 7 �����.
	���������� �������� ������, ������� ������ ������ �� �������� ������� F1, ������� ���� � ����� ��������.*/
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
