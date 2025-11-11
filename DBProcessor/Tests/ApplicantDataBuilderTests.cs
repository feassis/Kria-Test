using DBProcessor.Data_Classes;
using DBProcessor.DataProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class ApplicantDataBuilderTests
    {
        [Fact]
        public void Constructor_ShouldInitializeApplicantData()
        {
            // Arrange
            string candidato = "Felipe Assis";
            string dataReferencia = "2025-11-10";
            int numeroArquivo = 42;

            // Act
            var builder = new ApplicantDataBuilder(candidato, dataReferencia, numeroArquivo);

            // Assert (usando reflexão para acessar o campo privado)
            var field = typeof(ApplicantDataBuilder)
                .GetField("applicantData", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            var instance = field.GetValue(builder) as ApplicantData;

            Assert.NotNull(instance);
            Assert.Equal(candidato, instance.Candidato);
            Assert.Equal(dataReferencia, instance.DataReferencia);
            Assert.Equal(numeroArquivo, instance.NumeroArquivo);
        }

        [Fact]
        public void ProcessTransactions_ShouldCreateRegistrosAndAssignToApplicantData()
        {
            // Arrange
            var builder = new ApplicantDataBuilder("Felipe", "2025-11-10", 1);

            var transactions = new List<Transaction>
            {
                new Transaction
                {
                    Id = "6734e6d9db151c04900b1b96",
                    CodigoPracaPedagio = "100",
                    CodigoCabine = 2,
                    Instante = "2025-11-10T12:00:00",
                    Sentido = 1,
                    Rodagem = 2,
                    Isento = 2,
                    Evasao = 1,
                    TipoCobranca = 1,
                    ValorDevido = 10.50m,
                    ValorArrecadado = 10.50m
                },
                new Transaction
                {
                    Id = "6734e6d9db151c04900b1b97",
                    CodigoPracaPedagio = "101",
                    CodigoCabine = 3,
                    Instante = "2025-11-10T13:00:00",
                    Sentido = 2,
                    Rodagem = 1,
                    Isento = 1,
                    Evasao = 2,
                    TipoCobranca = 2,
                    ValorDevido = 7.25m,
                    ValorArrecadado = 7.25m
                }
            };

            // Act
            var result = builder.ProcessTransactions(transactions);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Felipe", result.Candidato);
            Assert.Equal(2, result.Registros.Length);

            var first = result.Registros[0];
            Assert.Equal("100", first.CodigoPracaPedagio);
            Assert.Equal("2", first.CodigoCabine);
            Assert.Equal("Crescente", first.Sentido);
            Assert.Equal("Comercial", first.TipoVeiculo);
            Assert.Equal("Não", first.Isento);
            Assert.Equal("Sim", first.Evasao);
            Assert.Equal("Manual", first.TipoCobrancaEfetuada);
            Assert.Equal("10.50", first.ValorDevido);

            var second = result.Registros[1];
            Assert.Equal("101", second.CodigoPracaPedagio);
            Assert.Equal("3", second.CodigoCabine);
            Assert.Equal("Decrescente", second.Sentido);
            Assert.Equal("Passeio", second.TipoVeiculo);
            Assert.Equal("Sim", second.Isento);
            Assert.Equal("Não", second.Evasao);
            Assert.Equal("TAG", second.TipoCobrancaEfetuada);
            Assert.Equal("7.25", second.ValorDevido);
        }

        [Fact]
        public void ProcessTransactions_EmptyList_ShouldReturnApplicantDataWithEmptyRegistros()
        {
            // Arrange
            var builder = new ApplicantDataBuilder("Felipe", "2025-11-10", 99);

            // Act
            var result = builder.ProcessTransactions(new List<Transaction>());

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Registros);
            Assert.Empty(result.Registros);
        }
    }
}
