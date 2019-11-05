using BP.Infrastructure.Domain;
using BP.Repository.DataBase;
using MongoDB.Driver;

namespace BP.Repository
{
    public abstract class Repository<DomainType, IdType, DatabaseType>
        where DomainType : IAggregateRoot
        where DatabaseType : Entity<IdType>
    {
        private static IMongoClient _client = null;
        private static IMongoDatabase _database = null;
        protected static IMongoCollection<DatabaseType> _collection = null;

        public Repository()
        {
            _client = new MongoClient("mongodb://10.0.75.1:27017");
            _database = _client.GetDatabase("CloudDataHubDb");
            _collection = _database.GetCollection<DatabaseType>(typeof(DomainType).Name);
        }

        public virtual DomainType Update(DomainType aggregate)
        {

            var databaseObject = ConvertToDatabaseType(aggregate);
            var filter = _collection.Find(x => x.Id.Equals(databaseObject.Id)).Filter;
            _collection.ReplaceOne(filter, databaseObject);

            return ConvertToDomain(databaseObject);
        }

        public virtual DomainType Insert(DomainType aggregate)
        {
            var databaseObject = ConvertToDatabaseType(aggregate);
            _collection.InsertOne(databaseObject);

            return ConvertToDomain(databaseObject);
        }

        public virtual void Delete(DomainType aggregate)
        {
            var obj = ConvertToDatabaseType(aggregate);
            var filter = _collection.Find(x => x.Id.Equals(obj.Id)).Filter;
            _collection.DeleteOne(filter);
        }

        public abstract DomainType FindBy(IdType id);

        public abstract DatabaseType ConvertToDatabaseType(DomainType domainType);

        public abstract DomainType ConvertToDomain(DatabaseType databaseType);
    }
}
