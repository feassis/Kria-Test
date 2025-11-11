
namespace DBProcessor.Data_Classes
{
    public class Registros
    {
        public string GUID { get; set; }
        public int CodigoPracaPedagio { get; set; }
        public int CodigoCabine { get; set; }
        public string Instante { get; set; }
        public int Sentido { get; set; }
        public int TipoVeiculo { get; set; }
        public int Isento { get; set; }
        public int Evasao { get; set; }
        public int TipoCobrancaEfetuada { get; set; }
        public float ValorDevido { get; set; }
        public float ValorArrecadado { get; set; }
        public float MultiplicadorTarifa { get; set; }
    }

}
