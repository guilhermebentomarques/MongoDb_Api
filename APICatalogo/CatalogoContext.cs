using APICatalogo.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace APICatalogo
{
    public class CatalogoContext
    {
        private readonly IMongoDatabase _database = null;

        public CatalogoContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Produto> Produtos
        {
            get
            {
                return _database.GetCollection<Produto>("Produto");
            }
        }

        public IMongoCollection<Sequencia> Sequencias
        {
            get
            {
                return _database.GetCollection<Sequencia>("Sequencia");
            }
        }
    }
}
