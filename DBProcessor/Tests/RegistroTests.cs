namespace Tests
{
    using global::DBProcessor.Data_Classes;
    using Xunit;
    using Transaction = global::DBProcessor.Data_Classes.Transaction;

    namespace DBProcessor.Tests
    {
        public class RegistrosTests
        {
            // --- ConvertObjectIdToGuidLike ---
            [Fact]
            public void ConvertObjectIdToGuidLike_ValidId_ReturnsFormatted()
            {
                string objectId = "6734e6d9db151c04900b1b96";
                string result = Registros.ConvertObjectIdToGuidLike(objectId);

                Assert.Equal("6734e6d9-db15-1c04-900b-1b9600000000", result);
            }

            [Fact]
            public void ConvertObjectIdToGuidLike_InvalidLength_Throws()
            {
                Assert.Throws<ArgumentException>(() => Registros.ConvertObjectIdToGuidLike("1234"));
            }

            // --- ConverterSentido ---
            [Theory]
            [InlineData(1, "Crescente")]
            [InlineData(2, "Decrescente")]
            [InlineData(99, "99 -Desconhecido")]
            public void ConverterSentido_ReturnsExpectedValues(int input, string expected)
            {
                string result = Registros.ConverterSentido(input);
                Assert.Equal(expected, result);
            }

            // --- ConverterTipoVeiculo ---
            [Theory]
            [InlineData(1, "Passeio")]
            [InlineData(2, "Comercial")]
            [InlineData(3, "Moto")]
            [InlineData(99, "Desconhecido")]
            public void ConverterTipoVeiculo_ReturnsExpectedValues(int input, string expected)
            {
                string result = Registros.ConverterTipoVeiculo(input);
                Assert.Equal(expected, result);
            }

            // --- ConvertIsento ---
            [Theory]
            [InlineData(1, "Sim")]
            [InlineData(2, "Não")]
            public void ConvertIsento_ReturnsExpectedValues(int input, string expected)
            {
                string result = Registros.ConvertIsento(input);
                Assert.Equal(expected, result);
            }

            [Fact]
            public void ConvertIsento_Invalid_ThrowsException()
            {
                Assert.Throws<ArgumentException>(() => Registros.ConvertIsento(5));
            }

            // --- ConvertEvasao ---
            [Theory]
            [InlineData(1, "Sim")]
            [InlineData(2, "Não")]
            public void ConvertEvasao_ReturnsExpectedValues(int input, string expected)
            {
                string result = Registros.ConvertEvasao(input);
                Assert.Equal(expected, result);
            }

            [Fact]
            public void ConvertEvasao_Invalid_ThrowsException()
            {
                Assert.Throws<ArgumentException>(() => Registros.ConvertEvasao(99));
            }

            // --- ConvertTipoCobrancaToString ---
            [Theory]
            [InlineData(1, "Manual")]
            [InlineData(2, "TAG")]
            [InlineData(3, "OCR/Placa")]
            [InlineData(9, "Desconhecido")]
            public void ConvertTipoCobrancaToString_ReturnsExpectedValues(int input, string expected)
            {
                string result = Registros.ConvertTipoCobrancaToString(input);
                Assert.Equal(expected, result);
            }

            // --- FormatDecimal ---
            [Theory]
            [InlineData(10.5, "10.50")]
            [InlineData(3.14159, "3.14")]
            public void FormatDecimal_ReturnsFormattedString(decimal input, string expected)
            {
                string result = Registros.FormatDecimal(input);
                Assert.Equal(expected, result);
            }

            // --- GetRandomMultiplier1to2 ---
            [Fact]
            public void GetRandomMultiplier1to2_ReturnsAllowedValue()
            {
                decimal value = Registros.GetRandomMultiplier1to2();
                Assert.Contains(value, new decimal[] { 1.0m, 1.5m, 2.0m });
            }

            // --- GetRandomIntegerBetween2And20 ---
            [Fact]
            public void GetRandomIntegerBetween2And20_ReturnsValueInRange()
            {
                int value = Registros.GetRandomIntegerBetween2And20();
                Assert.InRange(value, 2, 20);
            }

            // --- CalculateTariffModifier ---
            [Theory]
            [InlineData(3, 1, "0")]
            [InlineData(3, 2, "0.5")]
            public void CalculateTariffModifier_ReturnsFixedValues(int type, int exempt, string expected)
            {
                string result = Registros.CalculateTariffModifier(type, exempt);
                Assert.Equal(expected, result);
            }

            [Fact]
            public void CalculateTariffModifier_Type1_ReturnsMultiplierValue()
            {
                string result = Registros.CalculateTariffModifier(1, 1);
                decimal val = decimal.Parse(result);
                Assert.Contains(val, new decimal[] { 1.0m, 1.5m, 2.0m });
            }

            [Fact]
            public void CalculateTariffModifier_Type2_ReturnsIntegerValue()
            {
                string result = Registros.CalculateTariffModifier(2, 2);
                int val = int.Parse(result);
                Assert.InRange(val, 2, 20);
            }

            [Fact]
            public void CalculateTariffModifier_InvalidValues_Throws()
            {
                Assert.Throws<ArgumentException>(() => Registros.CalculateTariffModifier(99, 99));
            }
        }

    }
}