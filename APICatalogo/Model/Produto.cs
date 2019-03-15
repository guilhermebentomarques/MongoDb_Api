using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICatalogo.Model
{
    public class Produto
    {
        public string _id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Tipo { get; set; }
        public string PartNumber { get; set; }
        public string Tamanho { get; set; }
        public string URLImagem { get; set; }
        public double PrecoCusto { get; set; }
        public double PrecoGO { get; set; }
        public ProdutoFabricante Fabricante { get; set; }
        public List<ProdutoAcessorio> Acessorio { get; set; }
    }
}
