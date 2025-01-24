using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioBackEnd.Models
{
    public class VENDAS
    {
        public VENDAS()
        {

        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idVenda { get; set; }

        public DateTime DataVenda { get; set; }

        public float Valor { get; set; }

        public bool Pago { get; set; }

        [Required]
        public long idUsuario { get; set; }

        public virtual USUARIOS? Usuario { get; set; }

        public ICollection<PRODUTO_VENDAS>? PRODUTO_VENDAS { get; set; }

    }
}