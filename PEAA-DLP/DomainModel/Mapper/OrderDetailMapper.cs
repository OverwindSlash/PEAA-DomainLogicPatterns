using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Domain;
using DomainModel.DbUtil;

namespace DomainModel.Mapper
{
    public class OrderDetailMapper : AbstractMapper
    {
        protected override string FindSingleStatement()
        {
            return "SELECT * FROM [Order Details] " +
            "WHERE [Order Details].OrderID = @OrderID " +
            "AND [Order Details].ProductID = @ProductID";
        }

        protected override string FindManyStatement()
        {
            return "SELECT * FROM [Order Details] " +
            "WHERE [Order Details].OrderID = @OrderID";
        }

        protected override string GetUniqueId(IDataRecord record)
        {
            return string.Format("{0}:{1}", record.GetInt32("OrderID"), record.GetInt32("ProductID"));
        }

        protected override DomainObject doLoad(string uniqueId, IDataRecord record)
        {
            OrderDetail orderDetail = new OrderDetail();

            // You can do real mapper logic here.
            orderDetail.OrderID = record.GetInt32("OrderID");
            orderDetail.ProductID = record.GetInt32("ProductID");
            orderDetail.Quantity = record.GetInt16("Quantity");
            orderDetail.Discount = record.GetFloat("Discount");

            return orderDetail;
        }

        public OrderDetail FindOrderDetailById(int orderId, int productId)
        {
            ParameterSource parameterSource = CreateParameterSourceOfFindOrderDetailById(orderId, productId);

            return (OrderDetail)AbstractFindSingleWithParameterSource(typeof (OrderDetail), parameterSource);
        }

        private ParameterSource CreateParameterSourceOfFindOrderDetailById(int orderId, int productId)
        {
            Criterion orderCriterion = new Criterion
            {
                IsKeyParam = true,
                ParamName = "@OrderID",
                ParamType = DbType.Int32,
                ParamValue = orderId
            };

            Criterion productCriterion = new Criterion
            {
                IsKeyParam = true,
                ParamName = "@ProductID",
                ParamType = DbType.Int32,
                ParamValue = productId
            };

            ParameterSource parameterSource = new ParameterSource();
            parameterSource.AddCriterion(orderCriterion);
            parameterSource.AddCriterion(productCriterion);

            return parameterSource;
        }

        public IList<OrderDetail> FindOrderDetailByCriterion(int orderId)
        {
            ParameterSource parameterSource = CreateParameterSourceOfFindOrderDetailByCriterion(orderId);

            IList<DomainObject> domainObjects = AbstractFindManyWithParameterSource(typeof(OrderDetail), parameterSource);

            return domainObjects.OfType<OrderDetail>().ToList();
        }

        private ParameterSource CreateParameterSourceOfFindOrderDetailByCriterion(int orderId)
        {
            Criterion orderCriterion = new Criterion
            {
                IsKeyParam = true,
                ParamName = "@OrderID",
                ParamType = DbType.Int32,
                ParamValue = orderId
            };

            ParameterSource parameterSource = new ParameterSource();
            parameterSource.AddCriterion(orderCriterion);

            return parameterSource;
        }
    }
}
