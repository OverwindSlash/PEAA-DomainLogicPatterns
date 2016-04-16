USE Northwind
SELECT [Employees].FirstName, [Employees].LastName, COUNT([Orders].OrderID)
FROM [Employees], [Orders]
WHERE [Orders].EmployeeID = [Employees].EmployeeID
GROUP BY [Employees].EmployeeID, [Employees].FirstName, [Employees].LastName