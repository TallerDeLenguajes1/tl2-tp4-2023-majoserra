using System.ComponentModel;
using System.Data.Common;
using EspacioAccesoADatosCadeteria;
using EspacioAccesoADatosCadetes;
using EspacioAccesoADatosPedidos;
using EspacioInforme;

namespace EspacioPedido;

public class Cadeteria
{
    private string? nombre;
    private string? telefono;
    private List<Cadete> ListaCadete;
    private List<Pedido>? listaPedido;
    private AccesoADatosPedidos accesoPedidos;
    private AccesoADatosCadetes accesoCadetes;
    public string? Nombre { get => nombre; set => nombre = value; }
    public string? Telefono { get => telefono; set => telefono = value; }

    // Comentamos el Singleton , intentamos no trabajar con este 

    /*private static Cadeteria? instance; // instancia de cadeteriaa
    public static Cadeteria GetInstance(){
        if (instance == null)
        {
            var JsonCadeteria = new AccesoADatosCadeteria(); 
            Cadeteria cadeteria = JsonCadeteria.Obtener(); // Falta la ruta del archivo
            instance = cadeteria;
            var JsonCadete = new AccesoADatosCadetes(); 
            var listaCadetes = JsonCadete.Obtener();
            instance.AgregarCadetes(listaCadetes);
            instance.
        }
        return instance;
    }*/
    public Cadeteria(AccesoADatosCadeteria accesoCadeteria, AccesoADatosCadetes accesoCadetes, AccesoADatosPedidos accesoPedidos){
        Cadeteria cadeteria = accesoCadeteria.Obtener();
        this.nombre = cadeteria.nombre;
        this.telefono = cadeteria.telefono;
        // Obtenemos los Cadetes
        accesoCadetes = new AccesoADatosCadetes();
        ListaCadete = accesoCadetes.Obtener();
        // Obtenemos los pedidos
        accesoPedidos = new AccesoADatosPedidos();
        this.accesoPedidos = accesoPedidos;
        listaPedido = accesoPedidos.Obtener();
        // cargamos los datos cadeteria
        
    }
    public Cadeteria(){ // contructor vacio

    }
    public Cadeteria(string nombre, string telefono)
    {
        this.nombre = nombre;
        this.telefono = telefono;
        ListaCadete = new List<Cadete>();
        listaPedido = new List<Pedido>();
    }

    public Informe GetInforme(int id_cad){
        AccesoADatosCadeteria accesoCadeteria = new AccesoADatosCadeteria();
        var cadeteria = accesoCadeteria.Obtener();
        var info = new Informe(id_cad, cadeteria);
        return info;
    }
    
    public List<Pedido> GetPedidos(){ // Retornar Lista de Pedidos
        return listaPedido;
    }

    public List<Cadete> GetCadetes(){//Retornar Lista de Cadetes
        return ListaCadete;
    }
    public bool AgregarPedido(Pedido pedido){
        var pedidos = new AccesoADatosPedidos();
        pedido.Id_Cadete= 99;
        pedido.Numero = listaPedido.Count(); // se incremente solo
        listaPedido.Add(pedido);
        if (pedidos.Guardar(listaPedido))
        {
            return true;
        }else
        {
            return false;
        }
        
    }
    //Constructor de Cadeteria
    
    
    public void AgregarCadetes(List<Cadete> Lista){
        ListaCadete = Lista;
    }
    // Aceptar un pedido y ponerlo en "Espera"
    // public void AceptarPedido(int num, string obs, string nomb, string dir, string telef, string datos)
    // {
    //     Pedido pedido = new Pedido(num, obs, nomb, dir, telef, datos); //Creamos el pedido
    //     listaPedido.Add(pedido); // Agregar los pedido a la Lista de Pedidos
    // }

    // Asignar un Pedido a un Cadete 
    /* Agregar el método AsignarCadeteAPedido en la clase Cadeteria que recibe como
    parámetro el id del cadete y el id del Pedido*/
    public Pedido AsignarPedido(int id_pedido, int idcad)
    {
      //  Cadete? cadBuscado = ListaCadete.FirstOrDefault(cad => cad.Id == idcad); control para saber si el id del cadete existe 
        Pedido? pedBuscado = listaPedido.FirstOrDefault(p => p.Numero == id_pedido);
        pedBuscado.Id_Cadete = idcad;
        accesoPedidos.Guardar(listaPedido);

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
        pedBuscado.Id_Cadete = idNuevoCadete;
        accesoPedidos.Guardar(listaPedido);
        return pedBuscado;
    }

    public bool CambiarEstadoDePedido(int id_pedido, int estado) //FAlTA controlar que el pedido tenga un cadete asociado
    {
        var pedidos = accesoPedidos.Obtener();
        Pedido? pedEncontrado = pedidos.FirstOrDefault(p => p.Numero == id_pedido);
        pedEncontrado.Estado = estado;
        accesoPedidos.Guardar(pedidos);
        if (pedEncontrado!=null)
        {
            return true;
        }else
        {
            return false;
        }
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
    public Pedido buscarPedido(int id_pedido){
        Pedido? ped = listaPedido.FirstOrDefault(ped => ped.Numero == id_pedido);
        return ped;
    }
    public float JornalACobrar(int id_cad)
    {
        return EnviosEntregados(id_cad) * 500;
    }
}