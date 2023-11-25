using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Repos
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public int? LocalId { get; set; }
        [Required]
        [MaxLength(256)]
        public string Name { get; set; } = null!;
        [Required]
        [MaxLength(8096)]
        public string Message { get; set; } = null!;
    }
}
