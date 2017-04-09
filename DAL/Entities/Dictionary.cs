using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Dictionary
    {
        public int Id { get; set; }
        public string WordOriginal { get; set; }
        public string WordTranslate { get; set; }
        public bool IsTranslate { get; set; }
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }
    }
}
