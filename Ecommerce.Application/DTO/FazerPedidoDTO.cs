namespace Ecommerce.Application.DTO
{

    public class FazerPedidoDTO
    {


        public int Id { get; set; }
        public string Usuario { get; set; }
        public DateTime DataPedido { get; set; }
        public string Status { get; set; }
        public string TipoPedido { get; set; }
    }
}
