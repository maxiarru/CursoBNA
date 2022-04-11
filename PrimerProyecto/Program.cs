using System;

namespace PrimerProyecto
{
    class Program
    {
        static void Main(string[] args)
        {
            // utilizo string[] en parametroEntrada por si en algun momento son multiples opciones, seria man sencillo consultarlas con un for  que poner todas las opciones.
            string[] parametroEntrada = { "longformat" , "shortformat"};
            Boolean parametroOk = true;
            
            if (args.Length > 0 && (args[0] != parametroEntrada[0] && args[0] != parametroEntrada[1]))
            {
                Console.WriteLine("Error parametro inicial {0}, debe ser unico y de los valores acordados en documentacion ", args[0]);
                parametroOk = false;                
            }
            
            String valorEntrada = "";
            int tamañoCorrectoEntrada = 25;
            DateTime fecha;
            DateTime hora;
            float temperatura = 0;
            float humedad = 0;
            Boolean entradaOK;
            string estadoString;

            do
             {
                if (!parametroOk) { break; }

                Console.WriteLine("Ingresar informacion");
                valorEntrada = Console.ReadLine();

                if (valorEntrada.Length != tamañoCorrectoEntrada) {
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

                try
                {
                 temperatura = float.Parse(temp) / 10;
                }
                catch (Exception error)
                {                
                 Console.WriteLine(" Temperatura incorrecta: {0}", error.Message);
                 entradaOK = false;
                 break;
                }

                try
                {
                 humedad = float.Parse(humedadString) / 10;
                }
                catch (Exception error)
                {                  
                  Console.WriteLine(" Humedad incorrecta: {0}", error.Message);
                  entradaOK = false;
                  break;
                }

                if (!DateTime.TryParseExact(fec, formatFec, System.Globalization.CultureInfo.InvariantCulture,
                                          System.Globalization.DateTimeStyles.None,
                                          out fecha))
                {
                 Console.WriteLine("fecha incorrecta {0}", fecha);
                 entradaOK = false;
                 break;
                }

                if (!DateTime.TryParseExact(horastring, formatHora, System.Globalization.CultureInfo.InvariantCulture,
                                          System.Globalization.DateTimeStyles.None,
                                          out hora))
                {
                 Console.WriteLine("hora incorrecta");
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
                            
                if (args.Length == 0 || args[0] == parametroEntrada[0]) {
                    Console.WriteLine($"Fecha del registro: {fecha.ToString("yyyy")}/{fecha.ToString("MM")}/{fecha.ToString("dd")}");
                    Console.WriteLine($"Hora del registro: {hora.ToString("HH")}hs {hora.ToString("MM")}min {hora.ToString("ss")}seg");
                }
                else {
                    Console.WriteLine($"Fecha/Hora del registro: {fecha.ToString("yyyy")}/{fecha.ToString("MM")}/{fecha.ToString("dd")} {hora.ToString("HH")}:{hora.ToString("MM")}:{hora.ToString("ss")}.{hora.ToString("fff")} ");
                }

                Console.WriteLine("Temperatura: {0}°", temperatura);
                Console.WriteLine("Humedad: {0}% ", humedad);
                Console.WriteLine("Codigo: “{0}” ", codigo);
                Console.WriteLine(estadoString);

            } while (!entradaOK);  
     
        }
    }
}
