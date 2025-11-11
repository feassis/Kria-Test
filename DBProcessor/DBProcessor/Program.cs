using DBProcessor.Data_Classes;
using DBProcessor.Database;

public class Program
{
    private const string DB_PATH = "mongodb+srv://nelsonhilariaokria:uonQ8xiBByKLtKq7@kria.zzp95.mongodb.net/?retryWrites=true&w=majority&appName=Kria";

    private const string DB_TABLE = "Candidato";
    private const string DB_COLLECTION = "TabTransacoes";
    
    public static async Task Main(string[] args)
    {
        DatabaseWrapper dbWrapper = new DatabaseWrapper(DB_PATH);

        await dbWrapper.ConnectRaw();

        Console.WriteLine("Inicializando conexão com o servidor");

        var data = await dbWrapper.ConnectToDB<ApplicantData>(DB_TABLE, DB_COLLECTION);

        Console.WriteLine("🚀 Iniciando processamento...");


    }
}