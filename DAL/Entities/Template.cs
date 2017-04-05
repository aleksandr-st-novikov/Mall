using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("template")]
    public class Template
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Commentary { get; set; }
        public virtual ICollection<TemplateTable> TemplateTable { get; set; }
    }
}
