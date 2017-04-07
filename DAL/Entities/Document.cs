using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("document")]
    public class Document
    {
        public int Id { get; set; }
        public DateTime DateDocument { get; set; }
        public virtual ICollection<DocumentTable> DocumentTable { get; set; }
        public int TemplateId { get; set; }
        public virtual Template Template { get; set; }
    }
}
