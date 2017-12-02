using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Maruko.MongoDB.MongoDBRepos
{
    public class MongoDbContext
    {
        public MongoDbContext(IOptions<MongodbSettings> settings)
        {
            MongoSettings = settings.Value;
            MongoClientSettings mongoClientSettings = MongoClientSettings.FromUrl(new MongoUrl(MongoSettings.ConnectionString));
            //mongoClientSettings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };

            var credential = MongoCredential.CreateCredential(MongoSettings.LoginDatabase, MongoSettings.UserName, MongoSettings.Password);
            mongoClientSettings.Credentials = new[] { credential };
            Client = new MongoClient(mongoClientSettings);
        }

        public MongoClient Client { get; private set; }

        public MongodbSettings MongoSettings { get; private set; }

        public IMongoDatabase GetDateBase()
        {
            return Client.GetDatabase(MongoSettings.Database, new MongoDatabaseSettings() { });
        }
    }
}
