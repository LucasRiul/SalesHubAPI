using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace DesafioBackEnd.Models
{
    public class PRODUTOS
    {
        public PRODUTOS()
        {
            
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idProduto { get; set; }

        [Required]
        [StringLength(50)]
        public string Nome { get; set; }

        [Required]
        public int EstoqueMinimo { get; set; }

        [Required]
        public int EstoqueInicial { get; set; }

        public int EstoqueAtual { get; set; }

        [Required]
        public float PrecoCusto { get; set; }

        [Required]
        public float PrecoVenda { get; set; }

        public float LUCRO { get; set; }

        public float ICMS { get; set; }

        public float ISS { get; set; }

        public float COFINS { get; set; }

        [Required]
        public long idUsuario { get; set; }

        [Required]
        public long idFornecedor { get; set; }

        public virtual USUARIOS? Usuario { get; set; }

        public virtual FORNECEDORES? Fornecedor { get; set; }

        public ICollection<PRODUTO_VENDAS>? PRODUTO_VENDAS { get; set; }

    }
}