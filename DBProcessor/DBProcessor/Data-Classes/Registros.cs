
namespace DBProcessor.Data_Classes
{
    public class Registros
    {
        public string GUID { get; set; }
        public int CodigoPracaPedagio { get; set; }
        public int CodigoCabine { get; set; }
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
            CodigoPracaPedagio = int.Parse(transaction.CodigoPracaPedagio);
            CodigoCabine = transaction.CodigoCabine;
            Instante = transaction.Instante;
            Sentido = ConverterSentido(transaction.Sentido);
            TipoVeiculo = ConverterTipoVeiculo(transaction.Rodagem);
            Isento = ConvertIsento(transaction.Isento);
            Evasao = ConvertEvasao(transaction.Evasao);
            TipoCobrancaEfetuada = ConvertTipoCobrancaToString(transaction.TipoCobranca);
            ValorDevido = FormatDecimal(transaction.ValorDevido);
            ValorArrecadado = FormatDecimal(transaction.ValorArrecadado);
            MultiplicadorTarifa = CalculateTariffModifier(transaction.Rodagem, transaction.Isento);
            Console.WriteLine($"GUID {GUID}");
            Console.WriteLine($"CodigoPracaPedagio {CodigoPracaPedagio}");
            Console.WriteLine($"CodigoCabine {CodigoCabine}");
            Console.WriteLine($"Instante {Instante}");
            Console.WriteLine($"Sentido {Sentido}");
            Console.WriteLine($"Tipo Veiculo {TipoVeiculo}");
            Console.WriteLine($"Isento {Isento}");
            Console.WriteLine($"Evasao {Evasao}");
            Console.WriteLine($"Tipo de Cobença Efetuada {TipoCobrancaEfetuada}");
            Console.WriteLine($"Valor Devido {ValorDevido}");
            Console.WriteLine($"Valor Arrecadado {ValorArrecadado}");
            Console.WriteLine($"Multiplicador de Tarifa {MultiplicadorTarifa}");
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

        string ConverterSentido(int sentido)
        {
            return sentido switch
            {
                1 => "Crescente",
                2 => "Decrescente",
                _ => $"{sentido} -Desconhecido"
            };
        }

        string ConverterTipoVeiculo(int rodagem)
        {
            return rodagem switch
            {
                1 => "Passeio",
                2 => "Comercial",
                3 => "Moto",
                _ => "Desconhecido"
            };
        }

        string ConvertIsento(int value)
        {
            return value switch
            {
                1 => "Sim",
                2 => "Não",
                _ => throw new ArgumentException("Valor inválido! Use 1 para Sim ou 2 para Não.")
            };
        }

        string ConvertEvasao(int value)
        {
            return value switch
            {
                1 => "Sim",
                2 => "Não",
                _ => throw new ArgumentException("Valor inválido! Use 1 para Sim ou 2 para Não.")
            };
        }

        string ConvertTipoCobrancaToString(int tipo)
        {
            return tipo switch
            {
                1 => "Manual",
                2 => "TAG",
                3 => "OCR/Placa",
                _ => "Desconhecido"
            };
        }

        string FormatDecimal(decimal valor)
        {
            return valor.ToString("F2", System.Globalization.CultureInfo.InvariantCulture);
        }

        string CalculateTariffModifier(int type, int exempt)
        {
            if(type == 3 & exempt == 1)
            {
                return "0";
            }

            if (type == 3 & exempt == 1)
            {
                return "0";
            }

            if(type == 1)
            {
                return "1.5";
            }

            if(type == 2)
            {
                return "3";
            }

            throw new ArgumentException($"Valor inválido! type {type} | exempt {exempt}");
        }
    }
}
