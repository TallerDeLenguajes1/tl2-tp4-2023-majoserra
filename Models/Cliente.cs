namespace EspacioPedido;
public class Cliente
{
    private string nombre;
    private string direccion;
    private string telefono;
    private string datosReferencia;

    

    public Cliente(){

    }
    public Cliente(string nomb, string dir, string tel, string datos)
    {
        Nombre = nomb;
        Direccion = dir;
        Telefono = tel;
        DatosReferencia = datos;
    }

    public string Nombre { get => nombre; set => nombre = value; }
    public string Direccion { get => direccion; set => direccion = value; }
    public string Telefono { get => telefono; set => telefono = value; }
    public string DatosReferencia { get => datosReferencia; set => datosReferencia = value; }
}