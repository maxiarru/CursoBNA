using System;
using System.Collections.Generic;
using System.Text;

namespace PrimerProyecto
{
    class Medicion
    {
        string fechaHora;
        string temperatura;
        string humedad;
        string codigo;
        string estado;
        DateTime fecha;

        public Medicion(string campo1, string campo2, string campo3, string campo4, string campo5 , DateTime campo6) { 
            this.fechaHora = campo1;
            this.temperatura = campo2;
            this.humedad = campo3;
            this.codigo = campo4;
            this.estado = campo5;
            this.fecha = campo6;
        }
        public string GetFechaHora() {
            return fechaHora;
        }
        public DateTime GetDateTime() {
            return fecha;
        }
        public string GetTemperatura()
        {
            return temperatura;
        }
        public string GetHumedad()
        {
            return humedad;
        }
        public string GetCodigo()
        {
            return codigo;
        }
        public string GetEstado()
        {
            return estado;
        }
    }
}

