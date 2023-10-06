using EspacioPedido;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace EspacioAccesoADatos
{
    public abstract class AccesoADatos // vendria a ser la clase padre
    {
        public virtual List<Cadete> LeerCadetes(string nombreArchivo){
            return null;
        }
        public virtual List<Cadeteria> LeerCadeteria(string nombreArchivo){
            return null;
        }
    }

    public class AccesoCSV: AccesoADatos{ // heredan de Acceso a Datos
        public override List<Cadete> LeerCadetes(string nombreArchivo){

            List<Cadete> ListaCadete = new List<Cadete>(); // instanciamos la lista de los cadetes
            //FileStream MiArchivo = new FileStream(nombreArchivo, FileMode.Open);
            StreamReader StrReader = new StreamReader(nombreArchivo);
            string Linea = "";
            
            while ((Linea = StrReader.ReadLine()) != null) // recoremos las lineas del archivo 
            {
                string[] fila = Linea.Split(",").ToArray(); // la separamos de acuerdo al caracter
                Cadete cad = new Cadete(int.Parse(fila[0]), fila[1], fila[2], fila[3]); // llamamos al constructo de cdete
                ListaCadete.Add(cad); // añadimos el cadete a la lista
            }

            return ListaCadete; // devolvemos la lista de cadete
        }
    

        public override List<Cadeteria> LeerCadeteria(string nombreArchivo) // el nombre contiene el nombre del archivo y la ruta del mismo
        {
            List<Cadeteria> ListaCadeteria= new List<Cadeteria>(); // instanciamos la lista de la cadeteria
            //FileStream MiArchivo = new FileStream(nombreArchivo, FileMode.Open);
            StreamReader StrReader = new StreamReader(nombreArchivo);
            string? Linea = "";
            
            while ((Linea = StrReader.ReadLine()) != null) // recoremos las lineas del archivo 
            {
                string[] fila = Linea.Split(",").ToArray(); // la separamos de acuerdo al caracter
                Cadeteria cadeteria = new Cadeteria(fila[0], fila[1]);// llamamos al constructo de cdete
                ListaCadeteria.Add(cadeteria); // añadimos el cadete a la lista
            }
            return ListaCadeteria; // devolvemos la lista de cadete
        }
    
    }
    public class AccesoJSON : AccesoADatos{ // heredan de Acceso a Datos
        public override List<Cadete> LeerCadetes(string nombreArchivo)
        {
            if (File.Exists(nombreArchivo))
            {
                string? documento = File.ReadAllText(nombreArchivo); // Leemos todo el archivo
                List<Cadete>? ListaCadete = JsonSerializer.Deserialize<List<Cadete>>(documento);
                return ListaCadete;
            }else
            {
                return null;
            }
            // string documento;
            // using (var archivoOpen = new FileStream(nombreArchivo, FileMode.Open))
            // {
            //     using (var strReader = new StreamReader(archivoOpen))
            //     {
            //         documento = strReader.ReadToEnd();
            //         archivoOpen.Close();
            //     }
            // }

            // List<Cadete> ? ListaCadetes = JsonSerializer.Deserialize<List<Cadete>>(documento);
           
            // return ListaCadetes;
        }
        public override List<Cadeteria> LeerCadeteria(string nombreArchivo)
        {
            // if (File.Exists(nombreArchivo))
            // {
                string? documento = File.ReadAllText(nombreArchivo); // Leemos todo el archivo
                List<Cadeteria>? ListaCadeteria = JsonSerializer.Deserialize<List<Cadeteria>>(documento);
                return ListaCadeteria;
            // }else
            // {
            //     return null;
            // }

        }
    }
}
      