using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("dictionary_table")]
    public class DictionaryTable
    {
        public int Id { get; set; }
        public int DictionaryId { get; set; }
        public string WordOriginal { get; set; }
        public string WordTranslate { get; set; }
        public bool IsTranslate { get; set; }
        public virtual Dictionary Dictionary { get; set; }
    }
}
