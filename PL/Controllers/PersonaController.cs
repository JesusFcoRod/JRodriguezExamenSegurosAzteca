using Microsoft.AspNetCore.Mvc;
using ML;

namespace PL.Controllers
{
    public class PersonaController : Controller
    {
        [HttpGet]

        public ActionResult GetAll()
        {

            string RutaTxt = @"C:/Users/digis/OneDrive/Documents/Jesus Francisco Rodriguez Martinez/JRodriguezExamenSegurosAzteca/PL/wwwroot/Archivos/nombres.txt";

            ML.Persona persona = new ML.Persona();
            List<object> Personas = new List<object>();

            List<string> Nombres = new List<string>();
            List<string> NombresOrden = new List<string>();

            using (StreamReader sr = new StreamReader(RutaTxt))
            {
                string linea;
                while ((linea = sr.ReadLine()) != null)
                {
                    string[] nombres = linea.Split(','); //Para obtener un nombre del txt cada que encuentre una ,

                    foreach (string nombre in nombres)
                    {
                        Nombres.Add(nombre.Trim());
                    }

                    NombresOrden = Nombres.OrderBy(n => n).ToList();//LISTA EN ORDEN ALFABETICO(LINQ)

                    foreach (string nombre in NombresOrden)
                    {
                        persona = new ML.Persona();
                        persona.Nombre = nombre;

                        Personas.Add(persona);

                    }
                }
            }


            persona.Personas = Personas;
            return View(persona);
        }

        [HttpGet]

        public ActionResult Puntuacion(string Nombre)
        {
            //Eliminar comillas de de la cadena o nombre
            char[] CharAeliminar = { '"' };
            string NombreLimpio = Nombre.Trim(CharAeliminar);//Trim permite eliminar o alterar una cadena 

            //Alfabeto arreglo
            char[] Alfabeto = new char[26];

            for (int i = 0; i < 26; i++)
            {
                Alfabeto[i] = (char)('A' + i);
            }

            //Obtener puntuacion del nombre segun el arreglo alfabeto
            int[] Indices = new int[NombreLimpio.Length];

            for (int i = 0; i < NombreLimpio.Length; i++)
            {
                char letra = NombreLimpio[i];//recorremos el nombre letra por letra
                Indices[i] = Array.IndexOf(Alfabeto, letra) + 1;//obtenemos el indice segun la letra del nombre y el arreglo alfabeto
            }

            //Obtenemos la lista de los nombres ordenados alfabeticamente
            List<string> Nombres = ObtenerListaPersonas();


            int IdiceNombre = Nombres.IndexOf(Nombre) + 1;//Indice del nombre en la lista

            int PuntacionIndices = Indices.Sum();//suma de indices del nombre

            int PuntuacionTotal = IdiceNombre * PuntacionIndices;

            ViewBag.Message = "La puntuacion del nombre: " + Nombre + " es de " + PuntuacionTotal;
            return PartialView("Modal");
        }

        public List<string> ObtenerListaPersonas()
        {
            string RutaTxt = @"C:/Users/digis/OneDrive/Documents/Jesus Francisco Rodriguez Martinez/JRodriguezExamenSegurosAzteca/PL/wwwroot/Archivos/nombres.txt";

            ML.Persona persona = new ML.Persona();
            List<object> Personas = new List<object>();

            List<string> Nombres = new List<string>();
            List<string> NombresOrden = new List<string>();

            using (StreamReader sr = new StreamReader(RutaTxt))
            {
                string linea;
                while ((linea = sr.ReadLine()) != null)
                {
                    string[] nombres = linea.Split(','); //Para obtener un nombre del txt cada que encuentre una ,

                    foreach (string nombre in nombres)
                    {
                        Nombres.Add(nombre.Trim());
                    }
                    NombresOrden = Nombres.OrderBy(n => n).ToList();//LISTA EN ORDEN ALFABETICO(LINQ)
                }
            }
            return NombresOrden;
        }
    }
}
