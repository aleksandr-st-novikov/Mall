using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    [Table("dictionary")]
    public class Dictionary
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public string Name { get; set; }
        public string Commentary { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual ICollection<DictionaryTable> DictionaryTable { get; set; }
    }
}
