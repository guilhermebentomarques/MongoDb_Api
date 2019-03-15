using Microsoft.AspNetCore.Mvc;
using APICatalogo.Interfaces;
using APICatalogo.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APICatalogo.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ProdutoController : Controller
    {
        private readonly IProdutoRepository _ProdutoRepository;
        private readonly ISequenciaRepository _SequenciaRepository;

        public ProdutoController(IProdutoRepository ProdutoRepository, ISequenciaRepository SequenciaRepository)
        {
            _ProdutoRepository = ProdutoRepository;
            _SequenciaRepository = SequenciaRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Produto>> Get()
        {
            return await _ProdutoRepository.GetAllProdutos();
        }

        // GET api/Produtos/5
        [HttpGet("{id}")]
        public async Task<Produto> Get(string id)
        {
            return await _ProdutoRepository.GetProduto(id);
        }

        // GET api/Produtos/text/date/size
        [HttpGet(template: "{bodyText}/{updatedFrom}/{headerSizeLimit}")]
        public async Task<IEnumerable<Produto>> Get(string bodyText,
                                                 DateTime updatedFrom,
                                                 long headerSizeLimit)
        {
            return await _ProdutoRepository.GetProduto(bodyText, updatedFrom, headerSizeLimit)
                        ?? new List<Produto>();
        }

        // POST api/Produtos
        [HttpPost]
        public void Post([FromBody] ProdutoParam newProduto)
        {
            _ProdutoRepository.AddProduto(new Produto
            {
                 Nome = newProduto.Nome,
                 Descricao = newProduto.Descricao,
                 Tipo = newProduto.Tipo,
                 PartNumber = newProduto.PartNumber,
                 Tamanho = newProduto.Tamanho,
                 URLImagem = newProduto.URLImagem,
                 PrecoCusto = newProduto.PrecoCusto,
                 PrecoGO = newProduto.PrecoGO,
                 Fabricante = newProduto.Fabricante,
                 Acessorio = newProduto.Acessorio
            });
        }

        // PUT api/Produtos/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]string value)
        {
            _ProdutoRepository.UpdateProdutoDocument(id, value);
        }

        // DELETE api/Produtos/23243423
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _ProdutoRepository.RemoveProduto(id);
        }
    }
}
