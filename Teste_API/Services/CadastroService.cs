using System.Data.SqlClient;
using System.Text;
using Dapper;
using Teste_API.Models;

namespace Teste_API.Services
{
    public class CadastroService : ICadastro
    {
        private readonly IConfiguration _configuration;
        public CadastroService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<List<ResponseCadastro>> ListarCadastro()
        {
            string query = "SELECT * FROM Cadastro";

            using (var conn = new SqlConnection(_configuration.GetConnectionString("banco")))
            {
                await conn.OpenAsync();
             
                var listCadastro = (await conn.QueryAsync<ResponseCadastro>(query)).ToList();

                return listCadastro;
            }
        }
        public async Task<Cadastro> BuscaCadastro(int Id)
        {
          
            string query = $"SELECT * FROM Cadastro WHERE Id = {Id}";


            using (var conn = new SqlConnection(_configuration.GetConnectionString("banco")))
            {
               
                await conn.OpenAsync();

                var listCadastro = (await conn.QueryAsync<Cadastro>(query)).ToList().FirstOrDefault();

                return listCadastro;
            }
        }
        public async Task<bool> CriarCadastro(Cadastro cadastro)
        {
            string query = "INSERT INTO Cadastro (Nome, SobreNome, Telefone) VALUES (@Nome, @SobreNome, @Telefone)";

            using (var conn = new SqlConnection(_configuration.GetConnectionString("banco")))
            {
                await conn.OpenAsync();

                
                var result = await conn.ExecuteAsync(query, new
                {     
                    Nome = cadastro.Nome,
                    SobreNome = cadastro.SobreNome,
                    Telefone = cadastro.Telefone
                });

                return result > 0; 
            }
        }
        public async Task<bool> AtualizarCadastro(Cadastro cadastro, int id)
        {
            string query = "UPDATE Cadastro SET Nome = @Nome, SobreNome = @SobreNome, Telefone = @Telefone WHERE Id = @Id";
            StringBuilder s = new StringBuilder();
            s.Append("UPDATE Cadastro SET ");
            if (!String.IsNullOrEmpty(cadastro.Nome))
            {
                s.Append("Nome = @Nome ");
            }
            if (!String.IsNullOrEmpty(cadastro.SobreNome) && !string.IsNullOrEmpty(cadastro.Nome))
            {
                s.Append(",SobreNome = @SobreNome, ");
            }
            else if (!string.IsNullOrEmpty(cadastro.SobreNome))
            {
                s.Append("SobreNome = @SobreNome ");
            }
            if (!String.IsNullOrEmpty(cadastro.Telefone) && !string.IsNullOrEmpty(cadastro.SobreNome))
            {
                s.Append(",Telefone = @Telefone ");
            }
            else if (!string.IsNullOrEmpty(cadastro.Telefone))
            {
                s.Append("Telefone = @Telefone ");
            }
            s.Append("WHERE Id = @Id ");


            using (var conn = new SqlConnection(_configuration.GetConnectionString("banco")))
            {
                await conn.OpenAsync();

                var result = await conn.ExecuteAsync(s.ToString(), new
                {
                    Nome = cadastro.Nome,
                    SobreNome = cadastro.SobreNome,
                    Telefone = cadastro.Telefone,
                    Id = id
                });

                return result > 0; 
            }
        }
        public async Task<bool> ExcluirCadastro(int id)
        {
            string query = "DELETE FROM Cadastro WHERE Id = @Id";

            using (var conn = new SqlConnection(_configuration.GetConnectionString("banco")))
            {
                await conn.OpenAsync();

                var result = await conn.ExecuteAsync(query, new { Id = id });

                return result > 0; 
            }
        }



    }
}
