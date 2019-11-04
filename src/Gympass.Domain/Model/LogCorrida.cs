using System;

namespace Gympass.Domain.Model
{
    public class LogCorrida
    {
        public TimeSpan HoraVolta { get; set; }

        public Piloto Piloto { get; set; }

        public int NumVolta { get; set; }

        public TimeSpan TempoVolta { get; set; }

        public decimal VelocidadeMediaVolta { get; set; }
    }
}
