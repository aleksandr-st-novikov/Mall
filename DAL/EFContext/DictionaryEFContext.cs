﻿using DAL.Entities;
using DAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EFContext
{
    public class DictionaryEFContext : ApplicationEFContext<Dictionary>
    {
        public override async Task DeleteByIdAsync(int dictionaryId)
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

        public async Task<string> GetTranslateAsync(string original, int dictionaryId)
        {
            DictionaryTranslateResultView entry = await (from d in context.Dictionary
                                                         join dt in context.DictionaryTable on d.Id equals dt.DictionaryId
                                                         where d.Id == dictionaryId && dt.IsTranslate == true && dt.WordOriginal == original
                                                         select new DictionaryTranslateResultView { Translate = dt.WordTranslate })
                                                         .FirstOrDefaultAsync();
            return entry != null ? entry.ToString() : "";
        }

    }
}
