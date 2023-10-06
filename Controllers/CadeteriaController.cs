using EspacioInforme;
using EspacioPedido;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
namespace CadeteriaController;

[ApiController]  // atributo que indica que es un controlador 
[Route("[controller]")] // Ruta con la que se va a direccionar el recurso, en este ca<so se utilizara el nombre de la clase

public class CadeteriaController : ControllerBase // Herencia de la clase ControllerBase 
{
    private static Cadeteria? cadeteria; // campo estatico (Al ser estatica esta variable es compartida entre todas las instancias de la clase)

    private readonly ILogger<CadeteriaController> _logger; // se utiliza para realizar registros  y (readonly siginifca que una vez que se asigna el valor no puede volver a cambiarse)

    public CadeteriaController(ILogger<CadeteriaController> logger)
    {
        _logger = logger;
        cadeteria = Cadeteria.GetInstance(); // Consultar 
    }
    

    [HttpGet("GetPedidos")] // atributo que indica el verbo http de la solicitud
    // [Get] GetPedidos() => Retorna una lista de Pedidos
    public ActionResult<List<Pedido>> GetPedidos(){
        List<Pedido> pedidos = cadeteria.GetPedidos();
        return Ok(pedidos);
    }
    
    [HttpGet("GetCadetes")]
    //[Get] GetCadetes() => Retorna una lista de Cadetes
    public ActionResult<List<Cadete>> GetCadetes(){
        List<Cadete> listaCadete = cadeteria.GetCadetes();
        return Ok(listaCadete);
    }

    [HttpGet("GetInforme")]
    // [Get] GetInforme() => Retorna un objeto Informe
    public ActionResult<string> GetInforme(int id_cad, List<Pedido> ListaPedido){
        Informe info = new Informe(cadeteria, id_cad, listaPedido);
        return Ok(info);
    }

    [HttpPost("AgregarPedido")]
    // [Post] AgregarPedido(Pedido pedido)
    public ActionResult<Pedido> AgregarPedido(Pedido pedido){

        var ped = cadeteria.AgregarPedido(pedido);
        return Ok(pedido);
    }

    [HttpPut("AsignarPedido")]
    // [Put] AsignarPedido(int idPedido, int idCadete)
    public ActionResult<Pedido> AsignarPedido(int idPedido, int idCadete){
        Pedido ped = cadeteria.AsignarPedido(idPedido, idCadete);
        return Ok(ped);
    }

    [HttpPut("CambiarEstadoPedido")]
    // [Put] CambiarEstadoPedido(int idPedido,int NuevoEstado)
    public ActionResult<Pedido> CambiarEstadoPedido(int idPedido, int nuevoEstado){
        Pedido pedNuevoEstado = cadeteria.CambiarEstadoDePedido(idPedido, nuevoEstado);
        return Ok(pedNuevoEstado);
    } 

    [HttpPut("CambiarCadetePedido")]
    // [Put] CambiarCadetePedido(int idPedido,int idNuevoCadete)
    public ActionResult<Pedido> CambiarCadetePedido(int idPedido, int idNuevoCadete){
        Pedido pedCadeteCambiado = cadeteria.ReasignarPedido(idPedido, idNuevoCadete);
        return Ok(pedCadeteCambiado);
    }

}
