using System.Reflection.Metadata.Ecma335;

namespace Desafio_Dio.Models
{
    public class Funcionario
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Endereco { get; private set; }
        public string Ramal { get; private set; }
        public string EmailProfissional { get; private set; }
        public string Departamento { get; private set; }
        public decimal Salario { get; private set; }
        public DateTime DataAdmissao { get; private set; }

        public Funcionario(int id, string nome, string endereco, string ramal, string emailProfissional, string departamento, decimal salario, DateTime dataAdmissao)
        {
            if (id < 0) 
            {
                throw new ArgumentException("Id invalido");
            }

            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentException("Nome invalido");
            }

            if (string.IsNullOrWhiteSpace(endereco))
            {
                throw new ArgumentException("Endereço invalido");
            }

            if (string.IsNullOrWhiteSpace(ramal))
            {
                throw new ArgumentException("Ramal invalido");
            }

            if (string.IsNullOrWhiteSpace(emailProfissional))
            {
                throw new ArgumentException("Email invalido");
            }

            if (string.IsNullOrWhiteSpace(departamento))
            {
                throw new ArgumentException("Departamento invalido");
            }

            if(salario <= 0)
            {
                throw new ArgumentException("Salario invalido");
            }

            if (dataAdmissao == default)
            {
                throw new ArgumentException("Data invalido");
            }

       
            Id = id;
            Nome = nome;
            Endereco = endereco;
            Ramal = ramal;
            EmailProfissional = emailProfissional;
            Departamento = departamento;
            Salario = salario;
            DataAdmissao = dataAdmissao;
        }
        
        public void Alterar(string nome, string endereco, string ramal, string emailProfissional, string departamento, decimal salario)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentException("Nome invalido");
            }

            if (string.IsNullOrWhiteSpace(endereco))
            {
                throw new ArgumentException("Endereço invalido");
            }

            if (string.IsNullOrWhiteSpace(ramal))
            {
                throw new ArgumentException("Ramal invalido");
            }

            if (string.IsNullOrWhiteSpace(emailProfissional))
            {
                throw new ArgumentException("Email invalido");
            }

            if (string.IsNullOrWhiteSpace(departamento))
            {
                throw new ArgumentException("Departamento invalido");
            }

            if (salario <= 0)
            {
                throw new ArgumentException("Salario invalido");
            }

            Nome = nome;
            Endereco = endereco;
            Ramal = ramal;
            EmailProfissional = emailProfissional;
            Departamento = departamento;
            Salario = salario;
        }
       
    }
}
