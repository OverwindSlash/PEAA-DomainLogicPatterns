using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.DbUtil;
using DomainModel.Domain;

namespace DomainModel.Mapper
{
    public class CategoryMapper : AbstractMapper
    {
        protected override string FindSingleStatement()
        {
            return "SELECT * FROM [Categories] " +
                   "WHERE [Categories].CategoryID = @CategoryID";
        }

        protected override string FindManyStatement()
        {
            throw new NotImplementedException();
        }

        protected override string GetUniqueId(IDataRecord record)
        {
            return record.GetInt32("CategoryID").ToString();
        }

        protected override DomainObject doLoad(string uniqueId, IDataRecord record)
        {
            Category category = new Category();

            category.CategoryID = record.GetInt32("CategoryID");
            category.CategoryName = record.GetStringOrNull("CategoryName");

            return category;
        }

        public Category FindCategoryById(int categoryId)
        {
            ParameterSource parameterSource = CreateParameterSourceOfFindCategoryById(categoryId);

            return (Category)AbstractFindSingleWithParameterSource(typeof(Category), parameterSource);
        }

        private ParameterSource CreateParameterSourceOfFindCategoryById(int categoryId)
        {
            Criterion categoryCriterion = new Criterion
            {
                IsKeyParam = true,
                ParamName = "@CategoryID",
                ParamType = DbType.Int32,
                ParamValue = categoryId
            };

            ParameterSource parameterSource = new ParameterSource();
            parameterSource.AddCriterion(categoryCriterion);

            return parameterSource;
        }
    }
}
