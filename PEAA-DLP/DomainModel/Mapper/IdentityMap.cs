using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.Domain;

namespace DomainModel.Mapper
{
    public class IdentityMap
    {
        private Dictionary<Type, Dictionary<string, DomainObject>> identityMap
            = new Dictionary<Type, Dictionary<string, DomainObject>>();

        public DomainObject GetDomainObject(Type type, string key)
        {
            Dictionary<string, DomainObject> typeDictionary = GetDictionaryForType(type);

            DomainObject domainObject = null;
            typeDictionary.TryGetValue(key, out domainObject);

            return domainObject;
        }

        public void PutDomainObject(string key, DomainObject domainObject)
        {
            Type type = domainObject.GetType();

            Dictionary<string, DomainObject> domainDictionary = GetDictionaryForType(type);

            domainDictionary.Add(key, domainObject);
        }

        private Dictionary<string, DomainObject> GetDictionaryForType(Type type)
        {
            Dictionary<string, DomainObject> domainDictionary = null;
            identityMap.TryGetValue(type, out domainDictionary);

            if (domainDictionary == null)
            {
                domainDictionary = new Dictionary<string, DomainObject>();
                identityMap.Add(type, domainDictionary);
            }

            return domainDictionary;
        }
    }
}
