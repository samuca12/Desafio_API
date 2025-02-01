using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Teste_API.Models;
using Teste_API.Services;

namespace Teste_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CadastroController : ControllerBase
    {
        private readonly ICadastro _cadastro;
        public CadastroController(ICadastro cadastro)
        {
            _cadastro = cadastro;
        }
        [HttpGet]
        public async Task<IActionResult> ListarUsuario()
        {
            var Cadastro = await _cadastro.ListarCadastro();
            return Ok(Cadastro);
        }
        [HttpPost]
        public async Task<IActionResult> CriarCadastro([FromBody] Cadastro cadastro)
        {
            if (cadastro == null)
            {
                return BadRequest("Dados do cadastro inválidos.");
            }

            var sucesso = await _cadastro.CriarCadastro(cadastro);

            if (!sucesso)
            {
                return StatusCode(500, "Ocorreu um erro ao criar o cadastro.");
            }

            return CreatedAtAction(nameof(ListarUsuario), cadastro);  // Retorna 201 com o cadastro criado
        }

        // 3. Atualizar Cadastro (PUT)
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarCadastro(int id, [FromBody] Cadastro cadastro)
        {
            if (cadastro == null )
            {
                return BadRequest("Dados inválidos.");
            }

            var sucesso = await _cadastro.AtualizarCadastro(cadastro, id);

            if (!sucesso)
            {
                return NotFound("Cadastro não encontrado.");
            }

            return NoContent();  // Retorna 204 quando a atualização for bem-sucedida
        }

        // 4. Excluir Cadastro (DELETE)
        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirCadastro(int id)
        {
            var sucesso = await _cadastro.ExcluirCadastro(id);

            if (!sucesso)
            {
                return NotFound("Cadastro não encontrado.");
            }

            return NoContent();  // Retorna 204 quando a exclusão for bem-sucedida
        }
    }
}
