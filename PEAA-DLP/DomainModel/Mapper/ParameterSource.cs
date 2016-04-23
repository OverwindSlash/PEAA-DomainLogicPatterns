using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Mapper
{
    public class Criterion
    {
        public bool IsKeyParam { get; set; }
        public string ParamName { get; set; }
        public DbType ParamType { get; set; }
        public object ParamValue { get; set; }
    }

    public class ParameterSource
    {
        private IList<Criterion> allCriteria = new List<Criterion>();

        public void AddCriterion(Criterion criterion)
        {
            this.allCriteria.Add(criterion);
        }

        public IList<Criterion> GetAllCriteria()
        {
            return this.allCriteria;
        } 

        public string UniqueId
        {
            get
            {
                StringBuilder uniqueIdStringBuilder = new StringBuilder();
                foreach (Criterion criterion in allCriteria)
                {
                    if (criterion.IsKeyParam)
                    {
                        uniqueIdStringBuilder.Append(criterion.ParamValue.ToString());
                        uniqueIdStringBuilder.Append(":");
                    }
                }
                return uniqueIdStringBuilder.ToString().TrimEnd(':');
            }
        }
    }
}
