using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DBProcessor.Data_Classes
{
    public class Transaction
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public int IdTransacao { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime DtCriacao { get; set; }

        public string CodigoPracaPedagio { get; set; }

        public int CodigoCabine { get; set; }

        public string Instante { get; set; }

        public int Sentido { get; set; }

        public int QuantidadeEixosVeiculo { get; set; }

        public int Rodagem { get; set; }

        public int Isento { get; set; }

        public int MotivoIsencao { get; set; }

        public int Evasao { get; set; }

        public int EixoSuspenso { get; set; }

        public int QuantidadeEixosSuspensos { get; set; }

        public int TipoCobranca { get; set; }

        public string Placa { get; set; }

        public int LiberacaoCancela { get; set; }

        public decimal ValorDevido { get; set; }

        public decimal ValorArrecadado { get; set; }

        public string CnpjAmap { get; set; }

        public double? MultiplicadorTarifa { get; set; }

        public int VeiculoCarregado { get; set; }

        public string IdTag { get; set; }
    }
}
