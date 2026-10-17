using Azure.Data.Tables;
using Desafio_Dio.Data;
using Desafio_Dio.Models;
using System.Diagnostics;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Desafio_Dio.Services
{
    public class FuncionarioService
    {
        private readonly AppDbContext _context;
        private readonly TableClient _tableClient;

        public FuncionarioService(AppDbContext context, TableClient tableClient) 
        {
            _context = context;
            _tableClient = tableClient;
        }

        
        public async Task CriarFuncionario(string nome, string endereco, string ramal, string emailProfissional, string departamento, decimal salario, DateTime dataAdmissao)
        {
            
            Funcionario funcionario = new Funcionario(0, nome, endereco, ramal, emailProfissional, departamento, salario, dataAdmissao);
            

            _context.Add(funcionario);

            await _context.SaveChangesAsync();

            await GravarLogAsync(funcionario, TipoAcao.Criar);
        }

        public async Task<Funcionario?> ObterPorIdAsync(int id)
        {
            return await _context.Funcionarios.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<bool> RemoverPorIdAsync(int id)
        {
            var funcionario = await ObterPorIdAsync(id);

            if(funcionario == null)
            {
                return false;
            }
               
            _context.Funcionarios.Remove(funcionario);
            await _context.SaveChangesAsync();

            await GravarLogAsync(funcionario, TipoAcao.Deletar);

            return true;
        }

        public async Task<bool> AlterarPorIdAsync(int id, string nome, string endereco, string ramal, string emailProfissional, string departamento, decimal salario)
        {
            var funcionario = await ObterPorIdAsync(id);

            if (funcionario == null)
            {
                return false;
            }

            funcionario.Alterar(nome, endereco, ramal, emailProfissional, departamento, salario);

            await _context.SaveChangesAsync();

            await GravarLogAsync(funcionario, TipoAcao.Atualizar);

            return true;
        }

        private async Task GravarLogAsync(Funcionario funcionario, TipoAcao tipoAcao)
        {
            Log log = new Log(funcionario.Id, funcionario.Nome, funcionario.Endereco, funcionario.Ramal, funcionario.EmailProfissional, funcionario.Departamento, funcionario.Salario, funcionario.DataAdmissao, tipoAcao, funcionario.Id.ToString(), Guid.NewGuid().ToString());

            var registro = new TableEntity(log.PartitionKey, log.RowKey);

            registro["Id"] = log.Id;
            registro["Nome"] = log.Nome;
            registro["Endereco"] = log.Endereco;
            registro["Ramal"] = log.Ramal;
            registro["EmailProfissional"] = log.EmailProfissional;
            registro["Departamento"] = log.Departamento;
            registro["Salario"] = log.Salario.ToString(CultureInfo.InvariantCulture);
            registro["DataAdmissao"] = DateTime.SpecifyKind(log.DataAdmissao.Date, DateTimeKind.Utc);
            registro["TipoDeAcao"] = log.TipoDeAcao.ToString();

            await _tableClient.AddEntityAsync(registro);
        }
    }
}
