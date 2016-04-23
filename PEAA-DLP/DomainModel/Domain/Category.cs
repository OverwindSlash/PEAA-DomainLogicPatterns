using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Domain
{
    public class Category : DomainObject
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        public override string DomainId
        {
            get { return CategoryID.ToString(); }
        }
    }
}
