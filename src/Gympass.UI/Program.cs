using Gympass.Domain.InfraEstrutura;
using Gympass.Domain.Interfaces;
using Gympass.Domain.Services;
using System;
using System.IO;

namespace Gympass.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            IReadLog _ReadLog = new ReadLog();
            ICorridaServices corridaServices = new CorridaServices();

            string _pathLog = $@"{Directory.GetCurrentDirectory()}\\..\\..\\..\\..\\..\\LogCorrida.txt";

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\r\n \r\n   Seja bem vindo ao resultado da corrida \r\n \r\n");

            var _arrayLog = _ReadLog.ReadResult(_pathLog);

            // Resumo da Prova
            corridaServices.ResultadoCorrida(_arrayLog);
            corridaServices.MelhorVoltaCorrida();
            corridaServices.MelhorVoltaCadaPiloto();
            corridaServices.VelocidadeMediaCadaPiloto();
        }
    }
}
