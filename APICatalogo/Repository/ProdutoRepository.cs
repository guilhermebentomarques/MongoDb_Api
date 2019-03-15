using APICatalogo.Interfaces;
using APICatalogo.Model;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICatalogo.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly CatalogoContext _context = null;
        private ISequenciaRepository _ISequenciaRepository;

        public ProdutoRepository(IOptions<Settings> settings, ISequenciaRepository ISequenciaRepository)
        {
            _context = new CatalogoContext(settings);
            _ISequenciaRepository = ISequenciaRepository;
        }

        public async Task<IEnumerable<Produto>> GetAllProdutos()
        {
            try
            {
                return await _context.Produtos.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        // query after Id or InternalId (BSonId value)
        //
        public async Task<Produto> GetProduto(string id)
        {
            try
            {
                //ObjectId internalId = GetInternalId(id);
                return await _context.Produtos
                                .Find(Produto => Produto._id == id)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        // query after body text, updated time, and header image size
        //
        public async Task<IEnumerable<Produto>> GetProduto(string nome, DateTime updatedFrom, long headerSizeLimit)
        {
            try
            {
                var query = _context.Produtos.Find(Produto => Produto.Nome.Contains(nome) //&&
                                       //Produto.UpdatedOn >= updatedFrom &&
                                       //Produto.HeaderImage.ImageSize <= headerSizeLimit
                                       );

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        // Try to convert the Id to a BSonId value
        private ObjectId GetInternalId(string id)
        {
            if (!ObjectId.TryParse(id, out ObjectId internalId))
                internalId = ObjectId.Empty;

            return internalId;
        }

        public async Task AddProduto(Produto item)
        {
            try
            {
                //ID DOS PAIS
                long NewID = _ISequenciaRepository.GetNextSequenceValue("Produto");

                //ID PRODUTO
                item._id = NewID.ToString();

                //ID DOS FILHOS
                foreach (ProdutoAcessorio prod in item.Acessorio)
                {
                    prod._id = NewID.ToString();
                }

                //ID DO FABRICANTE
                item.Fabricante._id = NewID.ToString();

                await _context.Produtos.InsertOneAsync(item);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> RemoveProduto(string id)
        {
            try
            {
                DeleteResult actionResult = await _context.Produtos.DeleteOneAsync(
                     Builders<Produto>.Filter.Eq("Id", id));

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> UpdateProduto(string id, string nome)
        {
            var filter = Builders<Produto>.Filter.Eq(s => s._id, id);
            var update = Builders<Produto>.Update
                            .Set(s => s.Nome, nome);
            //.CurrentDate(s => s.UpdatedOn


            try
            {
                UpdateResult actionResult = await _context.Produtos.UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> UpdateProduto(string id, Produto item)
        {
            try
            {
                ReplaceOneResult actionResult = await _context.Produtos
                                                .ReplaceOneAsync(n => n._id.Equals(id)
                                                                , item
                                                                , new UpdateOptions { IsUpsert = true });
                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        // Demo function - full document update
        public async Task<bool> UpdateProdutoDocument(string id, string body)
        {
            var item = await GetProduto(id) ?? new Produto();
            //item.Body = body;
            //item.UpdatedOn = DateTime.Now;

            return await UpdateProduto(id, item);
        }

        public async Task<bool> RemoveAllProdutos()
        {
            try
            {
                DeleteResult actionResult = await _context.Produtos.DeleteManyAsync(new BsonDocument());

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        // it creates a sample compound index (first using UserId, and then Body)
        // 
        // MongoDb automatically detects if the index already exists - in this case it just returns the index details
        public async Task<string> CreateIndex()
        {
            try
            {
                IndexKeysDefinition<Produto> keys = Builders<Produto>
                                                    .IndexKeys
                                                    .Ascending(item => item.Nome);

                return await _context.Produtos
                                .Indexes.CreateOneAsync(new CreateIndexModel<Produto>(keys));
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
    }
}
