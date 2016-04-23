using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Domain
{
    public abstract class DomainObject
    {
        public abstract string DomainId { get; }
    }
}
