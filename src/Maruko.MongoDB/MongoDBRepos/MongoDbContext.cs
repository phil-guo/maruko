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
            
            mongoClientSettings.Credential = MongoCredential.CreateCredential(MongoSettings.LoginDatabase, MongoSettings.UserName, MongoSettings.Password);
            Client = new MongoClient(mongoClientSettings);
        }

        private MongoClient Client { get; set; }

        private MongodbSettings MongoSettings { get; set; }

        public IMongoDatabase GetDateBase()
        {
            return Client.GetDatabase(MongoSettings.Database, new MongoDatabaseSettings() { });
        }
    }
}
