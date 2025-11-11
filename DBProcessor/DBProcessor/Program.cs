using DBProcessor.Data_Classes;
using DBProcessor.Database;
using DBProcessor.Secrets;

public class Program
{
    
    
    public static async Task Main(string[] args)
    {
        DatabaseWrapper dbWrapper = new DatabaseWrapper(Secrets.DB_PATH);

        await dbWrapper.ConnectRaw();

        Console.WriteLine("Inicializando conexão com o servidor");

        var data = await dbWrapper.ConnectToDB<ApplicantData>(Secrets.DB_TABLE, Secrets.DB_COLLECTION);

        Console.WriteLine("🚀 Iniciando processamento...");


    }
}