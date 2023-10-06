using System.ComponentModel;
using System.Data.Common;
using EspacioAccesoADatos;

namespace EspacioPedido;

public class Cadeteria
{
    private string? nombre;
    private string? telefono;
    private List<Cadete> ListaCadete = new List<Cadete>();

    /*Agregar ListadoPedidos en la clase Cadeteria que contenga todo los pedidos que
    se vayan generando.*/
    private List<Pedido> listaPedido = new List<Pedido>();
    public List<Pedido> ListaPedido { get => listaPedido; }
    public string? Nombre { get => nombre; set => nombre = value; }
    public string? Telefono { get => telefono; set => telefono = value; }

    private static Cadeteria? instance; // instancia de cadeteriaa
    public static Cadeteria GetInstance(){
        if (instance == null)
        {
            var Json = new AccesoJSON(); 
            List<Cadeteria> listaCadeteria = Json.LeerCadeteria("Cadeteria.json"); // Falta la ruta del archivo
            instance = listaCadeteria[0];
            var listaCadetes = Json.LeerCadetes("Cadetes.json");
            instance.AgregarCadetes(listaCadetes);
        }
        return instance;
    }
    
    public List<Pedido> GetPedidos(){ // Retornar Lista de Pedidos
        return listaPedido;
    }

    public List<Cadete> GetCadetes(){//Retornar Lista de Cadetes
        return ListaCadete;
    }
    public Pedido AgregarPedido(Pedido pedido){
        pedido.Id_Cadete= 99;
        pedido.Numero = listaPedido.Count(); // se incremente solo 
        listaPedido.Add(pedido);
        return pedido;
    }
    //Constructor de Cadeteria
    public Cadeteria(){

    }
    public Cadeteria(string nombre, string telefono)
    {
        this.nombre = nombre;
        this.telefono = telefono;

    }
    
    public void AgregarCadetes(List<Cadete> Lista){
        ListaCadete = Lista;
    }
    // Aceptar un pedido y ponerlo en "Espera"
    public void AceptarPedido(int num, string obs, string nomb, string dir, string telef, string datos)
    {
        Pedido pedido = new Pedido(num, obs, nomb, dir, telef, datos); //Creamos el pedido
        listaPedido.Add(pedido); // Agregar los pedido a la Lista de Pedidos
    }

    // Asignar un Pedido a un Cadete 
    /* Agregar el método AsignarCadeteAPedido en la clase Cadeteria que recibe como
    parámetro el id del cadete y el id del Pedido*/
    public Pedido AsignarPedido(int id_pedido, int idcad)
    {
        Cadete? cadBuscado = ListaCadete.FirstOrDefault(cad => cad.Id == idcad);
        Pedido? pedBuscado = listaPedido.FirstOrDefault(p => p.Numero == id_pedido);
        pedBuscado.Id_Cadete = cadBuscado.Id;
        return pedBuscado;  
    }
    public void CrearCadete(int id, string nomb, string dir, string telef)
    {
        Cadete cad = new Cadete(id, nomb, dir, telef);
        ListaCadete.Add(cad);
    }

    public Pedido ReasignarPedido(int idPedido, int idNuevoCadete)
    {
        Pedido? pedBuscado = listaPedido.FirstOrDefault(p => p.Numero == idPedido);
        Cadete? cadBuscado = ListaCadete.FirstOrDefault(cad => cad.Id == idNuevoCadete);
        pedBuscado.Id_Cadete = cadBuscado.Id;
        return pedBuscado;
    }

    public Pedido CambiarEstadoDePedido(int id_pedido, int estado) //FAlTA controlar que el pedido tenga un cadete asociado
    {
        Pedido? pedEncontrado = ListaPedido.FirstOrDefault(p => p.Numero == id_pedido);
        pedEncontrado.Estado = estado;
        return pedEncontrado;
    }
    public int EnviosEntregados(int id_cad)
    {
        int cantEnvios = 0;

        foreach (var ped in listaPedido)
        {
            if (ped.Id_Cadete!=99)//controlamos que los cadetes de esos pedidos sean los mismos y el estado sea 2 (Entregado)
            {
                if (ped.Id_Cadete== id_cad && ped.Estado == 2)
                {
                    cantEnvios++;
                }   
            }
        }
        return cantEnvios;
    }
    // Agregar el método JornalACobrar en la clase Cadeteria que recibe como parámetro el id del cadete y devuelve el monto a cobrar para dicho cadete

    public float JornalACobrar(int id_cad)
    {
        return EnviosEntregados(id_cad) * 500;
    }
}