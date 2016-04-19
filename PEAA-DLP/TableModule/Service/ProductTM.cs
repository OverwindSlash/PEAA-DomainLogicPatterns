using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TableModule.DTOs;
using TableModule.TableDataGateway;

namespace TableModule.Service
{
    public class ProductTM : AbstractTableModule
    {
        public override string TableName
        {
            get { return "Product"; }
        }

        public override string IdColumnName
        {
            get { return "ProductID"; }
        }

        public ProductTM(DataSetHolder holder) : base(holder)
        {
        }

        public void LoadProductById(int productId)
        {
            ProductTDG productTDG = new ProductTDG();
            List<ProductDto> productDtos = productTDG.FindProductById(productId).ToList();

            DataTable producTable = new DataTable();
            producTable.TableName = "Product";

            Type producType = typeof(ProductDto);
            PropertyInfo[] propertyInfos = producType.GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                DataColumn dataColumn = new DataColumn();
                dataColumn.ColumnName = propertyInfo.Name;
                dataColumn.DataType = propertyInfo.PropertyType;
                producTable.Columns.Add(dataColumn);
            }

            foreach (ProductDto productDto in productDtos)
            {
                DataRow newRow = producTable.NewRow();

                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    newRow[propertyInfo.Name] = propertyInfo.GetValue(productDto);
                }

                producTable.Rows.Add(newRow);
            }

            Holder.AddTable(producTable);
        }
    }
}
