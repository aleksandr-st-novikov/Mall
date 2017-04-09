using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EFContext
{
    public class DictionaryEFContext : ApplicationEFContext
    {
        public async Task AddDictionaryAsync(Dictionary dictionary)
        {
            Dictionary entry = await context.Dictionary.FirstOrDefaultAsync(t => t.Id == dictionary.Id);
            if (entry != null)
            {
                entry.BrandId = dictionary.BrandId;
                entry.IsTranslate = dictionary.IsTranslate;
                entry.WordOriginal = dictionary.WordOriginal;
                entry.WordTranslate = dictionary.WordTranslate;
            }
            else
            {
                context.Dictionary.Add(dictionary);
            }
        }

    }
}
