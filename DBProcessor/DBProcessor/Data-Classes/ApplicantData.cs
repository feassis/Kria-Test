namespace DBProcessor.Data_Classes
{
    public class ApplicantData
    {
        public string Candidato { get; set; }
        public string DataReferencia { get; set; }
        public int NumeroArquivo { get; set; }
        public Registros[] Registros { get; set; }
    }

}
