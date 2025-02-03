using Teste_API.Models;

namespace Teste_API.Services
{
    public interface ICadastro
    {
        Task<bool> AtualizarCadastro(Cadastro cadastro, int id);
        Task<bool> CriarCadastro(Cadastro cadastro);
        Task<bool> ExcluirCadastro(int id);
        Task<List<ResponseCadastro>> ListarCadastro();
        Task<Cadastro> BuscaCadastro (int id);
    }
}