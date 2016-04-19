using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableModule.DbUtil;
using TableModule.DTOs;

namespace TableModule.TableDataGateway
{
    public class OrderDetailTDG : AbstractTDG
    {
        private int orderId;

        protected override string GetExecuteReaderSql()
        {
            return "SELECT * FROM [Order Details] " +
            "WHERE [Order Details].OrderID = @OrderID";
        }

        protected override void PrepareCommandParameters(IDbCommand command)
        {
            IDbDataParameter parameter = providerFactory.CreateParameter();
            parameter.ParameterName = "@OrderID";
            parameter.DbType = DbType.Int32;
            parameter.Value = this.orderId;
            command.Parameters.Add(parameter);
        }

        public IEnumerable<OrderDetailDto> FindOrderDetailById(int orderId)
        {
            this.orderId = orderId;

            IList<OrderDetailDto> orderDetailDtos = new List<OrderDetailDto>();

            foreach (IDataRecord dataRecord in ExecuteReaderById(orderId))
            {
                orderDetailDtos.Add(CreateOrderDetailDto(dataRecord));
            }

            return orderDetailDtos;
        }

        private OrderDetailDto CreateOrderDetailDto(IDataRecord dataRecord)
        {
            OrderDetailDto orderDetailDto = new OrderDetailDto();

            orderDetailDto.OrderID = dataRecord.GetInt32("OrderID");
            orderDetailDto.ProductID = dataRecord.GetInt32("ProductID");
            orderDetailDto.Quantity = dataRecord.GetInt16("Quantity");
            orderDetailDto.Discount = dataRecord.GetFloat("Discount");

            return orderDetailDto;
        }
    }
}
