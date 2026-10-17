using Desafio_Dio.Models;
using Desafio_Dio.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Desafio_Dio.Controllers
{
    [ApiController]
    [Route("Funcionario")]
    public class FuncionarioController : ControllerBase
    {
        private readonly FuncionarioService _funcionarioService;

        public FuncionarioController(FuncionarioService funcionarioService)
        {
            _funcionarioService = funcionarioService;
        }


        [HttpPost]
        public async Task<IActionResult> CriarFuncionario([FromBody] FuncionarioDto request)
        {
            try
            {
                await _funcionarioService.CriarFuncionario(
                request.Nome,
                request.Endereco,
                request.Ramal,
                request.EmailProfissional,
                request.Departamento,
                request.Salario,
                request.DataAdmissao);
            }
            catch (ArgumentException ex) 
            {
                return BadRequest(new { mensagem = ex.Message });
            }

            return StatusCode(201);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var funcionario = await _funcionarioService.ObterPorIdAsync(id);

            if (funcionario == null)
            {
                return NotFound();
            }

            return Ok(funcionario);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteById(int id)
        {
           
           var excluir = await _funcionarioService.RemoverPorIdAsync(id);

            if (excluir)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateById(int id, [FromBody] AtualizarDto request)
        {
            try
            {
                var alterar = await _funcionarioService.AlterarPorIdAsync(id, request.Nome, request.Endereco, request.Ramal, request.EmailProfissional, request.Departamento, request.Salario);

                if (alterar)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }

        }
    }
}
