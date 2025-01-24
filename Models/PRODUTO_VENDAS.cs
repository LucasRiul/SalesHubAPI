using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;

namespace DesafioBackEnd.Models
{
    public class PRODUTO_VENDAS
    {
        public PRODUTO_VENDAS()
        {
            
        }
        public long idProduto { get; set; }

        public long idVenda { get; set; }

        public int Quantidade { get; set; }

        public virtual PRODUTOS? Produto { get; set; }

        public virtual VENDAS? Venda { get; set; }

    }
}