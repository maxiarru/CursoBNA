using System;

namespace PrimerProyecto
{
    class Program
    {
        public enum ParametroEntrada
        {
            longformat , 
            shortformat
        }
        static void Main(string[] args)
        {            
            Boolean parametroOk = true;
            
            if (args.Length > 0 && (args[0] != ParametroEntrada.longformat.ToString() && args[0] !=  ParametroEntrada.shortformat.ToString() ))
            {
                Console.WriteLine($"Error parametro inicial {args[0]}, debe ser unico y de los valores acordados en documentacion ");
                parametroOk = false;                
            }
            
            String valorEntrada = "";
            int DIMENSION_ENTRADA = 25;
            DateTime fecha;
            DateTime hora;
            Boolean entradaOK;
            string estadoString;

            do
             {
                if (!parametroOk) { break; }

                Console.WriteLine("Ingresar informacion");
                valorEntrada = Console.ReadLine();

                if (valorEntrada.Length != DIMENSION_ENTRADA) {
                    Console.WriteLine("Error al ingresar informacion, chequear documento funcional");
                    entradaOK = false;
                    break;
                }
                        
                entradaOK = true;
                string fec = valorEntrada.Substring(0, 8);
                string[] formatFec = { "yyyyMMdd" };
                string[] formatHora = { "HHMMss" };

                string horastring     = valorEntrada.Substring(8, 6);
                string temp           = valorEntrada.Substring(14, 3);
                string humedadString  = valorEntrada.Substring(17, 3);
                string codigo         = valorEntrada.Substring(20, 4);
                string estado         = valorEntrada.Substring(24, 1);
              
                if (!float.TryParse(temp, out float temperatura))
                {
                    Console.WriteLine($"Temperatura incorrecta: {temperatura}");
                    entradaOK = false;
                    break;
                }

                if (!float.TryParse(humedadString, out float humedad))
                {
                    Console.WriteLine($"Temperatura incorrecta: {humedad}");
                    entradaOK = false;
                    break;
                }

                if (!DateTime.TryParseExact(fec, formatFec, System.Globalization.CultureInfo.InvariantCulture,
                                          System.Globalization.DateTimeStyles.None,
                                          out fecha))
                {
                 Console.WriteLine($"fecha incorrecta {fecha}");
                 entradaOK = false;
                 break;
                }

                if (!DateTime.TryParseExact(horastring, formatHora, System.Globalization.CultureInfo.InvariantCulture,
                                          System.Globalization.DateTimeStyles.None,
                                          out hora))
                {
                 Console.WriteLine($"hora incorrecta --> {hora}");
                 entradaOK = false;
                 break;
                }
                            
                switch (estado)
                {
                 case "0":
                   estadoString = "Activo : NO";
                   break;
                 case "1":
                   estadoString = "Activo : SI";
                   break;
                 default:
                   estadoString = "Estado Incorrecto";
                   Console.WriteLine(estadoString);
                   entradaOK = false;
                   break;
                }
                            
                if (args.Length == 0 || args[0] == ParametroEntrada.longformat.ToString()) {
                    Console.WriteLine($"Fecha del registro: {fecha.ToString("yyyy")}/{fecha.ToString("MM")}/{fecha.ToString("dd")}");
                    Console.WriteLine($"Hora del registro: {hora.ToString("HH")}hs {hora.ToString("MM")}min {hora.ToString("ss")}seg");
                }
                else {
                    Console.WriteLine($"Fecha/Hora del registro: {fecha.ToString("yyyy")}/{fecha.ToString("MM")}/{fecha.ToString("dd")} {hora.ToString("HH")}:{hora.ToString("MM")}:{hora.ToString("ss")}.{hora.ToString("fff")} ");
                }

                humedad     /= 10;
                temperatura /= 10;
                Console.WriteLine($"Temperatura: {temperatura}°");
                Console.WriteLine($"Humedad: {humedad}% ");
                Console.WriteLine($"Codigo: “{codigo}” ");
                Console.WriteLine(estadoString);

            } while (!entradaOK);       
        }
    }
}
