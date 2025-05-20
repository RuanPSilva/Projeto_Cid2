using MySql.Data.MySqlClient;
using Projeto_Cid2.Models;
using System.Configuration;
using System.Data;

namespace Projeto_Cid2.Repositorio
{

    public class ProdutoRepositorio(IConfiguration configuration)
    {
        private readonly string _conexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");
        public void Cadastrar(Produto produto)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
              
                conexao.Open();
    
                MySqlCommand cmd = new MySqlCommand("insert into produtos (nomeProd,descricao,preco,quantidade) values (@nomeProd, @descricao, @preco,@quantidade)", conexao); 
                                                                                                                                                                                         
                cmd.Parameters.Add("@nomeprod", MySqlDbType.VarChar).Value = produto.nomeProd;
                
                cmd.Parameters.Add("@descricao", MySqlDbType.VarChar).Value = produto.descricao;
           
                cmd.Parameters.Add("@preco", MySqlDbType.Int32).Value = produto.preco;
               
                cmd.Parameters.Add("@quantidade", MySqlDbType.Int32).Value = produto.quantidade;
                

                
                cmd.ExecuteNonQuery();
               
                conexao.Close();
            }

        }


        public IEnumerable<Produto> TodosProdutos()
        {
           
            List<Produto> Produtolist = new List<Produto>();

           
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                
                conexao.Open();
               
                MySqlCommand cmd = new MySqlCommand("SELECT * from produtos", conexao);

              
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                
                DataTable dt = new DataTable();
                
                da.Fill(dt);
               
                conexao.Close();

               
                foreach (DataRow dr in dt.Rows)
                {
                    
                    Produtolist.Add(
                                new Produto
                                {
                                    idProd = Convert.ToInt32(dr["idProd"]), 
                                    nomeProd = ((string)dr["nomeProd"]),
                                    quantidade = Convert.ToInt32(dr["quantidade"]),
                                    preco = Convert.ToInt32(dr["PrecoProd"]),
                                    descricao = ((string)dr["descricao"]), 
                                }
                    );
                }
                
                return Produtolist;
            }
        }

      
        public Produto ObterProduto(int Codigo)
        {
           
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
               
                conexao.Open();
               
                MySqlCommand cmd = new MySqlCommand("SELECT * from produtos where idProd=@idProd ", conexao);

               
                cmd.Parameters.AddWithValue("@idProd", Codigo);

               
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                
                MySqlDataReader dr;
                
                Produto produto = new Produto();

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
           
                while (dr.Read())
                {
                    
                    produto.idProd = Convert.ToInt32(dr["idProd"]);
                    produto.nomeProd = ((string)dr["nomeProd"]);
                    produto.quantidade = Convert.ToInt32(dr["quantidade"]);
                    produto.preco = Convert.ToDecimal(dr["preco"]);
                    produto.descricao = ((string)dr["descricao"]);
                }
                
                return produto;
            }
        }
        
        public bool Atualizar(Produto produto)
        {
            try
            {
                
                using (var conexao = new MySqlConnection(_conexaoMySQL))
                {
                    
                    conexao.Open();
                   
                    MySqlCommand cmd = new MySqlCommand("Update produto set nomeProd=@nomeprod, preco=@preco, descricao=@descricao, quantidade=@quantidade" + " where idProd=@idProd ", conexao);
                   
                    cmd.Parameters.Add("@idProd", MySqlDbType.Int32).Value = produto.idProd;
             
                    cmd.Parameters.Add("@nomeProd", MySqlDbType.VarChar).Value = produto.nomeProd;
                    
                    cmd.Parameters.Add("@preco", MySqlDbType.Int32).Value = produto.preco;
                    
                    cmd.Parameters.Add("@descricao", MySqlDbType.VarChar).Value = produto.descricao;
                  
                    cmd.Parameters.Add("@quantidade", MySqlDbType.Int32).Value = produto.quantidade;
                    
                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    return linhasAfetadas > 0; 
                }
            }
            catch (MySqlException ex)
            {
                
                Console.WriteLine($"Erro na atualização do Produto: {ex.Message}");
                return false; 

            }
        }
        public void ExcluirProduto(int Id)
        {
            
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {               
                conexao.Open();

              
                MySqlCommand cmd = new MySqlCommand("delete from produto where idProd=@idProd", conexao);

                
                cmd.Parameters.AddWithValue("@idProd", Id);

               
                int i = cmd.ExecuteNonQuery();

                conexao.Close(); 
            }
        }
    }
}

