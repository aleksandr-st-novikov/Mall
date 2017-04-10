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
        public async Task<int> AddDictionaryAsync(Dictionary dictionary)
        {
            Dictionary entry = await context.Dictionary.FirstOrDefaultAsync(t => t.Id == dictionary.Id);
            if (entry != null)
            {
                entry.BrandId = dictionary.BrandId;
                entry.Commentary = dictionary.Commentary;
                entry.Name = dictionary.Name;
            }
            else
            {
                context.Dictionary.Add(dictionary);
            }
            await context.SaveChangesAsync();
            return dictionary.Id;
        }

        public async Task DeleteDictionaryAsync(int dictionaryId)
        {
            Dictionary entry = await context.Dictionary.FindAsync(dictionaryId);
            if (entry != null)
            {
                context.Dictionary.Remove(entry);
            }
            List<DictionaryTable> toDelete = await context.DictionaryTable.Where(t => t.DictionaryId == dictionaryId).ToListAsync();
            if (toDelete.Count() > 0)
            {
                context.DictionaryTable.RemoveRange(toDelete);
            }
        }

        public async Task<Dictionary> GetDictionaryByIdAsync(int dictionaryId)
        {
            return await context.Dictionary.FindAsync(dictionaryId);
        }

    }
}
