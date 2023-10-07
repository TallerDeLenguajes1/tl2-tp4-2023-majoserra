namespace EspacioAccesoADatosCadetes;
using EspacioPedido;
using System.Text.Json;
public class AccesoADatosCadetes{
    public  List<Cadete> Obtener()
        {
            if (File.Exists("Cadetes.json"))
            {
                string? documento = File.ReadAllText("Cadetes.json"); // Leemos todo el archivo
                List<Cadete>? ListaCadete = JsonSerializer.Deserialize<List<Cadete>>(documento);
                return ListaCadete;
            }else
            {
                return null;
            }
        }
}