using Gympass.Domain.InfraEstrutura;
using Gympass.Domain.Interfaces;
using Gympass.Domain.Services;
using NSubstitute;
using System.ComponentModel;
using Xunit;

namespace Gympass.Test
{
    public class UnitTestCorrida
    {
        private static string _pathLog = @"C:\\Gympass\\test\\Gympass.Test\\Log\\LogCorrida.txt";

        private static IReadLog _ReadLog;
        private static ICorridaServices _corridaServices;

        public UnitTestCorrida()
        {
            _ReadLog = Substitute.For<ReadLog>();
            _corridaServices = Substitute.For<CorridaServices>();
        }

        [Theory(DisplayName = "Retorna o vencedor da Prova")]
        [InlineData(38)]
        [Description("Input do usuário - 38 F.MASSA")]
        public void PilotoGanhador(int NumeroPiloto)
        {
            var _arrayLog = _ReadLog.ReadResult(_pathLog);
            var test = _corridaServices.ResultadoCorrida(_arrayLog).Find(x => x.Piloto.Numero == NumeroPiloto);

            Assert.Equal(test.Piloto.Numero, NumeroPiloto);
        }
    }
}
