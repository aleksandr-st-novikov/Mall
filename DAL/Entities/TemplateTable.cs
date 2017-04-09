using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("template_table")]
    public class TemplateTable
    {
        public int Id { get; set; }

        //[ForeignKey("template_fk")]
        public int TemplateId { get; set; }
        public string FieldIn { get; set; }
        public int SettingDocId { get; set; }
        public int Order { get; set; }
        public string Prefix { get; set; }
        public string Postfix { get; set; }
        public bool Translate { get; set; }
        public bool TranslateByWord { get; set; }
        public virtual Template Template { get; set; }
        public virtual SettingDoc SettingDoc { get; set; }
    }
}
