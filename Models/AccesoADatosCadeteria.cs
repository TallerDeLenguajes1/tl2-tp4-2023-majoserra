namespace EspacioAccesoADatosCadeteria;
using EspacioPedido;
using System.Text.Json;
public class AccesoADatosCadeteria{
    public Cadeteria Obtener()
        {
             if (File.Exists("Cadeteria.json"))
             {
                string? documento = File.ReadAllText("Cadeteria.json"); // Leemos todo el archivo
                List<Cadeteria>? ListaCadeteria = JsonSerializer.Deserialize<List<Cadeteria>>(documento);
                return ListaCadeteria[0]; // devolvemos una sola cadeteria 
             }else
             {
                 return null;
             }

        }
}

