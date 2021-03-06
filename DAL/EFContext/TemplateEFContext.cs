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
    public class TemplateEFContext : ApplicationEFContext<Template>
    {
        public async new Task DeleteByIdAsync(int templateId)
        {
            Template entry = await context.Template.FindAsync(templateId);
            if (entry != null)
            {
                context.Template.Remove(entry);
            }
            List<TemplateTable> toDelete = await context.TemplateTable.Where(t => t.TemplateId == templateId).ToListAsync();
            if (toDelete.Count() > 0)
            {
                context.TemplateTable.RemoveRange(toDelete);
            }
        }

        //public async Task<List<TemplateForDocumentView>> GetTemplateForDocumentByNameAsync(string name)
        //{
        //    List<TemplateForDocumentView> tml = await (from t in context.Template
        //                                               join tt in context.TemplateTable on t.Id equals tt.TemplateId
        //                                               join sd in context.SettingDoc on tt.SettingDocId equals sd.Id
        //                                               where t.Name == name && sd.IsActive == true
        //                                               select new TemplateForDocumentView()
        //                                               {
        //                                                   FileldIn = tt.FieldIn,
        //                                                   FieldOutName = sd.Descr,
        //                                                   FieldOut = sd.DocField,
        //                                                   Order = tt.Order,
        //                                                   Prefix = tt.Prefix,
        //                                                   Postfix = tt.Postfix,
        //                                                   Translate = tt.Translate
        //                                               }).ToListAsync();

        //    return tml;
        //}

        public async Task<List<TemplateForDocumentView>> GetTemplateForDocumentByIdAsync(int templateId)
        {
            List<TemplateForDocumentView> tml = await (from t in context.Template
                                                       join tt in context.TemplateTable on t.Id equals tt.TemplateId
                                                       join sd in context.SettingDoc on tt.SettingDocId equals sd.Id
                                                       where t.Id == templateId && sd.IsActive == true
                                                       select new TemplateForDocumentView()
                                                       {
                                                           FileldIn = tt.FieldIn,
                                                           FieldOutName = sd.Descr,
                                                           FieldOut = sd.DocField,
                                                           Order = tt.Order,
                                                           Prefix = tt.Prefix,
                                                           Postfix = tt.Postfix,
                                                           Translate = tt.Translate,
                                                           TranslateByWord = tt.TranslateByWord,
                                                           DictionaryId = tt.DictionaryId
                                                       }).ToListAsync();

            return tml;
        }

        public async Task<List<string>> GetListTemplateNameAsync()
        {
            return await context.Template.OrderBy(t => t.Name).Select(t => t.Name).ToListAsync();
        }

    }
}
