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
                    yield return CreateOrderRelativeInfo((IDataRecord)reader);
                }
            }
        }

        private static OrderRelativeInfoDto CreateOrderRelativeInfo(IDataRecord dataRecord)
        {
            OrderRelativeInfoDto dto = new OrderRelativeInfoDto();

            dto.OrderID = dataRecord.GetInt32("OrderID");
            dto.ProductName = dataRecord.GetStringOrNull("ProductName");
            dto.Quantity = dataRecord.GetInt16("Quantity");
            dto.CategoryID = dataRecord.GetInt32OrNull("CategoryID");
            dto.CategoryName = dataRecord.GetStringOrNull("CategoryName");

            return dto;
        } 
        #endregion
    }
}
