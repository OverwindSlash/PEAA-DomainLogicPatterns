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
    public class CategoryTDG : AbstractTDG
    {
        private int categoryId;

        protected override string GetExecuteReaderSql()
        {
            return "SELECT * FROM [Categories] " +
                   "WHERE [Categories].CategoryID = @CategoryID";
        }

        protected override void PrepareCommandParameters(IDbCommand command)
        {
            IDbDataParameter parameter = providerFactory.CreateParameter();
            parameter.ParameterName = "@CategoryID";
            parameter.DbType = DbType.Int32;
            parameter.Value = categoryId;
            command.Parameters.Add(parameter);
        }

        public IEnumerable<CategoryDto> FindCategoryById(int categoryId)
        {
            this.categoryId = categoryId;

            IList<CategoryDto> categoryDtos = new List<CategoryDto>();

            foreach (IDataRecord dataRecord in ExecuteReaderById(categoryId))
            {
                categoryDtos.Add(CreateCategoryDto(dataRecord));
            }

            return categoryDtos;
        }

        private CategoryDto CreateCategoryDto(IDataRecord dataRecord)
        {
            CategoryDto categoryDto = new CategoryDto();

            categoryDto.CategoryID = dataRecord.GetInt32("CategoryID");
            categoryDto.CategoryName = dataRecord.GetStringOrNull("CategoryName");

            return categoryDto;
        }
    }
}
