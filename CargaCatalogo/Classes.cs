using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace CargaCatalogo
{
    public class Fabricante
    {
        public string ID { get; set; }
        public string Nome { get; set; }
        public string Identificacao { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
    }

    public class Produto
    {
        public string ID { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Tipo { get; set; }
        public string PartNumber { get; set; }
        public string Tamanho { get; set; }
        public string URLImagem { get; set; }
        public double PrecoCusto { get; set; }
        public double PrecoGO { get; set; }
        public Fabricante Fabricante { get; set; }
        public List<ProdutoAcessorio> Acessorio { get; set; }
    }

    public class ProdutoAcessorio
    {
        public string ID { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Tipo { get; set; }
        public string PartNumber { get; set; }
        public string Tamanho { get; set; }
        public string URLImagem { get; set; }
        public double PrecoCusto { get; set; }
        public double PrecoGO { get; set; }
        public Fabricante Fabricante { get; set; }
    }
}
