using System.Data.Common;
using EspacioPedido;
namespace EspacioInforme
{
    public class Informe
    {
        private int cantPedidos;
        private float totalCobrarCadetes;

        public int CantPedidos { get => cantPedidos; set => cantPedidos = value; }
        public float TotalCobrarCadetes { get => totalCobrarCadetes; set => totalCobrarCadetes = value; }

        
    }

}