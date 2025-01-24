using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioBackEnd.Models
{
    public class FORNECEDORES
    {
        public FORNECEDORES()
        {
            
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idFornecedor { get; set; }

        [Required]
        [StringLength(150)]
        public string Nome { get; set; }

        [Required]
        [StringLength(17)]
        public string CPNJ { get; set; }

        [Required]
        [StringLength(30)]
        public string Telefone1 { get; set; }

        [StringLength(30)]
        public string Telefone2 { get; set; }

        [Required]
        [StringLength(10)]
        public string CEP { get; set; }

        [StringLength(20)]
        public string Logradouro { get; set; }

        [Required]
        [StringLength(100)]
        public string Endereco { get; set; }

        [Required]
        [StringLength(100)]
        public string Cidade { get; set; }

        [Required]
        [StringLength(100)]
        public string Bairro { get; set; }

        [Required]
        [StringLength(2)]
        public string UF { get; set; }

        [StringLength(50)]
        public string Pais { get; set; }

        public ICollection<PRODUTOS>? PRODUTOS { get; set; }
        public ICollection<LOTES>? LOTES{ get; set; }
    }
}