
using System;

namespace DBProcessor.Data_Classes
{
    public class Registros
    {
        public string GUID { get; set; }
        public string CodigoPracaPedagio { get; set; }
        public string CodigoCabine { get; set; }
        public string Instante { get; set; }
        public string Sentido { get; set; }
        public string TipoVeiculo { get; set; }
        public string Isento { get; set; }
        public string Evasao { get; set; }
        public string TipoCobrancaEfetuada { get; set; }
        public string ValorDevido { get; set; }
        public string ValorArrecadado { get; set; }
        public string MultiplicadorTarifa { get; set; }
    
        public Registros(Transaction transaction)
        {
            GUID = ConvertObjectIdToGuidLike(transaction.Id);
            CodigoPracaPedagio = int.Parse(transaction.CodigoPracaPedagio).ToString();
            CodigoCabine = transaction.CodigoCabine.ToString();
            Instante = transaction.Instante;
            Sentido = ConverterSentido(transaction.Sentido);
            TipoVeiculo = ConverterTipoVeiculo(transaction.Rodagem);
            Isento = ConvertIsento(transaction.Isento);
            Evasao = ConvertEvasao(transaction.Evasao);
            TipoCobrancaEfetuada = ConvertTipoCobrancaToString(transaction.TipoCobranca);
            ValorDevido = FormatDecimal(transaction.ValorDevido);
            ValorArrecadado = FormatDecimal(transaction.ValorArrecadado);
            MultiplicadorTarifa = CalculateTariffModifier(transaction.Rodagem, transaction.Isento);
        }


        public static string ConvertObjectIdToGuidLike(string objectId)
        {
            objectId = objectId.Trim().ToLower();

            if (objectId.Length != 24)
                throw new ArgumentException("O ObjectId precisa ter 24 caracteres hexadecimais.");

            string padded = objectId.PadRight(32, '0');

            
            string formatted =
                $"{padded.Substring(0, 8)}-" +
                $"{padded.Substring(8, 4)}-" +
                $"{padded.Substring(12, 4)}-" +
                $"{padded.Substring(16, 4)}-" +
                $"{padded.Substring(20, 12)}";

            return formatted;
        }

        public static string ConverterSentido(int sentido)
        {
            return sentido switch
            {
                1 => "Crescente",
                2 => "Decrescente",
                _ => $"{sentido} -Desconhecido"
            };
        }

        public static string ConverterTipoVeiculo(int rodagem)
        {
            return rodagem switch
            {
                1 => "Passeio",
                2 => "Comercial",
                3 => "Moto",
                _ => "Desconhecido"
            };
        }

        public static string ConvertIsento(int value)
        {
            return value switch
            {
                1 => "Sim",
                2 => "Não",
                _ => throw new ArgumentException("Valor inválido! Use 1 para Sim ou 2 para Não.")
            };
        }

        public static string ConvertEvasao(int value)
        {
            return value switch
            {
                1 => "Sim",
                2 => "Não",
                _ => throw new ArgumentException("Valor inválido! Use 1 para Sim ou 2 para Não.")
            };
        }

        public static string ConvertTipoCobrancaToString(int tipo)
        {
            return tipo switch
            {
                1 => "Manual",
                2 => "TAG",
                3 => "OCR/Placa",
                _ => "Desconhecido"
            };
        }

        public static string FormatDecimal(decimal valor)
        {
            return valor.ToString("F2", System.Globalization.CultureInfo.InvariantCulture);
        }

        public static string CalculateTariffModifier(int type, int exempt)
        {
            if(type == 3 & exempt == 1)
            {
                return "0";
            }

            if (type == 3 & exempt == 2)
            {
                return "0.5";
            }

            if(type == 1)
            {
                return GetRandomMultiplier1to2().ToString();
            }

            if(type == 2)
            {
                return GetRandomIntegerBetween2And20().ToString();
            }

            throw new ArgumentException($"Valor inválido! type {type} | exempt {exempt}");
        }

        public static decimal GetRandomMultiplier1to2()
        {
            decimal[] valores = { 1.0m, 1.5m, 2.0m };
            Random random = new Random();
            int index = random.Next(valores.Length);
            return valores[index];
        }

        public static int GetRandomIntegerBetween2And20()
        {
            Random random = new Random();
            return random.Next(2, 21); 
        }
    }


}
