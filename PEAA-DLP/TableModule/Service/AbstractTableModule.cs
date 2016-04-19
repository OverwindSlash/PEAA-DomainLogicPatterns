using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableModule.Service
{
    public abstract class AbstractTableModule
    {
        private DataSetHolder holder;
        public abstract string TableName { get; }
        public abstract string IdColumnName { get; }

        public AbstractTableModule(DataSetHolder holder)
        {
            this.holder = holder;
        }

        public DataSetHolder Holder
        {
            get { return holder; }
        }

        public DataTable Table
        {
            get { return holder.GetTable(TableName); }
        }

        public DataRow[] this[int key]
        {
            get
            {
                string filter = string.Format("{0} = {1}", IdColumnName, key);
                DataRow[] rows = Table.Select(filter);

                return rows.Length == 0 ? null : rows;
            }
        }
    }
}
