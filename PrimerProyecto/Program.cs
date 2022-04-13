using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace PrimerProyecto
{
    class Program
    {
        static void Main(string[] args)
        {
            string valorEntrada = "";
            DateTime fecha;
            DateTime hora;
            DateTime fechaHora;
            Boolean imprimirOK = false;
            Boolean errorEntrada = false;
            string estadoString;
            int DIMENSION_ENTRADA = 25;
            List<Medicion> listaMediciones = new List<Medicion>();
           
            do
            {
            Console.WriteLine("Ingresar informacion");
            valorEntrada = Console.ReadLine();

                if (valorEntrada == "" && listaMediciones.Count < 1) {
                    Console.WriteLine("no ingreso ningun valor");
                    imprimirOK = false;
                    continue;
                }

                if (valorEntrada == "") {
                    imprimirOK = true;
                    break; }              

                if (valorEntrada.Length != DIMENSION_ENTRADA) {
                  imprimirOK = false;
                  Console.WriteLine("Valor ingresado incorrecto");
                  continue;
                }                    
                
               errorEntrada = false;
               imprimirOK = false;
               string fec = valorEntrada.Substring(0, 8);
               string[] formatFecha = { "yyyyMMdd" };
               string[] formatHora = { "HHmmss" };
               string[] formatFechaHora = { "yyyyMMddHHmmss" };
               string horastring = valorEntrada.Substring(8, 6);
               string temp = valorEntrada.Substring(14, 3);
               string humedadString = valorEntrada.Substring(17, 3);
               string codigo = valorEntrada.Substring(20, 4);
               string estado = valorEntrada.Substring(24, 1);
               string fechaHoraString = valorEntrada.Substring(0, 14);

               if (!float.TryParse(temp, out float temperatura))
               {
                 Console.WriteLine($"Temperatura incorrecta: {temperatura}");
                 errorEntrada = true;
                 continue;
               }

               if (!float.TryParse(humedadString, out float humedad))
               {
                 Console.WriteLine($"Humedad incorrecta: {humedad}");
                 errorEntrada = true;
                 continue;
               }

                if (!DateTime.TryParseExact(fec, formatFecha, System.Globalization.CultureInfo.InvariantCulture,
                                       System.Globalization.DateTimeStyles.None, out fecha))              
               {
                 Console.WriteLine("fecha incorrecta {0}", fecha);
                 errorEntrada = true;
                 continue;
               }

               if (!DateTime.TryParseExact(horastring, formatHora, System.Globalization.CultureInfo.InvariantCulture,
                   System.Globalization.DateTimeStyles.None, out hora))
               {
                 Console.WriteLine("hora incorrecta");
                 errorEntrada = true;
                 continue;
               }
               fechaHora = DateTime.ParseExact(fechaHoraString,formatFechaHora, System.Globalization.CultureInfo.InvariantCulture);
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
                 errorEntrada = true;
                 break;
               }
               
               if (!errorEntrada)
               {
               humedad /= 10;
               temperatura /= 10;
               string campoFechaHora = ($"Fecha/Hora del registro: {fecha.ToString("yyyy")}/{fecha.ToString("MM")}/{fecha.ToString("dd")} {hora.ToString("HH")}:{hora.ToString("mm")}:{hora.ToString("ss")}.{hora.ToString("fff")} ");
               DateTime campoDatetime = fechaHora;
               string campoTemp = $"Temperatura: {temperatura}°";
               string campoHumedad = $"Humedad: {humedad}% ";
               string campoCodigo = $"Codigo: “{codigo}” ";
               string campoEstado = estadoString;
               Medicion muestreo = new Medicion(campoFechaHora, campoTemp, campoHumedad, campoCodigo, campoEstado, campoDatetime);
               listaMediciones.Add(muestreo);
               }   
               
            } while (!imprimirOK);

            // OPCIONES DE ORDENAMIENTO VARIANTE 3
            //ordenamientoBurbujeo();
            //ordenamientoDelegado();
            ordenamientoLinq();
            
            foreach (Medicion aMedicion in listaMediciones)
            {
                Console.WriteLine("**********************");
                Console.WriteLine(aMedicion.GetFechaHora());
                Console.WriteLine(aMedicion.GetTemperatura());
                Console.WriteLine(aMedicion.GetHumedad());
                Console.WriteLine(aMedicion.GetCodigo());
                Console.WriteLine(aMedicion.GetEstado());                
            }

            void ordenamientoBurbujeo()
            {
                Medicion t;
                for (int a = 1; a < listaMediciones.Count; a++)
                {
                    for (int b = listaMediciones.Count - 1; b >= a; b--)
                    {
                        if (listaMediciones[b - 1].GetDateTime() > listaMediciones[b].GetDateTime())
                        {
                            t = listaMediciones[b - 1];
                            listaMediciones[b - 1] = listaMediciones[b];
                            listaMediciones[b] = t;
                        }
                    }
                }
            }

            void ordenamientoDelegado()
            {                
                    listaMediciones.Sort(delegate (Medicion x, Medicion y) {
                        //return x.GetDateTime().CompareTo(y.GetDateTime());
                        if (x.GetDateTime() == null && y.GetDateTime() == null) return 0;
                        else if (x.GetDateTime() == null) return -1;
                        else if (y.GetDateTime() == null) return 1;
                        else return x.GetDateTime().CompareTo(y.GetDateTime());
                    });                
            }
            
            void ordenamientoLinq() {
                listaMediciones = listaMediciones.OrderBy(Medicion => Medicion.GetDateTime()).ToList();
            }

        }        
    }
}
