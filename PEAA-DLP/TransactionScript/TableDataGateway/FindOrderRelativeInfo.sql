USE Northwind
SELECT [Order Details].OrderID, [Order Details].Quantity, [Products].ProductName, [Products].UnitsInStock, [Products].UnitsOnOrder
FROM [Order Details], [Products]
WHERE [Order Details].ProductID = [Products].ProductID
AND [Order Details].OrderID = 10435