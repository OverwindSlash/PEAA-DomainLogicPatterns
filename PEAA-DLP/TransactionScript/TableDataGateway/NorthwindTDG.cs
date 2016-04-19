using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionScript.DbUtil;
using TransactionScript.DTOs;

namespace TransactionScript.TableDataGateway
{
    public static class NorthwindTDG
    {
        private static readonly DbProviderFactory providerFactory = DbSettings.ProviderFactory;

        #region FindOrderDetailById
        private const string cmdFindOrderDetailById =
            "SELECT * FROM [Order Details] " +
            "WHERE [Order Details].OrderID = @OrderID";

        public static IEnumerable<OrderDetailDto> FindOrderDetailById(int orderId)
        {
            using (IDbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = DbSettings.ConnectionString;
                connection.Open();

                IDbCommand command = providerFactory.CreateCommand();
                command.Connection = connection;
                command.CommandText = cmdFindOrderDetailById;

                IDbDataParameter parameter = providerFactory.CreateParameter();
                parameter.ParameterName = "@OrderID";
                parameter.DbType = DbType.Int32;
                parameter.Value = orderId;
                command.Parameters.Add(parameter);

                IDataReader reader = command.ExecuteReader();
                int AffectedRows = reader.RecordsAffected;

                while (reader.Read())
                {
                    yield return CreateOrderDetailDto((IDataRecord)reader);
                }
            }
        }

        private static OrderDetailDto CreateOrderDetailDto(IDataRecord dataRecord)
        {
            OrderDetailDto orderDetailDto = new OrderDetailDto();

            orderDetailDto.OrderID = dataRecord.GetInt32("OrderID");
            orderDetailDto.ProductID = dataRecord.GetInt32("ProductID");
            orderDetailDto.Quantity = dataRecord.GetInt16("Quantity");
            orderDetailDto.Discount = dataRecord.GetFloat("Discount");

            return orderDetailDto;
        }
        #endregion

        #region FindProductById
        private const string cmdFindProductById =
            "SELECT * FROM [Products] " +
            "WHERE [Products].ProductID = @ProductID";

        public static IEnumerable<ProductDto> FindProductById(int productId)
        {
            using (IDbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = DbSettings.ConnectionString;
                connection.Open();

                IDbCommand command = providerFactory.CreateCommand();
                command.Connection = connection;
                command.CommandText = cmdFindProductById;

                IDbDataParameter parameter = providerFactory.CreateParameter();
                parameter.ParameterName = "@ProductID";
                parameter.DbType = DbType.Int32;
                parameter.Value = productId;
                command.Parameters.Add(parameter);

                IDataReader reader = command.ExecuteReader();
                int AffectedRows = reader.RecordsAffected;

                while (reader.Read())
                {
                    yield return CreateProductDto((IDataRecord)reader);
                }
            }
        }

        private static ProductDto CreateProductDto(IDataRecord dataRecord)
        {
            ProductDto productDto = new ProductDto();

            productDto.ProductID = dataRecord.GetInt32("ProductID");
            productDto.ProductName = dataRecord.GetStringOrNull("ProductName");
            productDto.CategoryID = dataRecord.GetInt32("CategoryID");

            return productDto;
        }
        #endregion

        #region FindCategoryById
        private const string cmdFindCategoryById =
            "SELECT * FROM [Categories] " +
            "WHERE [Categories].CategoryID = @CategoryID";

        public static IEnumerable<CategoryDto> FindCategoryById(int categoryId)
        {
            using (IDbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = DbSettings.ConnectionString;
                connection.Open();

                IDbCommand command = providerFactory.CreateCommand();
                command.Connection = connection;
                command.CommandText = cmdFindCategoryById;

                IDbDataParameter parameter = providerFactory.CreateParameter();
                parameter.ParameterName = "@CategoryID";
                parameter.DbType = DbType.Int32;
                parameter.Value = categoryId;
                command.Parameters.Add(parameter);

                IDataReader reader = command.ExecuteReader();
                int AffectedRows = reader.RecordsAffected;

                while (reader.Read())
                {
                    yield return CreateCategoryDto((IDataRecord)reader);
                }
            }
        }

        private static CategoryDto CreateCategoryDto(IDataRecord dataRecord)
        {
            CategoryDto categoryDto = new CategoryDto();

            categoryDto.CategoryID = dataRecord.GetInt32("CategoryID");
            categoryDto.CategoryName = dataRecord.GetStringOrNull("CategoryName");

            return categoryDto;
        } 
        #endregion

        #region FindOrderRelativeInfo
        private const string cmdFindOrderRelativeInfo =
            "SELECT [Order Details].OrderID, [Products].ProductName, [Order Details].Quantity, [Categories].CategoryID, [Categories].CategoryName " +
            "FROM [Order Details], [Products], [Categories] " +
            "WHERE [Order Details].ProductID = [Products].ProductID AND [Products].CategoryID = [Categories].CategoryID " +
            "AND [Order Details].OrderID = @OrderID";

        public static IEnumerable<OrderRelativeInfoDto> FindOrderRelativeInfo(int orderId)
        {
            using (IDbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = DbSettings.ConnectionString;
                connection.Open();

                IDbCommand command = providerFactory.CreateCommand();
                command.Connection = connection;
                command.CommandText = cmdFindOrderRelativeInfo;

                IDbDataParameter parameter = providerFactory.CreateParameter();
                parameter.ParameterName = "@OrderID";
                parameter.DbType = DbType.Int32;
                parameter.Value = orderId;
                command.Parameters.Add(parameter);

                IDataReader reader = command.ExecuteReader();
                int AffectedRows = reader.RecordsAffected;

                while (reader.Read())
                {
                    yield return CreateOrderRelativeInfoDto((IDataRecord)reader);
                }
            }
        }

        private static OrderRelativeInfoDto CreateOrderRelativeInfoDto(IDataRecord dataRecord)
        {
            OrderRelativeInfoDto orderRelativeInfoDto = new OrderRelativeInfoDto();

            orderRelativeInfoDto.OrderID = dataRecord.GetInt32("OrderID");
            orderRelativeInfoDto.ProductName = dataRecord.GetStringOrNull("ProductName");
            orderRelativeInfoDto.Quantity = dataRecord.GetInt16("Quantity");
            orderRelativeInfoDto.CategoryID = dataRecord.GetInt32OrNull("CategoryID");
            orderRelativeInfoDto.CategoryName = dataRecord.GetStringOrNull("CategoryName");

            return orderRelativeInfoDto;
        }
        #endregion

        #region FindAllEmployees
        private const string cmdFindAllEmployees =
            "SELECT * FROM [Employees]";

        public static IEnumerable<EmployeeDto> FindAllEmployees()
        {
            using (IDbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = DbSettings.ConnectionString;
                connection.Open();

                IDbCommand command = providerFactory.CreateCommand();
                command.Connection = connection;
                command.CommandText = cmdFindAllEmployees;

                IDataReader reader = command.ExecuteReader();
                int AffectedRows = reader.RecordsAffected;

                while (reader.Read())
                {
                    yield return CreateEmployeeDto((IDataRecord)reader);
                }
            }
        }

        private static EmployeeDto CreateEmployeeDto(IDataRecord dataRecord)
        {
            EmployeeDto employeeDto = new EmployeeDto();

            employeeDto.EmployeeID = dataRecord.GetInt32("EmployeeID");
            employeeDto.FirstName = dataRecord.GetStringOrNull("FirstName");
            employeeDto.LastName = dataRecord.GetStringOrNull("LastName");
            employeeDto.Fullname = employeeDto.FirstName + " " + employeeDto.LastName;
            employeeDto.Title = dataRecord.GetStringOrNull("Title");

            return employeeDto;
        }
        #endregion

        #region FindOrdersByEmployeeID
        private const string cmdFindOrdersByEmployeeID =
            "SELECT * FROM [Orders] " +
            "WHERE [Orders].EmployeeId = @EmployeeId";

        public static IEnumerable<OrderDto> FindOrdersByEmployeeID(int employeeId)
        {
            using (IDbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = DbSettings.ConnectionString;
                connection.Open();

                IDbCommand command = providerFactory.CreateCommand();
                command.Connection = connection;
                command.CommandText = cmdFindOrdersByEmployeeID;

                IDbDataParameter parameter = providerFactory.CreateParameter();
                parameter.ParameterName = "@EmployeeId";
                parameter.DbType = DbType.Int32;
                parameter.Value = employeeId;
                command.Parameters.Add(parameter);


                IDataReader reader = command.ExecuteReader();
                int AffectedRows = reader.RecordsAffected;

                while (reader.Read())
                {
                    yield return CreateOrderDto((IDataRecord)reader);
                }
            }
        }

        private static OrderDto CreateOrderDto(IDataRecord dataRecord)
        {
            OrderDto orderDto = new OrderDto();

            orderDto.OrderID = dataRecord.GetInt32("OrderID");
            orderDto.EmployeeID = dataRecord.GetInt32("EmployeeID");

            return orderDto;
        } 
        #endregion
    }
}
