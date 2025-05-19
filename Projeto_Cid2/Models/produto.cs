namespace Projeto_Cid2.Models
{
    public class Produto
    {
        public int idProd { get; set; }
        public string? nomeProd { get; set; }
        public string? descricao { get; set; }
        public decimal? preco { get; set; }
        public int quantidade { get; set; }

        public List<Produto>? listarProduto { get; set; }
    }
}
