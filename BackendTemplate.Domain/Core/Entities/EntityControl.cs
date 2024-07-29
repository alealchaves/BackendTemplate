using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendTemplate.Domain.Core.Entities
{
    [ComplexType]
    public class EntityControl
    {
        [Required]
        [DefaultValue(true)]
        [Column("Ativo")]
        public virtual bool Ativo { get; set; }

        [Required]
        [Column("DataInclusao")]
        public virtual DateTime DataInclusao { get; set; }

        [Required]
        [Column("UsuarioInclusao")]
        public virtual string UsuarioInclusao { get; set; }

        [Column("DataUltimaAlteracao")]
        public virtual DateTime? DataUltimaAlteracao { get; set; }

        [Column("UsuarioUltimaAlteracao")]
        public virtual string UsuarioUltimaAlteracao { get; set; }

        [Column("DataInativacao")]
        public virtual DateTime? DataInativacao { get; set; }

        [Column("UsuarioInativacao")]
        public virtual string UsuarioInativacao { get; set; }

        public void RegistrarInclusao(string usuario = null)
        {
            this.Ativo = true;
            this.DataInclusao = DateTime.Now;

            if (!string.IsNullOrWhiteSpace(usuario))
            {
                this.UsuarioInclusao = usuario;
            }
        }

        public void RegistrarAlteracao(string usuario = null)
        {
            this.DataUltimaAlteracao = DateTime.Now.Date;

            if (!string.IsNullOrWhiteSpace(usuario))
            {
                this.UsuarioUltimaAlteracao = usuario;
            }
        }

        public void RegistrarInativacao(string usuario = null)
        {
            this.Ativo = false;
            this.DataInativacao = DateTime.Now;

            if (!string.IsNullOrWhiteSpace(usuario))
            {
                this.UsuarioInativacao = usuario;
            }
        }
    }
}
