using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ViewModel
{
    public class DictionaryTranslateResultView
    {
        public string Translate { get; set; }

        public override string ToString()
        {
            return Translate;
        }
    }
}
