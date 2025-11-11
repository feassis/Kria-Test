using DBProcessor.Data_Classes;
using DBProcessor.Database;
using DBProcessor.DataProcessor;
using DBProcessor.DataSender;
using DBProcessor.Secrets;
using MongoDB.Bson;

public class Program
{
    
    
    public static async Task Main(string[] args)
    {
        DatabaseWrapper dbWrapper = new DatabaseWrapper(Secrets.DB_PATH);

        Console.WriteLine("Inicializando conexão com o servidor");

        List<Transaction> data = await dbWrapper.ConnectToDB<Transaction>(Secrets.DB_TABLE, Secrets.DB_COLLECTION);

        Console.WriteLine("🚀 Iniciando processamento...");

        ApplicantDataBuilder applicantDataBuilder = new ApplicantDataBuilder(Secrets.APPLICANT_NAME,
            DateTime.Now.ToString("dd/MM/yyyy"), Secrets.ARCHIVE_NUMBER);

        var applycantData = applicantDataBuilder.ProcessTransactions(data);

        Console.WriteLine(applycantData.ToJson());

        DataSender sender = new DataSender();

        await sender.SendData(applycantData);
    }
}