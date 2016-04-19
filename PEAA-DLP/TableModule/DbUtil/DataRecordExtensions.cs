using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableModule.DbUtil
{
    public static class DataRecordExtensions
    {
        public static string GetStringOrNull(this IDataRecord record, int ordinal)
        {
            return record.IsDBNull(ordinal) ? null : record.GetString(ordinal);
        }

        public static string GetStringOrNull(this IDataRecord record, string columnName)
        {
            return record.GetStringOrNull(record.GetOrdinal(columnName));
        }

        public static int? GetInt32OrNull(this IDataRecord record, int ordinal)
        {
            return record.IsDBNull(ordinal) ? null : (int?)record.GetInt32(ordinal);
        }

        public static int? GetInt32OrNull(this IDataRecord record, string columnName)
        {
            return record.GetInt32OrNull(record.GetOrdinal(columnName));
        }


        public static int GetInt32(this IDataRecord record, string columnName)
        {
            return record.GetInt32(record.GetOrdinal(columnName));
        }

        public static short GetInt16(this IDataRecord record, string columnName)
        {
            return record.GetInt16(record.GetOrdinal(columnName));
        }

        public static decimal GetDecimal(this IDataRecord record, string columnName)
        {
            return record.GetDecimal(record.GetOrdinal(columnName));
        }

        public static float GetFloat(this IDataRecord record, string columnName)
        {
            return record.GetFloat(record.GetOrdinal(columnName));
        }
    }
}
