using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DBProcessor.Data_Classes;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace DBProcessor.DataSender
{
    internal class DataSender
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task SendData(ApplicantData dados)
        {
            try
            {
                string url = Secrets.Secrets.URL;

                // Serializa o objeto para JSON
                string json = dados.ToJson();

                // Cria o conteúdo da requisição
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Envia via POST
                HttpResponseMessage response = await client.PostAsync(url, content);

                // Lê a resposta
                string resposta = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("✅ Requisição bem-sucedida!");
                    Console.WriteLine($"Resposta: {resposta}");
                }
                else
                {
                    Console.WriteLine($"❌ Erro: {response.StatusCode}");
                    Console.WriteLine($"Mensagem: {resposta}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Erro ao enviar dados: {ex.Message}");
            }
        }
    }
}
