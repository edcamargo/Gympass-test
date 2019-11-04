using Gympass.Domain.Interfaces;
using Gympass.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gympass.Domain.Services
{
    public class CorridaServices : ICorridaServices
    {
        private string[] _arrayLogCorrida;
        private int count = 1;
        private readonly StringBuilder strGridChedada = new StringBuilder();

        /// <summary>
        /// Lista o resumo final da prova
        /// </summary>
        /// <param name="_arrayLog"></param>
        public List<LogCorrida> ResultadoCorrida(string[] _arrayLog)
        {
            _arrayLogCorrida = _arrayLog;
            var list = ParseLog();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("   Resultado da Corrida: ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;


            List<LogCorrida> ListaResultCorrida = new List<LogCorrida>();

            foreach (var item in list.Where(x => x.NumVolta > 3).OrderBy(x => x.TempoVolta))
            {
                var listNomePiloto = list.Where(x => x.Piloto.Numero == item.Piloto.Numero).Select(x => x.Piloto.Nome).ToList().Distinct();

                LogCorrida logCorrida = new LogCorrida();

                logCorrida.HoraVolta = item.HoraVolta;
                logCorrida.Piloto = new Piloto()
                {
                    Numero = item.Piloto.Numero,
                    Nome = listNomePiloto.First()
                };
                logCorrida.NumVolta = item.NumVolta;
                logCorrida.TempoVolta = item.TempoVolta;
                logCorrida.VelocidadeMediaVolta = item.VelocidadeMediaVolta;

                strGridChedada.Clear();
                strGridChedada.AppendLine("   Posição Chegada: " + count++ +
                                          " | Código Piloto: " + item.Piloto.Numero +
                                          " | Nome Piloto: " + listNomePiloto.First() +
                                          " | Qtde Voltas Completadas: " + item.NumVolta +
                                          " | Tempo Total de Prova: " + item.TempoVolta); ; ;

                Console.WriteLine(strGridChedada);

                ListaResultCorrida.Add(logCorrida);

            }

            return ListaResultCorrida;
        }

        /// <summary>
        /// Retorno melhor volta da corrida
        /// </summary>
        public void MelhorVoltaCorrida()
        {
            var list = ParseLog();
            count = 1;

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("   Melhor Volta da Corrida: ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            foreach (var item in list.GroupBy(x => x.Piloto.Numero).Select(x => x.First()).Take(1))
            {
                var listVolta = list.Where(x => x.Piloto.Numero == item.Piloto.Numero).Min(x => x.TempoVolta);
                strGridChedada.Clear();
                strGridChedada.AppendLine("   Código Piloto: " + item.Piloto.Numero +
                                          " | Nome Piloto: " + item.Piloto.Nome +
                                          " | Tempo de Volta: " + listVolta);

                Console.WriteLine(strGridChedada);
            }
        }

        /// <summary>
        /// Retorno da melhor volta de cada Piloto
        /// </summary>
        public void MelhorVoltaCadaPiloto()
        {
            var list = ParseLog();
            count = 1;

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("   Melhor Volta Cada Piloto: ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            foreach (var item in list.GroupBy(x => x.Piloto.Numero).Select(x => x.First()))
            {
                var listVolta = list.Where(x => x.Piloto.Numero == item.Piloto.Numero).OrderBy(x => x.TempoVolta).Min(x => x.TempoVolta);
                strGridChedada.Clear();
                strGridChedada.AppendLine("   Código Piloto: " + item.Piloto.Numero +
                                          " | Nome Piloto: " + item.Piloto.Nome +
                                          " | Tempo de Volta: " + listVolta);

                Console.WriteLine(strGridChedada);
            }
        }

        /// <summary>
        /// Calcular a velocidade média de cada piloto durante toda corrida
        /// </summary>
        public void VelocidadeMediaCadaPiloto()
        {
            var list = ParseLog();
            count = 1;

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("   Velocidade Média Cada Piloto: ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;

            foreach (var item in list.GroupBy(x => x.Piloto.Numero).Select(x => x.First()))
            {
                var listVolta = list.Where(x => x.Piloto.Numero == item.Piloto.Numero).Sum(x => x.VelocidadeMediaVolta) / list.Count(x => x.Piloto.Numero == item.Piloto.Numero);
                strGridChedada.Clear();
                strGridChedada.AppendLine("   Código Piloto: " + item.Piloto.Numero +
                                          " | Nome Piloto: " + item.Piloto.Nome +
                                          " | Velocidade Média: " + listVolta);

                Console.WriteLine(strGridChedada);
            }
        }

        /// <summary>
        /// Realiza o Parse do Arquivo txt para um List de Log Corrida
        /// </summary>
        /// <param name="_arrayLog"></param>
        /// <returns></returns>
        private List<LogCorrida> ParseLog()
        {
            List<LogCorrida> lst = new List<LogCorrida>();
            foreach (string line in _arrayLogCorrida.Where(x => !x.Contains("Hora")).ToList())
            {
                if (line.Length > 1)
                {
                    LogCorrida corrida = new LogCorrida();

                    corrida.HoraVolta = TimeSpan.Parse(line.Substring(0, 17).Trim());

                    corrida.Piloto = new Piloto()
                    {
                        Numero = Convert.ToInt32(line.Substring(18, 3).Trim()),
                        Nome = line.Substring(23, 35).Trim()
                    };

                    corrida.NumVolta = Convert.ToInt32(line.Substring(57, 2).Trim());

                    TimeSpan ts = new TimeSpan(0, 0,
                    int.Parse(line.Substring(61, 30).Trim().Substring(0, 1)),
                    int.Parse(line.Substring(61, 30).Trim().Substring(2, 2)),
                    int.Parse(line.Substring(61, 30).Trim().Substring(5, 3)));

                    corrida.TempoVolta = ts * corrida.NumVolta;
                    corrida.VelocidadeMediaVolta = Convert.ToDecimal(line.Substring(92, 7).Trim());

                    lst.Add(corrida);
                }
            }

            return lst.OrderBy(x => x.NumVolta).ToList();
        }
    }
}
