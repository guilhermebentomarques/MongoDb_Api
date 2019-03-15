using APICatalogo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICatalogo.Interfaces
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> GetAllProdutos();

        Task<Produto> GetProduto(string id);

        // query after multiple parameters
        Task<IEnumerable<Produto>> GetProduto(string bodyText, DateTime updatedFrom, long headerSizeLimit);

        // add new Produto document
        Task AddProduto(Produto item);

        // remove a single document / Produto
        Task<bool> RemoveProduto(string id);

        // update just a single document / Produto
        Task<bool> UpdateProduto(string id, string body);

        // demo interface - full document update
        Task<bool> UpdateProdutoDocument(string id, string body);

        // should be used with high cautious, only in relation with demo setup
        Task<bool> RemoveAllProdutos();

        // creates a sample index
        Task<string> CreateIndex();
    }
}
