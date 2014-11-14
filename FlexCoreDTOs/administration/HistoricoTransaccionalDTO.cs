using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoreDTOs.administration
{
    public class HistoricoTransaccionalDTO
    {
        public int idTransaccion;
        public String descripcion;
        public DateTime fechaHoraEntrada;
        public DateTime fechaHoraSalida;
        public string estado;
        public int versionAplicacion;
        public int idCuenta;
        public int tipoTransaccion;

        public HistoricoTransaccionalDTO(int idTransaccion, String descripcion, DateTime fechaHoraEntrada, DateTime fechaHoraSalida, string estado, int versionAplicacion,
            int idCuenta, int tipoTransaccion)
        {
            this.idTransaccion = idTransaccion;
            this.descripcion = descripcion;
            this.fechaHoraEntrada = fechaHoraEntrada;
            this.fechaHoraSalida = fechaHoraSalida;
            this.estado = estado;
            this.versionAplicacion = versionAplicacion;
            this.idCuenta = idCuenta;
            this.tipoTransaccion = tipoTransaccion;
        }

        public HistoricoTransaccionalDTO()
        {
        }

        public int getIdTransaccion() { return this.idTransaccion; }
        public String getDescripcion() { return this.descripcion; }
        public DateTime getFechaHoraEntrada() { return this.fechaHoraEntrada; }
        public DateTime getFechaHoraSalida() { return this.fechaHoraSalida; }
        public string getEstado() { return this.estado; }
        public int getVersionAplicacion() { return this.versionAplicacion; }
        public int getIdCuenta() { return this.idCuenta; }
        public int getTipoTransaccion() { return this.tipoTransaccion; }
        public void setIdTransaccion(int idTransaccion) { this.idTransaccion = idTransaccion; }
        public void setDescripcion(String descripcion) { this.descripcion = descripcion; }
        public void setFechaHoraEntrada(DateTime fechaHoraEntrada) { this.fechaHoraEntrada = fechaHoraEntrada; }
        public void setFechaHoraSalida(DateTime fechaHoraSalida) { this.fechaHoraSalida = fechaHoraSalida; }
        public void setEstado(string estado) { this.estado = estado; }
        public void setVersionAplicacion(int versionAplicacion) { this.versionAplicacion = versionAplicacion; }
        public void setIdCuenta(int idCuenta) { this.idCuenta = idCuenta; }
        public void setTipoTransaccion(int tipoTransaccion) { this.tipoTransaccion = tipoTransaccion; }
    }
}
