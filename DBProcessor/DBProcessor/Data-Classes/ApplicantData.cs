using DBProcessor.Secrets;
namespace DBProcessor.Data_Classes
{
    public class ApplicantData
    {
        public string Candidato { get; set; }
        public string DataReferencia { get; set; }
        public int NumeroArquivo { get; set; }
        public Registros[] Registros { get; set; } = new Registros[Secrets.Secrets.NUMBER_OF_TRANSACTIONS];

        public ApplicantData(string candidato, string dataReferencia, int numeroArquivo)
        {
            Candidato = candidato;
            DataReferencia = dataReferencia;
            NumeroArquivo = numeroArquivo;
        }

        public void SetRegistros(Registros[] Registros)
        {
            this.Registros = Registros;
        }
    }

}
