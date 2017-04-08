using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("setting_doc")]
    public class SettingDoc
    {
        public int Id { get; set; }
        public string DocField { get; set; }
        public string Descr { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<TemplateTable> TemplateTable { get; set; }
    }
}
