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
    public class CategoryTM : AbstractTableModule
    {
        public override string TableName
        {
            get { return "Category"; }
        }

        public override string IdColumnName
        {
            get { return "CategoryID"; }
        }

        public CategoryTM(DataSetHolder holder) : base(holder)
        {
        }

        public void LoadCategoryById(int categoryId)
        {
            CategoryTDG categoryTDG = new CategoryTDG();
            List<CategoryDto> categoryDtos = categoryTDG.FindCategoryById(categoryId).ToList();

            DataTable categoryTable = new DataTable();
            categoryTable.TableName = "Category";

            Type categoryType = typeof(CategoryDto);
            PropertyInfo[] propertyInfos = categoryType.GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                DataColumn dataColumn = new DataColumn();
                dataColumn.ColumnName = propertyInfo.Name;
                dataColumn.DataType = propertyInfo.PropertyType;
                categoryTable.Columns.Add(dataColumn);
            }

            foreach (CategoryDto categoryDto in categoryDtos)
            {
                DataRow newRow = categoryTable.NewRow();

                foreach (PropertyInfo propertyInfo in propertyInfos)
                {
                    newRow[propertyInfo.Name] = propertyInfo.GetValue(categoryDto);
                }

                categoryTable.Rows.Add(newRow);
            }

            Holder.AddTable(categoryTable);
        }
    }
}
