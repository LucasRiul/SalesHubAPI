using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;

namespace DesafioBackEnd.Models
{
    public class LOTES
    {
        public LOTES()
        {
            
        }

        [Key]
        public long idLote { get; set; }

        public DateTime DataValidade { get; set; }

        public int Quantidade { get; set; }

        public DateTime DataCadastro { get; set; }

        public long idFornecedor { get; set; }

        public long idProduto { get; set; }

        public virtual PRODUTOS? Produto { get; set; }
        public virtual FORNECEDORES? FORNECEDORES { get; set; }

    }
}