using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableModule.Service
{
    public class DataSetHolder
    {
        private DataSet dataSet = new DataSet();

        public DataTable this[string tableName]
        {
            get { return dataSet.Tables[tableName]; }
        }

        public void AddTable(DataTable dataTable)
        {
            if (dataSet.Tables.Contains(dataTable.TableName))
            {
                dataSet.Tables.Remove(dataTable.TableName);
            }
            dataSet.Tables.Add(dataTable);
        }

        public DataTable GetTable(string tableName)
        {
            return dataSet.Tables[tableName];
        }
    }
}
