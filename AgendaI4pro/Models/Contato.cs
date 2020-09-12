using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AgendaI4pro.Models
{
    [Table("Contato")]
    public class Contato
    {
        [Key]
        public int Id { get; set; }        
        
        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        public List<int> IdEmail { get; set; }
        [Required]
        public List<string> Email { get; set; }

        public List<int> IdTelefone { get; set; }
        [Required]
        public List<string> Telefone { get; set; }
        
    }
}
