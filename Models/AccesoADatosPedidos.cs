using System.Text.Json;
using EspacioPedido;

namespace EspacioAccesoADatosPedidos;

public class AccesoADatosPedidos{
    
    public List<Pedido> Obtener(){ // clase para obtener los archivos json 

        string? jsonPedidos = File.ReadAllText("Pedido.json");
        List<Pedido> listaPedidos = JsonSerializer.Deserialize<List<Pedido>>(jsonPedidos);
        return listaPedidos;
    }

    public bool Guardar(List<Pedido> pedidos){
        string listaPedidos = JsonSerializer.Serialize(pedidos);
        File.WriteAllText("Pedido.json", listaPedidos);
        if (listaPedidos != null)
        {
            return true;
        }else{
            return false;
        }
    }
}