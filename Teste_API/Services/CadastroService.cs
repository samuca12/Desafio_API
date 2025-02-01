using System.Data.SqlClient;
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
        public async Task<List<Cadastro>> ListarCadastro()
        {
            // Definindo a consulta SQL para buscar todos os registros
            string query = "SELECT * FROM Cadastro";  // Substitua 'Cadastro' pelo nome correto da sua tabela

            // Usando Dapper para executar a consulta e mapear para uma lista de objetos Cadastro
            using (var conn = new SqlConnection(_configuration.GetConnectionString("banco")))
            {
                // Abrindo a conexão
                await conn.OpenAsync();

                // Usando Dapper para fazer a consulta e mapear os dados para uma lista
                var listCadastro = (await conn.QueryAsync<Cadastro>(query)).ToList();

                // Retornando a lista de cadastros
                return listCadastro;
            }
        }
        public async Task<bool> CriarCadastro(Cadastro cadastro)
        {
            string query = "INSERT INTO Cadastro (Nome, SobreNome, Telefone) VALUES (@Nome, @SobreNome, @Telefone)";

            using (var conn = new SqlConnection(_configuration.GetConnectionString("banco")))
            {
                await conn.OpenAsync();

                // A inserção usa o comando SQL com parâmetros para evitar SQL Injection
                var result = await conn.ExecuteAsync(query, new
                {
                    Id = Guid.NewGuid(),  // Gerando um novo GUID para o 'Id'
                    Nome = cadastro.Nome,
                    SobreNome = cadastro.SobreNome,
                    Telefone = cadastro.Telefone
                });

                return result > 0;  // Se o número de linhas afetadas for maior que 0, a inserção foi bem-sucedida
            }
        }
        public async Task<bool> AtualizarCadastro(Cadastro cadastro, int id)
        {
            string query = "UPDATE Cadastro SET Nome = @Nome, SobreNome = @SobreNome, Telefone = @Telefone WHERE Id = @Id";

            using (var conn = new SqlConnection(_configuration.GetConnectionString("banco")))
            {
                await conn.OpenAsync();

                // Atualizando o registro com base no ID
                var result = await conn.ExecuteAsync(query, new
                {
                    Nome = cadastro.Nome,
                    SobreNome = cadastro.SobreNome,
                    Telefone = cadastro.Telefone,
                    Id = id
                });

                return result > 0;  // Se o número de linhas afetadas for maior que 0, a atualização foi bem-sucedida
            }
        }
        public async Task<bool> ExcluirCadastro(int id)
        {
            string query = "DELETE FROM Cadastro WHERE Id = @Id";

            using (var conn = new SqlConnection(_configuration.GetConnectionString("banco")))
            {
                await conn.OpenAsync();

                // Excluindo o registro com base no ID
                var result = await conn.ExecuteAsync(query, new { Id = id });

                return result > 0;  // Se o número de linhas afetadas for maior que 0, a exclusão foi bem-sucedida
            }
        }



    }
}
