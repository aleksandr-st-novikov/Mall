using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModel
{
    public class TemplateForDocumentView
    {
        public string FileldIn { get; set; }
        public string FieldOut { get; set; }
        public int Order { get; set; }
        public string Prefix { get; set; }
        public string Postfix { get; set; }
        public bool Translate { get; set; }
    }
}
