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
    public class ProductMapper : AbstractMapper
    {
        protected override string FindSingleStatement()
        {
            return "SELECT * FROM [Products] " +
                   "WHERE [Products].ProductID = @ProductID";
        }

        protected override string FindManyStatement()
        {
            throw new NotImplementedException();
        }

        protected override string GetUniqueId(IDataRecord record)
        {
            return record.GetInt32("ProductID").ToString();
        }

        protected override DomainObject doLoad(string uniqueId, IDataRecord record)
        {
            Product product = new Product();

            product.ProductID = record.GetInt32("ProductID");
            product.ProductName = record.GetStringOrNull("ProductName");
            product.CategoryID = record.GetInt32("CategoryID");

            return product;
        }

        public Product FindProductById(int productId)
        {
            ParameterSource parameterSource = CreateParameterSourceOfFindProductById(productId);

            return (Product)AbstractFindSingleWithParameterSource(typeof(Product), parameterSource);
        }

        private ParameterSource CreateParameterSourceOfFindProductById(int productId)
        {
            Criterion productCriterion = new Criterion
            {
                IsKeyParam = true,
                ParamName = "@ProductID",
                ParamType = DbType.Int32,
                ParamValue = productId
            };

            ParameterSource parameterSource = new ParameterSource();
            parameterSource.AddCriterion(productCriterion);

            return parameterSource;
        }
    }
}
