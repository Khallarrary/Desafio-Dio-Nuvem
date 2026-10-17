using Azure;
using Azure.Data.Tables;


namespace Desafio_Dio.Models
{
    public class Log : Funcionario, ITableEntity
    {
        public TipoAcao TipoDeAcao { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        public Log(int id, string nome, string endereco, string ramal, string emailProfissional, string departamento, decimal salario, DateTime dataAdmissao, TipoAcao tipoDeAcao, string partitionKey, string rowkey) : base(id, nome, endereco, ramal, emailProfissional, departamento, salario, dataAdmissao)
        {
            TipoDeAcao = tipoDeAcao;
            PartitionKey = partitionKey;
            RowKey = rowkey;
        }

    }
}
