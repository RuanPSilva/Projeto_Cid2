using MySql.Data.MySqlClient;
using Projeto_Cid2.Models;
using System.Configuration;
using System.Data;

namespace Projeto_Cid2.Repositorio
{
    // Define a classe responsável por interagir com os dados de clientes no banco de dados
    public class ProdutoRepositorio(IConfiguration configuration)
    {
        // Declara uma variável privada somente leitura para armazenar a string de conexão com o MySQL
        private readonly string _conexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");
        public void Cadastrar(produto produto)
        {
            // Bloco using para garantir que a conexão seja fechada e os recursos liberados após o uso
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                // Abre a conexão com o banco de dados MySQL
                conexao.Open();
                // Cria um novo comando SQL para inserir dados na tabela 'produto'
                MySqlCommand cmd = new MySqlCommand("insert into produto (NomeProd,DescricaoProd,PrecoProd,QuantProd) values (@nomeprod, @descricaoprod, @preco,@quantidade)", conexao); // @: PARAMETRO
                                                                                                                                                                                         // Adiciona um parâmetro para o nome, definindo seu tipo e valor
                cmd.Parameters.Add("@nomeprod", MySqlDbType.VarChar).Value = produto.nomeProd;
                // Adiciona um parâmetro para o nome, definindo seu tipo e valor
                cmd.Parameters.Add("@descricao", MySqlDbType.VarChar).Value = produto.descricao;
                // Adiciona um parâmetro para o descricao, definindo seu tipo e valor
                cmd.Parameters.Add("@preco", MySqlDbType.Int32).Value = produto.preco;
                // Adiciona um parâmetro para o preco, definindo seu tipo e valor
                cmd.Parameters.Add("@quantidade", MySqlDbType.Int32).Value = produto.quantidade;
                // Adiciona um parâmetro para o quantidade, definindo seu tipo e valor

                // Executa o comando SQL de inserção e retorna o número de linhas afetadas
                cmd.ExecuteNonQuery();
                // Fecha explicitamente a conexão com o banco de dados (embora o 'using' já faça isso)
                conexao.Close();
            }

        }
        public IEnumerable<produto> TodosProdutos()
        {
            // Cria uma nova lista para armazenar os objetos Produto
            List<produto> Produtolist = new List<produto>();

            // Bloco using para garantir que a conexão seja fechada e os recursos liberados após o uso
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                // Abre a conexão com o banco de dados MySQL
                conexao.Open();
                // Cria um novo comando SQL para selecionar todos os registros da tabela 'produto'
                MySqlCommand cmd = new MySqlCommand("SELECT * from produto", conexao);

                // Cria um adaptador de dados para preencher um DataTable com os resultados da consulta
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                // Cria um novo DataTable
                DataTable dt = new DataTable();
                // metodo fill- Preenche o DataTable com os dados retornados pela consulta
                da.Fill(dt);
                // Fecha explicitamente a conexão com o banco de dados 
                conexao.Close();

                // interage sobre cada linha (DataRow) do DataTable
                foreach (DataRow dr in dt.Rows)
                {
                    // Cria um novo objeto Cliente e preenche suas propriedades com os valores da linha atual
                    Produtolist.Add(
                                new produto
                                {
                                    idProd = Convert.ToInt32(dr["idProd"]), // Converte o valor da coluna "CodProd" para inteiro
                                    nomeProd = ((string)dr["nomeProd"]), // Converte o valor da coluna "NomeProd" para string
                                    quantidade = Convert.ToInt32(dr["quantidade"]), // Converte o valor da coluna "QuantProd" para inteiro
                                    preco = Convert.ToInt32(dr["PrecoProd"]), // Converte o valor da coluna "PrecoProd" para inteiro
                                    descricao = ((string)dr["descricao"]), // Converte o valor da coluna "DescricaoProd" para string
                                });
                }
                // Retorna a lista de todos os produtos
                return Produtolist;
            }
        }

        // Método para buscar um produto específico pelo seu código (Codigo)
        public produto ObterProduto(int Codigo)
        {
            // Bloco using para garantir que a conexão seja fechada e os recursos liberados após o uso
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                // Abre a conexão com o banco de dados MySQL
                conexao.Open();
                // Cria um novo comando SQL para selecionar um registro da tabela 'produto' com base no código
                MySqlCommand cmd = new MySqlCommand("SELECT * from produto where CodProd=@codigo ", conexao);

                // Adiciona um parâmetro para o código a ser buscado, definindo seu tipo e valor
                cmd.Parameters.AddWithValue("@codigo", Codigo);

                // Cria um adaptador de dados (não utilizado diretamente para ExecuteReader)
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                // Declara um leitor de dados do MySQL
                MySqlDataReader dr;
                // Cria um novo objeto Produto para armazenar os resultados
                produto produto = new produto();

                /* Executa o comando SQL e retorna um objeto MySqlDataReader para ler os resultados
                CommandBehavior.CloseConnection garante que a conexão seja fechada quando o DataReader for fechado*/

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                // Lê os resultados linha por linha
                while (dr.Read())
                {
                    // Preenche as propriedades do objeto Produto com os valores da linha atual
                    produto.idProd = Convert.ToInt32(dr["CodProd"]);
                    produto.nomeProd = ((string)dr["NomeProd"]);
                    produto.quantidade = Convert.ToInt32(dr["QuantProd"]);
                    produto.preco = Convert.ToInt32(dr["PrecoProd"]);
                    produto.descricao = ((string)dr["NomeProd"]);
                }
                // Retorna o objeto Cliente encontrado (ou um objeto com valores padrão se não encontrado)
                return produto;
            }
        }
        // Método para Editar (atualizar) os dados de um produto existente no banco de dados
        public bool Atualizar(produto produto)
        {
            try
            {
                // Bloco using para garantir que a conexão seja fechada e os recursos liberados após o uso
                using (var conexao = new MySqlConnection(_conexaoMySQL))
                {
                    // Abre a conexão com o banco de dados MySQL
                    conexao.Open();
                    // Cria um novo comando SQL para atualizar dados na tabela 'produto' com base no código
                    MySqlCommand cmd = new MySqlCommand("Update produto set NomeProd=@nomeprod, PrecoProd=@preco, DescricaoProd=@descricaoprod, QuantProd=@quantidadeprod" + " where CodProd=@codigo ", conexao);
                    // Adiciona um parâmetro para o código do produto a ser atualizado, definindo seu tipo e valor
                    cmd.Parameters.Add("@codigo", MySqlDbType.Int32).Value = produto.idProd;
                    // Adiciona um parâmetro para o novo nome, definindo seu tipo e valor
                    cmd.Parameters.Add("@nomeprod", MySqlDbType.VarChar).Value = produto.nomeProd;
                    // Adiciona um parâmetro para o novo preco, definindo seu tipo e valor
                    cmd.Parameters.Add("@preco", MySqlDbType.Int32).Value = produto.preco;
                    // Adiciona um parâmetro para o novo descricao, definindo seu tipo e valor
                    cmd.Parameters.Add("@descricaoprod", MySqlDbType.VarChar).Value = produto.descricao;
                    // Adiciona um parâmetro para o novo descricao, definindo seu tipo e valor
                    cmd.Parameters.Add("@quantidadeprod", MySqlDbType.Int32).Value = produto.quantidade;
                    // Executa o comando SQL de atualização e retorna o número de linhas afetadas
                    //executa e verifica se a alteração foi realizada
                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    return linhasAfetadas > 0; // Retorna true se ao menos uma linha foi atualizada

                }
            }
            catch (MySqlException ex)
            {
                // Logar a exceção (usar um framework de logging como NLog ou Serilog)
                Console.WriteLine($"Erro ao atualizar produto: {ex.Message}");
                return false; // Retorna false em caso de erro

            }
        }
        public void ExcluirProduto(int Id)
        {
            // Bloco using para garantir que a conexão seja fechada e os recursos liberados após o uso
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                // Abre a conexão com o banco de dados MySQL
                conexao.Open();

                // Cria um novo comando SQL para deletar um registro da tabela 'Produto' com base no código
                MySqlCommand cmd = new MySqlCommand("delete from produto where CodProd=@codigo", conexao);

                // Adiciona um parâmetro para o código a ser excluído, definindo seu tipo e valor
                cmd.Parameters.AddWithValue("@codigo", Id);

                // Executa o comando SQL de exclusão e retorna o número de linhas afetadas
                int i = cmd.ExecuteNonQuery();

                conexao.Close(); // Fecha  a conexão com o banco de dados
            }
        }
    }
}

