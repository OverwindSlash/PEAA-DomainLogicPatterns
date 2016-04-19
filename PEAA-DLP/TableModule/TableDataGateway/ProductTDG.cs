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
    public class ProductTDG : AbstractTDG
    {
        private int productId;

        protected override string GetExecuteReaderSql()
        {
            return "SELECT * FROM [Products] " +
                   "WHERE [Products].ProductID = @ProductID";
        }

        protected override void PrepareCommandParameters(IDbCommand command)
        {
            IDbDataParameter parameter = providerFactory.CreateParameter();
            parameter.ParameterName = "@ProductID";
            parameter.DbType = DbType.Int32;
            parameter.Value = this.productId;
            command.Parameters.Add(parameter);
        }

        public IEnumerable<ProductDto> FindProductById(int productId)
        {
            this.productId = productId;

            IList<ProductDto> productDtos = new List<ProductDto>();

            foreach (IDataRecord dataRecord in ExecuteReaderById(productId))
            {
                productDtos.Add(CreateProductDto(dataRecord));
            }

            return productDtos;
        }

        private ProductDto CreateProductDto(IDataRecord dataRecord)
        {
            ProductDto productDto = new ProductDto();

            productDto.ProductID = dataRecord.GetInt32("ProductID");
            productDto.ProductName = dataRecord.GetStringOrNull("ProductName");
            productDto.CategoryID = dataRecord.GetInt32("CategoryID");

            return productDto;
        }
    }
}
