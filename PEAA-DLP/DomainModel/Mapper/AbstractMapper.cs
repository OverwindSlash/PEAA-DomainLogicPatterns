using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.DbUtil;
using DomainModel.Domain;

namespace DomainModel.Mapper
{
    public abstract class AbstractMapper
    {
        protected static readonly DbProviderFactory providerFactory = DbSettings.ProviderFactory;

        protected IdentityMap identityMap = new IdentityMap();

        protected abstract string FindSingleStatement();
        protected DomainObject AbstractFindSingleWithParameterSource(Type type, ParameterSource parameterSource)
        {
            // Check identity map.
            DomainObject result = identityMap.GetDomainObject(type, parameterSource.UniqueId);
            if (result != null)
            {
                return result;
            }

            // Identity map not hit, do query.
            using (DbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = DbSettings.ConnectionString;
                connection.Open();

                DbCommand command = providerFactory.CreateCommand();
                command.Connection = connection;
                command.CommandText = FindSingleStatement();      // Find single, implement by derived class.

                foreach (Criterion criterion in parameterSource.GetAllCriteria())
                {
                    DbParameter parameter = providerFactory.CreateParameter();
                    parameter.ParameterName = criterion.ParamName;
                    parameter.DbType = criterion.ParamType;
                    parameter.Value = criterion.ParamValue;
                    command.Parameters.Add(parameter);
                }

                DbDataReader reader = command.ExecuteReader();
                reader.Read();

                int AffectedRows = reader.RecordsAffected;
                Console.WriteLine("Find() RowsAffected: {0}", AffectedRows);

                DomainObject domainObject = Load(type, parameterSource.UniqueId, (IDataRecord)reader);

                return domainObject;
            }
        }

        protected abstract string FindManyStatement();
        protected IList<DomainObject> AbstractFindManyWithParameterSource(Type type, ParameterSource parameterSource)
        {
            using (DbConnection connection = providerFactory.CreateConnection())
            {
                connection.ConnectionString = DbSettings.ConnectionString;
                connection.Open();

                DbCommand command = providerFactory.CreateCommand();
                command.Connection = connection;
                command.CommandText = FindManyStatement();      // Find many, implement by derived class.

                foreach (Criterion criterion in parameterSource.GetAllCriteria())
                {
                    DbParameter parameter = providerFactory.CreateParameter();
                    parameter.ParameterName = criterion.ParamName;
                    parameter.DbType = criterion.ParamType;
                    parameter.Value = criterion.ParamValue;
                    command.Parameters.Add(parameter);
                }

                DbDataReader reader = command.ExecuteReader();
                int AffectedRows = reader.RecordsAffected;
                Console.WriteLine("Find() RowsAffected: {0}", AffectedRows);

                IList<DomainObject> results = new List<DomainObject>();

                while (reader.Read())
                {
                    string UniqueId = GetUniqueId((IDataRecord)reader);
                    DomainObject item = Load(type, UniqueId, (IDataRecord)reader);
                    results.Add(item);
                }

                return results;
            }
        }

        abstract protected string GetUniqueId(IDataRecord record);

        abstract protected DomainObject doLoad(string uniqueId, IDataRecord record);
        protected DomainObject Load(Type type, string uniqueId, IDataRecord record)
        {
            // Check identity map.
            DomainObject result = identityMap.GetDomainObject(type, uniqueId);
            if (result != null)
            {
                return result;
            }

            result = doLoad(uniqueId, record);

            identityMap.PutDomainObject(uniqueId, result);

            return result;
        }
    }
}
