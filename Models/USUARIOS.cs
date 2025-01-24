using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesafioBackEnd.Models
{
    public class USUARIOS
    {
        public USUARIOS()
        {
            
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idUsuario { get; set; }

        [Required]
        [StringLength(14)]
        public string CPF { get; set; }

        public DateTime? DataNascimento { get; set; }

        [Required]
        [StringLength(150)]
        public string Nome { get; set; }

        [Required]
        [StringLength(30)]
        public string Nivel { get; set; }

        [Required]
        [StringLength(30)]
        public string Login { get; set; }

        [Required]
        public string Senha { get; set; }

        [Required]
        public bool Status { get; set; }

        public DateTime DataCadastro { get; set; }

        [Required]
        public long idEmpresa { get; set; }

        public virtual EMPRESAS? Empresa { get; set; }

        public ICollection<PRODUTOS>? PRODUTOS { get; set; }

        public ICollection<VENDAS>? VENDAS { get; set; }

    }
}