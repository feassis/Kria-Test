

using DBProcessor.Data_Classes;

namespace DBProcessor.DataProcessor
{
    public class ApplicantDataBuilder
    {
        private ApplicantData applicantData;

        public ApplicantDataBuilder(string candidato, string dataReferencia, int numeroArquivo)
        {
            applicantData = new ApplicantData(candidato, dataReferencia, numeroArquivo);
        }

        public ApplicantData ProcessTransactions(List<Transaction> data)
        {
            List<Registros> registros = new List<Registros>();
            foreach (var entry in data)
            {
                registros.Add(new Registros(entry));
            }

            applicantData.SetRegistros(registros.ToArray());

            return applicantData;
        }
    }
}
