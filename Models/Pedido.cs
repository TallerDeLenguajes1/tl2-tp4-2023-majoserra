namespace EspacioPedido;

public class Pedido
{
    private int numero;
    private string observacion;
    private Cliente cliente;
    private int estado;
    private int id_cadete; //Agregar una referencia a Cadete dentro de la clase Pedido
    public int Numero { get => numero; set => numero = value; }
    public int Estado { get => estado; set => estado = value; }
    public int Id_Cadete { get => id_cadete; set => id_cadete = value; }
    public Cliente Cliente { get => cliente; set => cliente = value; }

    // public void MostrarPedido()
    // {
    //     Console.WriteLine(Numero);
    //     Console.WriteLine(observacion);
    //     Console.WriteLine(Estado);
    //     Console.WriteLine(idCad);

    // }
    public Pedido(){
        
    }
    public Pedido(int num, string obs, string nomb, string dir, string telef, string datos)
    {
        Cliente = new Cliente(nomb, dir, telef, datos);
        observacion = obs;
        Estado = 0; //Pendiente
        id_cadete = 99;
    }
    
    public string verDireccionCliente()
    {
        return Cliente.Direccion;
    }
    // public void verDatosCliente()
    // {
    //     Console.WriteLine(cliente.Nombre);
    //     Console.WriteLine(cliente.Direccion);
    //     Console.WriteLine(cliente.Telefono);
    //     Console.WriteLine(cliente.DatosReferencia);
    // }
}