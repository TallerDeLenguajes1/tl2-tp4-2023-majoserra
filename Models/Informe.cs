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

        public Informe(int id_cad, Cadeteria cadet){
            cantPedidos = cadet.EnviosEntregados(id_cad);
            totalCobrarCadetes = cadet.JornalACobrar(id_cad);

        }
        
    }

}