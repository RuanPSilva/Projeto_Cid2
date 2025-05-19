using MySql.Data.MySqlClient;
using Projeto_Cid2.Models;
using System.Data;



namespace Projeto_Cid2.Repositorio
{
    
    public class LoginRepositorio(IConfiguration configuration)
    {
       
        private readonly string _conexaoMySQL = configuration.GetConnectionString("conexaoMySQL");

        public Usuario ObterUsuario(string email)
        {
            
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
               
                conexao.Open();
                
                MySqlCommand cmd = new("SELECT * FROM usuario WHERE email = @email", conexao);
               
                cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = email;

               
                using (MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                  
                    Usuario usuario = null;
                    
                    if (dr.Read())
                    {
                       
                        usuario = new Usuario
                        {
                            
                            idUser = Convert.ToInt32(dr["idUser"]),
                          
                            nomeUser = dr["nomeUser"].ToString(),
                            email = dr["email"].ToString(),
                            senha = dr["senha"].ToString()
                        };
                    }
                    
                    return usuario;
                }
            }
        }
    }
}