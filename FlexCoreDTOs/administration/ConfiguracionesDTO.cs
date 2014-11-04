using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoreDTOs.administration
{
    public class ConfiguracionesDTO
    {
        public DateTime fechaHoraSistema;
        public Decimal compraDolar;
        public Decimal ventaDolar;
        public Decimal tasaInteresAhorro;

        public ConfiguracionesDTO(Decimal compraDolar, Decimal ventaDolar, DateTime fechaHoraSistema,
            Decimal tasaInteresAhorro)
        {
            this.compraDolar = compraDolar;
            this.ventaDolar = ventaDolar;
            this.fechaHoraSistema = fechaHoraSistema;
            this.tasaInteresAhorro = tasaInteresAhorro;
        }

        public DateTime getFechaHoraActual() { return this.fechaHoraSistema; }
        public Decimal getCompraDolar() { return this.compraDolar; }
        public Decimal getVentaDolar() { return this.ventaDolar; }
        public Decimal getTasaInteresAhorro() { return this.tasaInteresAhorro; }
    }
}
