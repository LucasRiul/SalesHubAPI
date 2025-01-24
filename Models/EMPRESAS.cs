using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioBackEnd.Models
{
    public class EMPRESAS
    {
        public EMPRESAS()
        {
          
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idEmpresa { get; set; }

        [Required]
        [StringLength(18)]
        public string CNPJ { get; set; }

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
        public string Bairro { get; set; }

        [Required]
        [StringLength(100)]
        public string Cidade { get; set; }

        [Required]
        [StringLength(2)]
        public string UF { get; set; }

        [StringLength(50)]
        public string Pais { get; set; }

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [StringLength(100)]
        public string NomeFantasia { get; set; }

        [Required]
        [StringLength(100)]
        public string RegimeTributario { get; set; }

        public ICollection<USUARIOS>? USUARIOS { get; set; }

    }
}