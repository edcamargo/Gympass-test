using Gympass.Domain.Model;
using System.Collections.Generic;

namespace Gympass.Domain.Interfaces
{
    public interface ICorridaServices
    {
        /// <summary>
        /// Lista o resumo final da prova
        /// </summary>
        /// <param name="_arrayLog"></param>
        List<LogCorrida> ResultadoCorrida(string[] _arrayLog);

        /// <summary>
        /// Retorno melhor volta da corrida
        /// </summary>
        void MelhorVoltaCorrida();

        /// <summary>
        /// MelhorVoltaCadaPiloto(
        /// </summary>
        void MelhorVoltaCadaPiloto();

        /// <summary>
        /// Calcular a velocidade média de cada piloto durante toda corrida
        /// </summary>
        void VelocidadeMediaCadaPiloto();
    }
}
